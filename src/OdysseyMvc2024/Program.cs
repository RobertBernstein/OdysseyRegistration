// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Tardis Technologies">
//   Copyright 2025 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the main program for the entire application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OdysseyMvc2024.Models;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with connection string
builder.Services.AddDbContext<OdysseyEntities>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OdysseyConnection")));

// builder.Services.AddScoped<IOdysseyEntities, OdysseyEntities>();
builder.Services.AddScoped<IOdysseyRepository, OdysseyRepository>();

var app = builder.Build();

var logSqlServerUnavailable = LoggerMessage.Define<string?>(
    LogLevel.Error,
    new EventId(1001, nameof(OdysseyEntities)),
    "SQL Server is unavailable while processing request {Path}.");

var logSqlNotificationSent = LoggerMessage.Define<string>(
    LogLevel.Information,
    new EventId(1002, nameof(OdysseyEntities)),
    "SQL outage notification email sent to {Recipient}.");

var logSqlNotificationFailed = LoggerMessage.Define<string>(
    LogLevel.Error,
    new EventId(1003, nameof(OdysseyEntities)),
    "Failed to send SQL outage notification email: {Reason}");

const string DatabaseUnavailableHtml = """
<!DOCTYPE html>
<html lang=\"en\">
<head>
    <meta charset=\"utf-8\" />
    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />
    <title>Registration Site Temporarily Down</title>
</head>
<body>
    <h1>NoVA North Odyssey of the Mind Registration Site Temporarily Down</h1>
    <p>The registration site is temporarily down for maintenance.</p>
    <p>Please try again in a few minutes.</p>
</body>
</html>
""";

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    try
    {
        // In the debugger, a first-chance SqlException can break here in framework/EF code before stack unwinding
        // reaches this middleware. Continuing execution (F5) allows the exception to propagate back to this await and
        // be handled by the catch block below.
        await next().ConfigureAwait(false);
    }
    catch (SqlException sqlException) when (IsSqlServerUnavailable(sqlException))
    {
        // With Just My Code enabled, Visual Studio may report this as "not handled in user code" at throw time.
        // That message reflects debugger timing/location, not final handling outcome, because this catch still handles
        // matching SQL outages.
        logSqlServerUnavailable(app.Logger, context.Request.Path.Value, sqlException);

        try
        {
            var notificationRecipient = await SendSqlOutageNotificationAsync(app, context, sqlException, context.RequestAborted).ConfigureAwait(false);
            logSqlNotificationSent(app.Logger, notificationRecipient, null);
        }
        catch (InvalidOperationException invalidOperationException)
        {
            logSqlNotificationFailed(app.Logger, invalidOperationException.Message, invalidOperationException);
        }
        catch (SmtpException smtpException)
        {
            logSqlNotificationFailed(app.Logger, smtpException.StatusCode.ToString(), smtpException);
        }
        catch (FormatException formatException)
        {
            logSqlNotificationFailed(app.Logger, formatException.Message, formatException);
        }

        if (context.Response.HasStarted)
        {
            throw;
        }

        context.Response.Clear();
        context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        context.Response.ContentType = "text/html; charset=utf-8";

        await context.Response.WriteAsync(DatabaseUnavailableHtml, context.RequestAborted).ConfigureAwait(false);
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles(); // serves from wwwroot

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();

await app.RunAsync().ConfigureAwait(false);

static bool IsSqlServerUnavailable(SqlException sqlException) =>
    sqlException.Number is 2 or 53 ||
    sqlException.InnerException is Win32Exception { NativeErrorCode: 2 or 53 };

static async Task<string> SendSqlOutageNotificationAsync(WebApplication app, HttpContext context, SqlException sqlException, CancellationToken cancellationToken)
{
    IConfigurationSection notificationSection = app.Configuration.GetSection("SqlOutageNotification");

    string smtpHost = notificationSection["SmtpHost"] ?? throw new InvalidOperationException("SqlOutageNotification:SmtpHost is missing.");

    // TODO: Consider validating SmtpPort and EnableSsl if you want to support non-default ports or SSL. For now, we assume defaults.
    // - Sending email via WinHost was failing when these values were included.
    //if (!int.TryParse(notificationSection["SmtpPort"], out int smtpPort))
    //{
    //    throw new InvalidOperationException("SqlOutageNotification:SmtpPort is missing or invalid.");
    //}

    //if (!bool.TryParse(notificationSection["EnableSsl"], out bool enableSsl))
    //{
    //    throw new InvalidOperationException("SqlOutageNotification:EnableSsl is missing or invalid.");
    //}

    string fromAddressValue = notificationSection["FromAddress"] ?? "webmaster@novanorth.org";
    string toAddressValue = notificationSection["ToAddress"] ?? "webmaster@novanorth.org";

    MailAddress fromAddress = new(fromAddressValue);
    MailAddress toAddress = new(toAddressValue);

    string? username = notificationSection["Username"];
    string? password = notificationSection["Password"];

    if (!string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
    {
        throw new InvalidOperationException("SqlOutageNotification:Password is required when Username is configured.");
    }

    StringBuilder bodyBuilder = new();
    bodyBuilder.AppendLine("A SQL Server availability error occurred in OdysseyMvc2024.");
    bodyBuilder.AppendLine();
    bodyBuilder.Append("Timestamp (UTC): ");
    bodyBuilder.AppendLine(DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture));
    bodyBuilder.Append("Request Path: ");
    bodyBuilder.AppendLine(context.Request.Path.ToString());
    bodyBuilder.Append("Request Method: ");
    bodyBuilder.AppendLine(context.Request.Method);
    bodyBuilder.Append("Query String: ");
    bodyBuilder.AppendLine(context.Request.QueryString.ToString());
    bodyBuilder.AppendLine();
    bodyBuilder.AppendLine("Full Exception Details:");
    bodyBuilder.AppendLine(sqlException.ToString());

    using MailMessage message = new(fromAddress, toAddress)
    {
        Subject = "Odyssey Registration SQL Server Unavailable",
        Body = bodyBuilder.ToString(),
        IsBodyHtml = false,
    };

    // TODO: Consider including the smtpPort and enableSsl parameters in the SmtpClient constructor to support non-default ports or SSL.
    // - Sending email via WinHost was failing when these values were included.
    using SmtpClient smtpClient = new(smtpHost/*, smtpPort*/)
    {
        //EnableSsl = enableSsl,
    };

    if (!string.IsNullOrWhiteSpace(username))
    {
        smtpClient.Credentials = new NetworkCredential(username, password);
    }

    await smtpClient.SendMailAsync(message, cancellationToken).ConfigureAwait(false);
    return toAddress.Address;
}
