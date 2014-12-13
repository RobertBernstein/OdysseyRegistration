// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VolunteerRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the VolunteerRegistrationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Controllers
{
    using System;
    using System.Globalization;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using Elmah;

    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;
    using OdysseyMvc4.ViewData.VolunteerRegistration;

    /// <summary>
    /// The volunteer registration controller.
    /// </summary>
    public class VolunteerRegistrationController : BaseRegistrationController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VolunteerRegistrationController"/> class.
        /// </summary>
        public VolunteerRegistrationController()
        {
            this.CurrentRegistrationType = RegistrationType.Volunteer;
            this.FriendlyRegistrationName = this.GetFriendlyRegistrationName();
        }

        /// <summary>
        /// The build mail regional director hyper link.
        /// </summary>
        /// <param name="viewData">
        /// The view data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string BuildMailRegionalDirectorHyperLink(Page01ViewData viewData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("mailto:");
            sb.Append(viewData.Config["RegionalDirectorEmail"]);
            string mailString =
                ("?subject=I would like to help at the Region " + viewData.RegionNumber + " Tournament&body=I cannot be a volunteer this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A").Replace(" ", "%20");
            sb.Append(mailString);
            return sb.ToString();
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction("Page01");
        }

        /// <summary>
        /// Handle HTTP GET requests for Page01.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page01()
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page01ViewData viewData = new Page01ViewData();
            this.SetBaseViewData(viewData);

            viewData.MailRegionalDirectorHyperLink = this.BuildMailRegionalDirectorHyperLink(viewData);
            viewData.MailRegionalDirectorHyperLinkText = "send an e-mail to " + viewData.Config["RegionalDirectorText"];

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page01.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ActionName("Page01")]
        public ActionResult Page01Post()
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                Volunteer newVolunteer = new Volunteer
                                             {
                                                 TimeRegistrationStarted = DateTime.Now,
                                                 UserAgent = this.Request.UserAgent
                                             };

                // TODO: else case: Send an e-mail reporting database failure; could not create the record.
                this.Repository.AddVolunteer(newVolunteer);

                return this.RedirectToAction("Page02", new { id = newVolunteer.VolunteerID });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message.
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page02.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page02(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page02ViewData viewData = new Page02ViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page02.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="page02ViewData">
        /// The page 02 view data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    Volunteer newRegistrationData = new Volunteer
                                                        {
                                                            FirstName = page02ViewData.FirstName,
                                                            LastName = page02ViewData.LastName,
                                                            EveningPhone = page02ViewData.EveningPhone,
                                                            DaytimePhone = page02ViewData.DaytimePhone,
                                                            MobilePhone = page02ViewData.MobilePhone,
                                                            EmailAddress = page02ViewData.EmailAddress,
                                                            VolunteerWantsToSee = page02ViewData.VolunteerWantsToSee,
                                                            Notes = page02ViewData.Notes
                                                        };

                    // TODO: if case: Send an e-mail reporting database failure; could not find the record already added to the database
                    this.Repository.UpdateVolunteer(id, 2, newRegistrationData);
                    return this.RedirectToAction("Page03", new { id });
                }

                this.SetBaseViewData(page02ViewData);
                return this.View(page02ViewData);
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message.
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page03.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page03(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page03ViewData viewData = new Page03ViewData
            {
                VolunteerInfo = this.Repository.VolunteerInfo
            };

            this.SetBaseViewData(viewData);

            // Update the DateTime of the registration in the Volunteer record.
            viewData.Volunteer = this.Repository.GetVolunteerById(id);

            // This should NEVER happen!
            if (viewData.Volunteer == null)
            {
                // Volunteer not found; return error.
                viewData.ErrorMessage = "Your registration failed.  Please try the registration process over again.";
                return this.View(viewData);
            }

            this.Repository.UpdateVolunteer(id, 3, viewData.Volunteer);

            viewData.MailBody = this.GenerateEmailBody(viewData);
            
            if (!string.IsNullOrEmpty(viewData.Volunteer.EmailAddress) && (viewData.Volunteer.EmailAddress != "None"))
            {
                viewData.EmailAddressWasSpecified = true;

                // Instantiate a new instance of MailMessage.
                MailMessage mailMessage = this.BuildMessage(
                    viewData.Config["WebmasterEmail"],
                    viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " " + viewData.FriendlyRegistrationName,
                    viewData.MailBody,
                    viewData.Volunteer.EmailAddress,
                    null,
                    null);

                if (mailMessage == null)
                {
                    return this.RedirectToAction("BadEmail");
                }

                // Instantiate a new instance of SmtpClient.
                viewData.MailErrorMessage = this.SendMessage(viewData, mailMessage);
            }
            else
            {
                viewData.EmailAddressWasSpecified = false;
            }

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page03.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="submitButton">
        /// Only contains a value when resubmitting an e-mail address
        /// </param>
        /// <param name="homePageButton">
        /// The home page button.
        /// </param>
        /// <param name="nextButton">
        /// The next button.
        /// </param>
        /// <param name="restartRegistrationButton">
        /// The restart registration button.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page03(int id, string submitButton, string homePageButton, string nextButton, string restartRegistrationButton, FormCollection collection)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            if (!string.IsNullOrEmpty(restartRegistrationButton))
            {
                return this.RedirectToAction("Page01");
            }

            // User submitted a new e-mail address after mailing the previous one failed.
            if (!string.IsNullOrEmpty(submitButton))
            {
                // Update Volunteer e-mail in the database.
                this.Repository.UpdateVolunteerEmail(id, collection["NewEmailTextBox"]);
                return this.Page03(id);
            }

            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);

            return new RedirectResult(viewData.Config["HomePage"]);
        }

        /// <summary>
        /// The generate email body.
        /// </summary>
        /// <param name="page03ViewData">
        /// The page 03 view data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GenerateEmailBody(Page03ViewData page03ViewData)
        {
            string input = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(page03ViewData.VolunteerInfo.EventMailBody, "<span>VolunteerID</span>", page03ViewData.Volunteer.VolunteerID.ToString(CultureInfo.InvariantCulture)), "<span>FirstName</span>", page03ViewData.Volunteer.FirstName), "<span>LastName</span>", page03ViewData.Volunteer.LastName), "<span>Region</span>", "Region " + page03ViewData.Config["RegionNumber"]);
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationURL))
            {
                builder.Append("<a href=\"" + page03ViewData.TournamentInfo.LocationURL + "\" target=\"_blank\">");
            }

            builder.Append(page03ViewData.TournamentInfo.Location);
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationAddress))
            {
                builder.Append(", " + page03ViewData.TournamentInfo.LocationAddress);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationCity))
            {
                builder.Append(", " + page03ViewData.TournamentInfo.LocationCity);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationState))
            {
                builder.Append(", " + page03ViewData.TournamentInfo.LocationState);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationURL))
            {
                builder.Append("</a>");
            }

            return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(input, "<span>TournamentLocation</span>", builder.ToString()), "<span>TournamentDate</span>", page03ViewData.TournamentInfo.StartDate.HasValue ? page03ViewData.TournamentInfo.StartDate.Value.ToLongDateString() : "TBA"), "<span>TournamentTime</span>", !string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.Time) ? page03ViewData.TournamentInfo.Time : "TBA"), "<span>ContactUsURL</span>", page03ViewData.Config["HomePage"] + page03ViewData.Config["ContactUsURL"]);
        }
    }
}
