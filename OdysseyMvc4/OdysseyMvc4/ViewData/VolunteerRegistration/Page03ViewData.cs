// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page03ViewData.cs" company="Tardis Technologies">
//   Copyright 2021 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page03ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData.VolunteerRegistration
{
    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;

    /// <summary>
    /// The page 03 view data.
    /// </summary>
    public class Page03ViewData : BaseViewData
    {
        public bool EmailAddressWasSpecified { get; set; }

        public string ErrorMessage { get; set; }

        public string MailBody { get; set; }

        public string MailErrorMessage { get; set; }

        public Volunteer Volunteer { get; set; }

        public Event VolunteerInfo { get; set; }
    }
}
