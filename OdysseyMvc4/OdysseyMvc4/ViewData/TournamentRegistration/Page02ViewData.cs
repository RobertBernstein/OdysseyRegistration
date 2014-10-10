namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.ViewData;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Page02ViewData : BaseViewData
    {
        public IEnumerable<SelectListItem> SchoolList { get; set; }

        [Required(ErrorMessage="Please select one of the schools or organizations listed in the \"School/Organization Name\" field."), Display(Name="School or Sponsoring Organization Name")]
        public int SelectedSchool { get; set; }
    }
}

