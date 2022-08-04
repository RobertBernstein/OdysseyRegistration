// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResendEmailViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the ResendEmailViewData ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.ViewData.TournamentRegistration
{
    public class ResendEmailViewData : BaseViewData
    {
        public string? AltCoachCheckbox { get; set; }

        public string? CoachCheckbox { get; set; }

        public string? ErrorMessage { get; set; }

        public bool Success { get; set; }

        public int TeamNumber { get; set; }
    }
}

