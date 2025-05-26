// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2025 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base registration controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.BaseRegistrationController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Net;
using System.Net.Mail;
using ElmahCore;
using Microsoft.AspNetCore.Mvc;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData;

namespace OdysseyMvc2024.Controllers
{
    /// <summary>
    /// The base registration controller.
    /// </summary>
    public class BaseRegistrationController(IOdysseyEntities context) : Controller
    {
        /// <summary>
        /// The object that provides access to the database.
        /// </summary>
        protected readonly OdysseyRepository Repository = new(context);

        [HttpGet]
        public ActionResult BadEmail()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);

            // TODO: (Rob, 05/25/2025) This is a temporary fix to get the BadEmail page working.  We should
            // probably create a BadEmailViewData class that inherits from BaseViewData and use that
            // instead of BaseViewData.
            // TOD: (Rob, 05/25/2025) Why did I return View() here? That doesn't return the BadEmail view!
            //return View();

            // TODO: Test that this is the correct path to the BadEmail page, Rob - 01/18/2015.
            return this.View("~/Views/Shared/BadEmail.cshtml");
        }

        /// <summary>
        /// Construct all the components of an e-mail message
        /// </summary>
        /// <param name="from">
        /// The sender of the e-mail message.
        /// </param>
        /// <param name="subject">
        /// The subject of the e-mail message.
        /// </param>
        /// <param name="body">
        /// The contents of the e-mail message.
        /// </param>
        /// <param name="to">
        /// The recipient(s) of the e-mail message; separate multiple recipients with commas (,).
        /// </param>
        /// <param name="bcc">
        /// The blind carbon copy recipient(s) of the e-mail message; separate multiple recipients with commas (,).
        /// </param>
        /// <param name="cc">
        /// The carbon copy recipient(s) of the e-mail message; separate multiple recipients with commas (,).
        /// </param>
        /// <returns>
        /// The constructed e-mail message.
        /// </returns>
        protected MailMessage? BuildMessage(
          string from,
          string subject,
          string body,
          string to,
          string bcc,
          string cc)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                Priority = MailPriority.Normal
            };

            // Set the recipient address of the mail message.
            string str = to;
            char[] chArray = [','];
            foreach (string recipient in str.Split(chArray))
            {
                try
                {
                    mailMessage.To.Add(new MailAddress(recipient));
                }
                catch (FormatException ex)
                {
                    // Log the exception using Elmah
                    ElmahExtensions.RaiseError(ex);
                    return null;
                }
            }

            // Check if the bcc value is null or an empty string
            if (!string.IsNullOrWhiteSpace(bcc))
            {
                // Set the Bcc address of the mail message
                mailMessage.Bcc.Add(new MailAddress(bcc));
            }
            
            // Check if the cc value is null or an empty value
            if (!string.IsNullOrWhiteSpace(cc))
            {
                // Set the CC address of the mail message
                mailMessage.CC.Add(new MailAddress(cc));
            }

            return mailMessage;
        }

        /// <summary>
        /// Handle HTTP GET requests for the Closed page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the same page with the viewData
        /// populated.
        /// </returns>
        [HttpGet]
        public ActionResult Closed()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View((object)baseViewData);
        }

        /// <summary>
        /// Determines the appropriate CSS file to use based on the current page URL.
        /// </summary>
        /// <remarks>
        /// This method checks the page link in the <see cref="Url"/> property to determine which
        /// CSS file to apply. Ensure that the <see cref="Url"/> property is not null and provides a valid page link 
        /// for this method to function correctly.
        /// </remarks>
        /// <returns>
        /// A string representing the relative path to the CSS file. Returns "~/Content/NovaSouth.css"  if the page URL
        /// contains "novasouth" (case-insensitive); otherwise, returns "~/Content/NovaNorth.css".
        /// </returns>
        private string DetermineSiteCssFile() =>
            !string.IsNullOrEmpty(Url?.PageLink()) && // Added null conditional operator to handle potential null reference
            Url.PageLink()!.Contains("novasouth", StringComparison.InvariantCultureIgnoreCase) // Added null-forgiving operator
                ? Url.Content("~/Content/NovaSouth.css")
                : Url.Content("~/Content/NovaNorth.css"); // TODO: (Rob, 05/25/2015) Handle the case where Url is null.

        /// <summary>
        /// Determines the site name based on the current HTTP request's host.
        /// </summary>
        /// <remarks>
        /// The method retrieves the host from the current HTTP context and processes it to
        /// derive the site name. The result is always in lowercase.
        /// </remarks>
        /// <returns>
        /// A string representing the site name. If the host starts with "www.", the "www." prefix is removed. If the
        /// page link is unavailable, the default site name "novanorth.org" is returned.
        /// </returns>
        private string DetermineSiteName()
        {
            string hostname = HttpContext.Request.Host.Host;

            string siteName = !string.IsNullOrEmpty(Url.PageLink()) 
                ? hostname.ToLowerInvariant()
                : "novanorth.org";

            if (siteName.StartsWith("www."))
            {
                siteName = siteName.Substring(4);
            }

            return siteName;
        }

        /// <summary>
        /// Handle HTTP GET requests for the Down page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the same page with the viewData
        /// populated.
        /// </returns>
        [HttpGet]
        public ActionResult Down()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View((object)baseViewData);
        }

        /// <summary>
        /// Handle HTTP GET requests for the Error page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the same page with the viewData
        /// populated.
        /// </returns>
        [HttpGet]
        public ActionResult Error()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View((object)baseViewData);
        }

        /// <summary>
        /// Generates a user-friendly name for the current registration type, , e.g. Judges, Tournament.
        /// </summary>
        /// <remarks>This method returns a descriptive string based on the value of the <see
        /// cref="CurrentRegistrationType"/> property. If the registration type is <see
        /// cref="BaseRegistrationController.RegistrationType.None"/>, an empty string is returned. For the <see
        /// cref="BaseRegistrationController.RegistrationType.CoachesTraining"/> type, a specific name is returned. For
        /// all other registration types, the name is derived from the type's string representation, followed by "
        /// Registration".</remarks>
        /// <returns>
        /// A user-friendly string representing the current registration type, or an empty string if the registration
        /// type is <see cref="BaseRegistrationController.RegistrationType.None"/>.
        /// </returns>
        /// <remarks>
        /// TODO: Write tests for this.
        /// </remarks>
        public string GetFriendlyRegistrationName()
        {
            // Make sure that CurrentRegistrationType has been set before calling this method.
            // TODO: we should probably assert here if CurrentRegistrationType has not been set.
            // TODO: we should definitely log an error here if CurrentRegistrationType has not been set.
            if (this.CurrentRegistrationType == RegistrationType.None)
            {
                // If the registration type is None, return an empty string.
                return string.Empty;
            }

            return this.CurrentRegistrationType == RegistrationType.CoachesTraining
                ? "Coaches Training Registration"
                : $"{this.CurrentRegistrationType} Registration";
        }

        /// <summary>
        /// Determine whether the specified registration is closed based on the Config
        /// table in the database.
        /// </summary>
        /// <param name="registrationType">
        /// The registration type.
        /// </param>
        /// <returns>
        /// true if registration is closed, false otherwise.
        /// </returns>
        public bool IsRegistrationClosed(RegistrationType registrationType)
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);

            DateTime registrationCloseDateTime;
            try
            {
                // TODO: Change this to TryParse (everywhere).
                // TODO: This will fail if you specify an invalid date, such as 2/29 on a non-leap year! Log such possibilities. Add a unit test for this.
                registrationCloseDateTime = DateTime.Parse(baseViewData.Config[registrationType.ToString() + "RegistrationCloseDateTime"]);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception.
                // If we cannot even read the closing time from the database, close down registration!
                // Log the exception.
                ElmahExtensions.RaiseError(ex);
                return true;
            }

            // TODO: Add app setting to decide what timezone to consider local.
            // Adjust for the configured local timezone
            // TODO: (Rob, 05/25/2025) This is hard coded to Eastern Time. We should probably make this configurable.
            // TODO: (Rob, 05/25/2025) Test this!
            TimeZoneInfo localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"); // Example for Eastern Time
            DateTime currentEasternTime = TimeZoneInfo.ConvertTime(DateTime.Now, localTimeZone);

            // Compare the registration close date and time with the current time in the local timezone.
            // When the comparison is less than 0, it means the registration close date and time is in
            // the past, hence registration is closed.
            return DateTime.Compare(registrationCloseDateTime, currentEasternTime) < 0;
        }

        /// <summary>
        /// Determine whether the specified registration is coming soon based on the
        /// Config table in the database.
        /// </summary>
        /// <param name="registrationType">
        /// The registration type.
        /// </param>
        /// <returns>
        /// true if registration is coming soon, false otherwise.
        /// </returns>
        public bool IsRegistrationComingSoon(RegistrationType registrationType)
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);

            DateTime registrationOpenDate;
            try
            {
                registrationOpenDate = DateTime.Parse(baseViewData.Config[registrationType.ToString() + "RegistrationOpenDateTime"]);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception.
                // If we cannot even read the opening time from the database, assume we've passed the opening date.
                ElmahExtensions.RaiseError(ex);
                return false;
            }

            // TODO: Adjust for Eastern Time if not in Eastern Time, e.g. Pacific Time
            DateTime currentEasternTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")); // Adjusted for Eastern Time
            return DateTime.Compare(currentEasternTime, registrationOpenDate) < 0;
        }

        /// <summary>
        /// Is the current registration type set to be administratively down?
        /// </summary>
        /// <param name="registrationType">
        /// The registration type.
        /// </param>
        /// <returns>
        /// true if the registration is administratively down, false otherwise.
        /// </returns>
        public bool IsRegistrationDown(RegistrationType registrationType)
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);

            if (!bool.TryParse(baseViewData.Config[$"Is{registrationType}RegistrationDown"], out bool registrationIsDown))
            {
                // If parsing fails, log the issue and assume registration is not down.
                ElmahExtensions.RaiseError(new FormatException($"Failed to parse Is{registrationType}RegistrationDown as a boolean."));
                registrationIsDown = false;
            }

            return registrationIsDown;
        }

        /// <summary>
        /// Sends the provided e-mail message.
        /// </summary>
        /// <param name="viewData">
        /// Provides access to the Config information for the current registration.
        /// </param>
        /// <param name="mailMessage">
        /// The e-mail to send.
        /// </param>
        /// <returns>
        /// null on success, an error message otherwise.
        /// </returns>
        public string? SendMessage(BaseViewData viewData, MailMessage mailMessage)
        {
            SmtpClient smtpClient = new()
            {
                // Fix for CS8602: Dereference of a possibly null reference.
                Host = viewData.Config?["EmailServer"] ?? throw new InvalidOperationException("EmailServer configuration is missing."),
                Credentials = new NetworkCredential(
                    viewData.Config?["WebmasterEmail"] ?? throw new InvalidOperationException("WebmasterEmail configuration is missing."),
                    viewData.Config?["WebmasterEmailPassword"] ?? throw new InvalidOperationException("WebmasterEmailPassword configuration is missing."))
            };

            // Send the mail message
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException exception)
            {
                // TODO: (Rob, 05/25/2025) I had removed this from the ASP.NET MVC 4 code. Should we use this instead or as well?
                //ErrorSignal.FromCurrentContext().Raise(exception);

                // Log the exception using Elmah
                ElmahExtensions.RaiseError(exception);

                foreach (SmtpFailedRecipientException smtpFailedRecipientException in exception.InnerExceptions)
                {
                    switch (smtpFailedRecipientException.StatusCode)
                    {
                        case SmtpStatusCode.MailboxBusy:
                        case SmtpStatusCode.MailboxUnavailable:
                            Thread.Sleep(5000);
                            smtpClient.Send(mailMessage);
                            // TODO: (Rob, 05/25/2025) Log the retry attempt. Or, should this be a break?
                            ElmahExtensions.RaiseError(new Exception("Retrying to send email after mailbox unavailable."));
                            continue;
                        default:
                            return string.Format("Failed to deliver message to {0}", smtpFailedRecipientException.FailedRecipient);
                    }
                }
            }
            catch (SmtpException smtpException)
            {
                // TODO: (Rob, 05/25/2025) I had removed this from the ASP.NET MVC 4 code. Should we use this instead or as well?
                //ErrorSignal.FromCurrentContext().Raise(smtpException);

                // Log the exception using Elmah
                ElmahExtensions.RaiseError(smtpException);
                return smtpException.StatusCode.ToString();
            }

            return null;
        }

        /// <summary>
        /// Set the configuration information required by every ViewData class/object.
        /// </summary>
        /// <param name="viewData">
        /// The view data.
        /// </param>
        protected void SetBaseViewData(BaseViewData viewData)
        {
            viewData.Config = Repository.Config;
            viewData.RegionName = Repository.RegionName;
            viewData.RegionNumber = Repository.RegionNumber;
            viewData.TournamentInfo = Repository.TournamentInfo;
            viewData.FriendlyRegistrationName = FriendlyRegistrationName;
            viewData.SiteName = DetermineSiteName();
            viewData.PathToSiteCssFile = DetermineSiteCssFile();
        }

        /// <summary>
        /// Handle HTTP GET requests for the Soon page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the same page with the viewData
        /// populated.
        /// </returns>
        [HttpGet]
        public ActionResult Soon()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View((object)baseViewData);
        }

        /// <summary>
        /// Gets whether this registration system is coming soon, down, closed, or available.
        /// </summary>
        public BaseRegistrationController.RegistrationState CurrentRegistrationState
        {
            get
            {
                // Enable a user to bypass these checks
                ////if (!string.IsNullOrWhiteSpace(Request.QueryString["Test"]) &&
                ////    Request.QueryString["Test"] == DataLayer.OdysseyConfig["TestGuid"])
                ////{
                ////    return RegistrationState.Available;
                ////}

                BaseViewData baseViewData = new(Repository)
                {
                    Config = Repository.Config,
                    TournamentInfo = Repository.TournamentInfo
                };

                SetBaseViewData(baseViewData);

                // Is it too early to register?
                if (IsRegistrationComingSoon(CurrentRegistrationType))
                {
                    return RegistrationState.Soon;
                }

                // Is registration temporarily disabled?
                if (IsRegistrationDown(CurrentRegistrationType))
                {
                    return RegistrationState.Down;
                }

                // Is it too late to register?
                return IsRegistrationClosed(CurrentRegistrationType)
                    ? RegistrationState.Closed
                    : RegistrationState.Available;
            }
        }

        /// <summary>
        /// Gets or sets the current registration type.
        /// </summary>
        public RegistrationType CurrentRegistrationType { get; set; }

        /// <summary>
        /// Gets or sets the friendly, i.e. displayable, registration name.
        /// </summary>
        public required string FriendlyRegistrationName { get; set; }

        /// <summary>
        /// Determines the availability of a registration system.
        /// </summary>
        public enum RegistrationState
        {
            /// <summary>
            /// The registration system is available.
            /// </summary>
            Available,

            /// <summary>
            /// The registration system is closed for the year.
            /// </summary>
            Closed,

            /// <summary>
            /// The registration system is temporarily down.
            /// </summary>
            Down,

            /// <summary>
            /// The registration system is not open for the season yet.
            /// </summary>
            Soon
        }

        /// <summary>
        /// The registration type.
        /// </summary>
        public enum RegistrationType
        {
            /// <summary>
            /// The default, i.e. no registration type.  This should never be used.
            /// </summary>
            None,
            
            /// <summary>
            /// Identifies Tournament Registration.
            /// </summary>
            Tournament,

            /// <summary>
            /// Identifies Judges Registration.
            /// </summary>
            Judges,

            /// <summary>
            /// Identifies Coaches Training Registration.
            /// </summary>
            CoachesTraining,

            /// <summary>
            /// Identifies Volunteer Registration.
            /// </summary>
            Volunteer
        }
    }
}
