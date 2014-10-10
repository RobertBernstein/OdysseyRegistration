namespace OdysseyMvc4.ViewData.VolunteerRegistration
{
    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;
    using System;
    using System.Runtime.CompilerServices;

    public class Page03ViewData : BaseViewData
    {
        public bool EmailAddressWasSpecified { get; set; }

        public string ErrorMessage { get; set; }

        public string MailBody { get; set; }

        public string MailErrorMessage { get; set; }

        public OdysseyMvc4.Models.Volunteer Volunteer { get; set; }

        public Event VolunteerInfo { get; set; }
    }
}

