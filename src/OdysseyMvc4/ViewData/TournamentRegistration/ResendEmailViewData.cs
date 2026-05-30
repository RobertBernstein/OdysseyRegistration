namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.ViewData;
    using System;
    using System.Runtime.CompilerServices;

    public class ResendEmailViewData : BaseViewData
    {
        public string AltCoachCheckbox { get; set; }

        public string CoachCheckbox { get; set; }

        public string ErrorMessage { get; set; }

        public bool Success { get; set; }

        public int TeamNumber { get; set; }
    }
}

