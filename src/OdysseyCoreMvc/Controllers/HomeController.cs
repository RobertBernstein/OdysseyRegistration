// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using OdysseyCoreMvc.Models;
using System.Diagnostics;

namespace OdysseyCoreMvc.Controllers
{
    // *** The new HomeController methods ***

    //public class HomeController : Controller
    //{
    //    private readonly ILogger<HomeController> _logger;

    //    public HomeController(ILogger<HomeController> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    public IActionResult Privacy()
    //    {
    //        return View();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    //}

    // *** The old HomeController methods ***

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : BaseRegistrationController
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Provides database access.
        /// </summary>
        private readonly OdysseyRepository repository = new OdysseyRepository();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // TODO: What do we do with this?

        /// <summary>
        /// The view for the Index page.
        /// GET: /Home/
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    var viewData = new BaseViewData();
        //    this.SetBaseViewData(viewData);

        //    this.ViewData["Message"] =
        //        "Welcome to the " + this.repository.Config["RegionName"] + " Odyssey of the Mind Region " +
        //        this.repository.Config["RegionNumber"] + " Registration web pages.";

        //    return this.View(viewData);
        //}

        //// TODO: This is the new Error() method in ASP.NET Core, renamed to prevent conflict with BaseRegistrationController.Error(). Which is correct?
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult NewError()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}