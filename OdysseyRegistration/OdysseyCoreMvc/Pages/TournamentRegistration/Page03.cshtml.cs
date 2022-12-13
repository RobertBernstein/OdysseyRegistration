// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page03.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page03Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using OdysseyCoreMvc.Data;
using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.Pages.TournamentRegistration
{
    /// <summary>
    /// The Tournament Registration Page03 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page03Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page03Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            CurrentRegistrationType = RegistrationType.Tournament;
        }

        /// <summary>
        /// Gets or sets a value indicating whether judge already taken.
        /// </summary>
        public bool JudgeAlreadyTaken { get; set; }

        /// <summary>
        /// Gets or sets the judge first name.
        /// </summary>
        [Display(Name = "Judge's First Name")]
        [StringLength(0x19, ErrorMessage = "The Judge's first name must not be more than 25 characters.")]
        [Required]
        public string? JudgeFirstName { get; set; }

        /// <summary>
        /// Gets or sets the judge id.
        /// </summary>
        [StringLength(4, ErrorMessage = "The Judge's ID must not be more than 3 digits.")]
        [Required]
        [Display(Name = "Judge's ID Number")]
        [Range(0, 0x7fffffff, ErrorMessage = "The Judge's ID must only contain numeric digits.")]
        public string? JudgeId { get; set; }

        /// <summary>
        /// Gets or sets the judge last name.
        /// </summary>
        [Required]
        [Display(Name = "Judge's Last Name")]
        [StringLength(0x19, ErrorMessage = "The Judge's last name must not be more than 25 characters.")]
        public string? JudgeLastName { get; set; }

        /// <summary>
        /// Gets or sets the list of judges found.
        /// </summary>
        public IQueryable<Judges>? ListOfJudgesFound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether no judges found.
        /// </summary>
        public bool NoJudgesFound { get; set; }
    }
}
