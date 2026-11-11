// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationEmailBodyRenderer.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Renders the tournament registration email body from the shared Razor partial view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using ElmahCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OdysseyMvc2024.ViewData.TournamentRegistration;

namespace OdysseyMvc2024.Services;

public sealed class TournamentRegistrationEmailBodyRenderer : ITournamentRegistrationEmailBodyRenderer
{
    private const string EmailPartialViewName = "TournamentRegistration/EmailPartial";

    public string Render(
        ControllerContext controllerContext,
        ViewDataDictionary viewData,
        ITempDataDictionary tempData,
        Page10ViewData page10ViewData)
    {
        ArgumentNullException.ThrowIfNull(controllerContext);
        ArgumentNullException.ThrowIfNull(viewData);
        ArgumentNullException.ThrowIfNull(tempData);
        ArgumentNullException.ThrowIfNull(page10ViewData);

        using StringWriter writer = new();

        if (controllerContext.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) is not ICompositeViewEngine viewEngine)
        {
            const string message = "The ICompositeViewEngine service is not available for tournament registration email rendering.";
            ElmahExtensions.RaiseError(new InvalidOperationException(message));
            throw new InvalidOperationException(message);
        }

        ViewEngineResult partialView = viewEngine.FindView(controllerContext, EmailPartialViewName, false);
        if (!partialView.Success || partialView.View is null)
        {
            string searchedLocations = string.Join(", ", partialView.SearchedLocations ?? []);
            string message = string.IsNullOrWhiteSpace(searchedLocations)
                ? "Could not locate view 'TournamentRegistration/EmailPartial' when generating tournament registration email body."
                : $"Could not locate view 'TournamentRegistration/EmailPartial' when generating tournament registration email body. Searched: {searchedLocations}";
            ElmahExtensions.RaiseError(new InvalidOperationException(message));
            throw new InvalidOperationException(message);
        }

        ViewDataDictionary<Page10ViewData> partialViewData = new(viewData, page10ViewData);
        ViewContext viewContext = new(controllerContext, partialView.View, partialViewData, tempData, writer, new HtmlHelperOptions());
        partialView.View.RenderAsync(viewContext).GetAwaiter().GetResult();

        return writer.GetStringBuilder().ToString();
    }
}
