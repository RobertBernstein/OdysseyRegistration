﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page02ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page02ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page02ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    /// <summary>
    /// The backing data for Page 02 of the Tournament Registration wizard.
    /// </summary>
    public class Page02ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the school list.
        /// </summary>
        public IEnumerable<SelectListItem> SchoolList { get; set; }

        /// <summary>
        /// Gets or sets the selected school.
        /// </summary>
        [Required(ErrorMessage = "Please select one of the schools or organizations listed in the \"School/Organization Name\" field.")]
        [Display(Name = "School or Sponsoring Organization Name")]
        public int SelectedSchool { get; set; }
    }
}
