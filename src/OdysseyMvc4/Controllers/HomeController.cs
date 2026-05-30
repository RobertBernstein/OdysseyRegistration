// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Tardis Technologies">
//   Copyright 2021 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Controllers
{
    using System.Web.Mvc;

    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : BaseRegistrationController
    {
        /// <summary>
        /// Provides database access.
        /// </summary>
        private readonly IOdysseyRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
            repository = new OdysseyRepository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class
        /// with an injected repository, used for unit testing.
        /// </summary>
        public HomeController(IOdysseyRepository repository) : base(repository)
        {
            this.repository = repository;
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
            var viewData = new BaseViewData();
            this.SetBaseViewData(viewData);

            this.ViewData["Message"] =
                "Welcome to the " + this.repository.Config["RegionName"] + " Odyssey of the Mind Region " +
                this.repository.Config["RegionNumber"] + " Registration web pages.";

            return this.View(viewData);
        }
    }
}
