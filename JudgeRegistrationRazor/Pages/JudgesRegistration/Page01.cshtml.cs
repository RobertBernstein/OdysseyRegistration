using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using JudgeRegistrationRazor.Pages.Shared;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace JudgeRegistrationRazor.Pages.JudgesRegistration
{
    /// <summary>
    /// The Judges Registration Page01 backing page data.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page01Model : BasePageModel
    {
        public string Title { get; private set; }

        public string Message { get; private set; }

        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page01Model(OdysseyContext context, ILogger<Page01Model> logger)
            : base(context, logger)
        {
            CurrentRegistrationType = RegistrationType.Judges;
            Title = $"{DisplayableRegistrationName} Page 1 of 3";
            Message = $"Welcome to the {RegionName} Region {RegionNumber} {Config?["Year"]}-{Config?["EndYear"]} Odyssey of the Mind {DisplayableRegistrationName}.";
        }

        public IList<Judge> Judge { get; set; } = default!;

        /// <summary>
        /// Gets or sets the judges info.
        /// </summary>
        public Event? JudgesInfo { get; set; }

        /// <summary>
        /// Gets the judges training date or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingDate
        {
            get
            {
                return (JudgesInfo != null && JudgesInfo.StartDate.HasValue)
                    ? JudgesInfo.StartDate.Value.ToLongDateString()
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the judges training location or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingLocation
        {
            get
            {
                return (JudgesInfo != null && !string.IsNullOrWhiteSpace(JudgesInfo.Location))
                    ? JudgesInfo.Location
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the judges training time or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingTime
        {
            get
            {
                return (JudgesInfo != null && !string.IsNullOrWhiteSpace(JudgesInfo.Time))
                    ? JudgesInfo.Time
                    : "TBA";
            }
        }

        public string? MailRegionalDirectorHyperLink { get; set; }

        public string? MailRegionalDirectorHyperLinkText
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("mailto:");
                builder.Append(Config?["RegionalDirectorEmail"]);
                string mailString =
                    ("?subject=I would like to help at the Region " +
                    RegionNumber +
                    " Tournament&body=I cannot be a judge this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A").Replace(" ", "%20");
                builder.Append(mailString);
                return builder.ToString();
            }
        }

        public async Task OnGetAsync()
        {
            // Create a LINQ query to select the events.
            // Note: The query is only defined at this point, it has not been run against the database.
            var events = from e in Context.Events
                         select e;

            JudgesInfo = await (from o in Context.Events
                                where o.EventName.Contains("Judges") && o.EventName.Contains("Training")
                                select o).FirstAsync();

            Judge = await Context.Judges.ToListAsync();
            //Config = await Context.Configs;
        }

        public string BuildMailRegionalDirectorHyperLink()
        {
            StringBuilder builder = new();
            builder.Append("mailto:");
            builder.Append(Config?["RegionalDirectorEmail"]);
            string mailString =
                ("?subject=I would like to help at the Region " +
                RegionNumber +
                " Tournament&body=I cannot be a judge this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A").Replace(" ", "%20");
            builder.Append(mailString);
            return builder.ToString();
        }
    }
}
