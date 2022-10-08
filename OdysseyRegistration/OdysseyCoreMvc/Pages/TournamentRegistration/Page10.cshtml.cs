// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page10.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page10Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.Pages.TournamentRegistration
{
    /// <summary>
    /// The Tournament Registration Page10 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page10Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page10Model(Data.OdysseyContext context)
            : base(context)
        {
            this.CurrentRegistrationType = RegistrationType.Tournament;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

        public bool AcceptingPayPal
        {
            get
            {
                bool.TryParse(base.Config["AcceptingPayPal"], out bool flag);
                return flag;
            }
        }

        public string? Division { get; set; }

        public string? JudgeErrorMessage { get; set; }

        public string? JudgeFirstName { get; set; }

        public string? JudgeLastName { get; set; }

        public string? MailBody { get; set; }

        public string? MailErrorMessage { get; set; }

        public string? PaymentDueDate
        {
            get
            {
                DateTime? paymentDueDate = base.TournamentInfo.PaymentDueDate;
                return (paymentDueDate.HasValue ? paymentDueDate.Value.ToLongDateString() : "TBA");
            }
        }

        public string? ProblemName { get; set; }

        public string? SchoolName { get; set; }

        public Models.TournamentRegistration? TournamentRegistration { get; set; }

        public string? VolunteerErrorMessage { get; set; }

        public string? VolunteerFirstName { get; set; }

        public string? VolunteerLastName { get; set; }
    }
}

