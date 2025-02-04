﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.HomeController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Web.Mvc;
using OdysseyMvc2023.Models;
using OdysseyMvc2023.ViewData;

namespace OdysseyMvc2023.Controllers
{

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : BaseRegistrationController
    {
        /// <summary>
        /// Provides database access.
        /// </summary>
        private readonly OdysseyRepository repository = new OdysseyRepository();

        /// <summary>
        /// The view for the Index page.
        /// GET: /Home/
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Index()
        {
            BaseViewData baseViewData = new BaseViewData();
            this.SetBaseViewData(baseViewData);

            this.ViewData["Message"] =
                      (object)("Welcome to the " + this.repository.Config["RegionName"] + " Odyssey of the Mind Region " +
                      this.repository.Config["RegionNumber"] + " Registration web pages.");

            return (ActionResult)this.View((object)baseViewData);
        }
    }
}
