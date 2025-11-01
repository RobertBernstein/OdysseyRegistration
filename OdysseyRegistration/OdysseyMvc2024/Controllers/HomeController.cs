// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Tardis Technologies">
//   Copyright 2025 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The home controller for the Odyssey of the Mind registration system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.HomeController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData;
using Microsoft.AspNetCore.Mvc;

namespace OdysseyMvc2024.Controllers
{

    /// <summary>
    /// The home controller, i.e., the root of the website,  for the Odyssey Registration website.
    /// </summary>
    public class HomeController : BaseRegistrationController
    {
        public HomeController(IOdysseyRepository repository)
            : base(repository)
        {
        }

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
            BaseViewData baseViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);

            ViewData["Message"] =
                $"Welcome to the {Repository.RegionName ?? "Unknown"} Odyssey of the Mind Region {Repository.RegionNumber ?? "?"} Registration web pages.";

            return View(baseViewData);
        }
    }
}
