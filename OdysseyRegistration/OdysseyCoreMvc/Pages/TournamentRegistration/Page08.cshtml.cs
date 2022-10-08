﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page08.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page08Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.Pages.TournamentRegistration
{
    /// <summary>
    /// The Tournament Registration Page08 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page08Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page08Model(Data.OdysseyContext context)
            : base(context)
        {
            this.CurrentRegistrationType = RegistrationType.Tournament;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

        public string? SchedulingIssues { get; set; }

        public string? SpecialConsiderations { get; set; }

        public Models.TournamentRegistration? TournamentRegistration { get; set; }
    }
}

