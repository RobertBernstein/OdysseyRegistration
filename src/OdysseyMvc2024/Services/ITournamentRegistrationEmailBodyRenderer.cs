// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITournamentRegistrationEmailBodyRenderer.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines tournament registration email body rendering operations.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OdysseyMvc2024.ViewData.TournamentRegistration;

namespace OdysseyMvc2024.Services;

public interface ITournamentRegistrationEmailBodyRenderer
{
    Task<string> RenderAsync(
        ControllerContext controllerContext,
        ViewDataDictionary viewData,
        ITempDataDictionary tempData,
        Page10ViewData page10ViewData,
        CancellationToken cancellationToken);
}
