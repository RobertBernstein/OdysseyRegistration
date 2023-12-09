// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The MVC application.
//   Note: For instructions on enabling IIS6 or IIS7 classic mode,
//   visit http://go.microsoft.com/?LinkId=9394801
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OdysseyMvc2023
{
    // TODO: No longer a valid link as of 12/09/2023.
    /// <summary>
    /// The MVC application.
    /// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    /// visit http://go.microsoft.com/?LinkId=9394801
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// The start of the MVC application.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
