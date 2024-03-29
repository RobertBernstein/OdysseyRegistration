﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResendEmailViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the ResendEmailViewData ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Data;

namespace OdysseyCoreMvc.Pages.TournamentRegistration
{
    public class ResendEmailPageModel : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public ResendEmailPageModel(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            CurrentRegistrationType = RegistrationType.Tournament;
        }

        public string? AltCoachCheckbox { get; set; }

        public string? CoachCheckbox { get; set; }

        public string? ErrorMessage { get; set; }

        public bool Success { get; set; }

        public int TeamNumber { get; set; }
    }
}

