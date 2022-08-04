// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoachesTrainingRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The coaches training registration controller.
// </summary>
// <created>
//   October 27th, 2013
// </created>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    //using Elmah;

    using OdysseyCoreMvc.Models;
    using OdysseyCoreMvc.ViewData.CoachesTrainingRegistration;

    /// <summary>
    /// The coaches training registration controller.
    /// </summary>
    public class CoachesTrainingRegistrationController : BaseRegistrationController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoachesTrainingRegistrationController"/> class.
        /// </summary>
        public CoachesTrainingRegistrationController()
        {
            this.CurrentRegistrationType = RegistrationType.CoachesTraining;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

        /// <summary>
        /// The index.
        /// GET: /CoachesTrainingRegistration/
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
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            var viewData = new Page01ViewData
            {
                CoachesTrainingInfo = Repository.CoachesTrainingInfo,
                RoleList = new SelectList(Repository.Roles, "ID", "Name"),
                DivisionList = new SelectList(Repository.Divisions, "ID", "Name"),
                ProblemList = new SelectList(Repository.ProblemChoicesWithoutSpontaneous, "ProblemID", "ProblemName"),
                RegionList = new SelectList(Repository.Regions, "Name", "Name")
            };

            this.SetBaseViewData(viewData);

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page01.
        /// </summary>
        /// <param name="viewData">
        /// The view Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page01(Page01ViewData viewData)
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                ////this.UpdateModel(viewData);

                var registration = new CoachesTrainingRegistration
                {
                    FirstName = viewData.FirstName,
                    LastName = viewData.LastName,
                    SchoolName = viewData.SchoolName,
                    Role = viewData.SelectedRole,
                    Division = viewData.SelectedDivision,
                    SelectedProblem = viewData.SelectedProblem,
                    EmailAddress = viewData.EmailAddress,
                    YearsInvolved = viewData.YearsInvolved,
                    RegionNumber = viewData.SelectedRegion,
                    TimeRegistered = DateTime.Now,

                    // TODO: Test this!
                    UserAgent = /*Request.UserAgent*/ Request.Headers["User-Agent"].ToString()
                };

                // TODO: else case: Send an e-mail reporting database failure; could not create the record
                Repository.AddCoachesTrainingRegistration(registration);
                ////if (Repository.AddCoachesTrainingRegistration(registration) > 0)
                ////{
                ////    //Session["ID"] = registration.JudgeID;
                ////}

                return this.RedirectToAction("Page02", new { id = registration.RegistrationID });
            }
            catch (Exception ex)
            {
                // TODO: Re-add Elmah.
                //ErrorSignal.FromCurrentContext().Raise(ex);

                // TODO: Replace with Error Message
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
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            var viewData = new Page02ViewData
            {
                CoachesTrainingInfo = Repository.CoachesTrainingInfo,
            };

            this.SetBaseViewData(viewData);

            // Update the DateTime of the registration in the Judge record
            viewData.CoachesTraining = Repository.GetCoachesTrainingRegistrationById(id).FirstOrDefault();

            // This should NEVER happen
            if (viewData.CoachesTraining == null)
            {
                // Coaches Training record not found; return an error.
                viewData.ErrorMessage = "Your registration failed.  Please try the registration process over again.";
                return this.View(viewData);
            }

            viewData.MailBody = this.GenerateEmailBody(viewData);

            // Instantiate a new instance of MailMessage
            var mailMessage = BuildMessage(
                viewData.Config["WebmasterEmail"],
                viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " " + viewData.FriendlyRegistrationName,
                viewData.MailBody,
                viewData.CoachesTraining.EmailAddress,
                null,
                null);

            // Instantiate a new instance of SmtpClient
            viewData.MailErrorMessage = this.SendMessage(viewData, mailMessage);

            return this.View(viewData);
        }

        /// <summary>
        /// The generate email body.
        /// </summary>
        /// <param name="viewData">
        /// The view data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GenerateEmailBody(Page02ViewData viewData)
        {
            string mailBody = viewData.CoachesTrainingInfo.EventMailBody;
            mailBody = Regex.Replace(mailBody, "<span>Region</span>", "Region " + viewData.Config["RegionNumber"]);

            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationURL))
            {
                stringBuilder.Append("<a href=\"" + viewData.CoachesTrainingInfo.LocationURL + "\" target=\"_blank\">");
            }

            // Add the name of the Coaches Training location (as a link if a URL is present in the database).
            stringBuilder.Append(viewData.CoachesTrainingInfo.Location);

            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationAddress))
            {
                stringBuilder.Append(", " + viewData.CoachesTrainingInfo.LocationAddress);
            }

            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationCity))
            {
                stringBuilder.Append(", " + viewData.CoachesTrainingInfo.LocationCity);
            }

            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationState))
            {
                stringBuilder.Append(", " + viewData.CoachesTrainingInfo.LocationState);
            }

            // End the hyperlink code, if one is present in the database for Judges Training
            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationURL))
            {
                stringBuilder.Append("</a>");
            }

            mailBody = Regex.Replace(mailBody, "<span>Location</span>", stringBuilder.ToString());

            mailBody = Regex.Replace(
                mailBody,
                "<span>Date</span>",
                viewData.CoachesTrainingInfo.StartDate != null ? viewData.CoachesTrainingInfo.StartDate.Value.ToLongDateString() : "TBA");

            mailBody = Regex.Replace(
                mailBody,
                "<span>Time</span>",
                !string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.Time) ? viewData.CoachesTrainingInfo.Time : "TBA");

            mailBody = Regex.Replace(mailBody, "<span>Years</span>", viewData.Config["Year"] + " - " + viewData.Config["EndYear"]);
            
            mailBody = Regex.Replace(
                mailBody,
                "<span>ProgramGuide</span>",
                "<a href=\"" + viewData.Config["ProgramGuideURL"] + "\" target=\"_blank\">" + viewData.Config["ProgramGuideURL"] + "</a>");

            // Only include a reference to the Coaches Handbook from VA if a URL is specified in the database.
            stringBuilder.Clear();
            if (viewData.Config.ContainsKey("CoachesHandbookURL"))
            {
                if (!string.IsNullOrWhiteSpace(viewData.Config["CoachesHandbookURL"]))
                {
                    stringBuilder.Append(
                        "<li>\ta copy of the Coaches Handbook from the Virginia state website (<a href=\"" +
                        viewData.Config["CoachesHandbookURL"] + "\" target=\"blank\">" +
                        viewData.Config["CoachesHandbookURL"] + "</a>),</li>\n");
                }
            }

            mailBody = Regex.Replace(mailBody, "<span>VirginiaHandbook</span>", stringBuilder.ToString());
            mailBody = Regex.Replace(mailBody, "<span>Fee</span>", viewData.CoachesTrainingInfo.EventCost);
            mailBody = Regex.Replace(mailBody, "<span>MakeChecksOutTo</span>", viewData.CoachesTrainingInfo.EventMakeChecksOutTo);

            stringBuilder.Clear();
            if (viewData.Config.ContainsKey("CoordinatorsDoNotPayCoachesTrainingRegistrationFee"))
            {
                if (viewData.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"].ToLower() == "true")
                {
                    stringBuilder.Append(viewData.Config["SchoolCoordinatorsDoNotPayMessage"]);
                }
            }

            mailBody = Regex.Replace(mailBody, "<span>CoordinatorsDoNotPay</span>", stringBuilder.ToString());

            mailBody = Regex.Replace(
                mailBody,
                "<span>RegionalDirectorEmail</span>",
                "<a href=\"mailto:" + viewData.Config["RegionalDirectorEmail"] + "\">" + viewData.Config["RegionalDirectorEmail"] + "</a>");

            return mailBody;
        }
    }
}
