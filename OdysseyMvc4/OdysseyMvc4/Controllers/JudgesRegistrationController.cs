// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JudgesRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2013 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the JudgesRegistrationController type.
// </summary>
// <created>
//   October 27th, 2013
// </created>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    using Elmah;

    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;
    using OdysseyMvc4.ViewData.JudgesRegistration;

    /// <summary>
    /// The judges registration controller.
    /// </summary>
    public class JudgesRegistrationController : BaseRegistrationController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JudgesRegistrationController"/> class.
        /// </summary>
        public JudgesRegistrationController()
        {
            this.CurrentRegistrationType = RegistrationType.Judges;
            this.FriendlyRegistrationName = this.GetFriendlyRegistrationName();
        }

        /// <summary>
        /// The index.
        /// GET: /JudgesRegistration/
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
        /// The explanations.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Explanations()
        {
            var viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
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
                JudgesInfo = Repository.JudgesInfo,
                TournamentInfo = Repository.TournamentInfo
            };

            this.SetBaseViewData(viewData);
            viewData.MailRegionalDirectorHyperLink = this.BuildMailRegionalDirectorHyperLink(viewData);
            viewData.MailRegionalDirectorHyperLinkText = "send an e-mail to " + viewData.Config["RegionalDirectorText"];

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
                ////var viewData = new Page01ViewData();
                ////this.UpdateModel(viewData);

                var registration = new Judge
                {
                    TimeRegistrationStarted = DateTime.Now,
                    UserAgent = Request.UserAgent
                };

                // TODO: else case: Send an e-mail reporting database failure; could not create the record
                Repository.AddJudge(registration);
                ////if (Repository.AddJudge(registration) > 0)
                ////{
                ////    //Session["ID"] = registration.JudgeID;
                ////}

                return this.RedirectToAction("Page02", new { id = registration.JudgeID });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);

                // TODO: Replace with Error Message
                return this.RedirectToAction("Index", "Home");
            }
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
            var sb = new StringBuilder();
            sb.Append("mailto:");
            sb.Append(viewData.Config["RegionalDirectorEmail"]);
            string mailString =
                "?subject=I would like to help at the Region " +
                viewData.RegionNumber +
                " Tournament&body=I cannot be a judge this year, but would like to help in some other way.%0A%0A" +
                "My name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A";
            mailString = mailString.Replace(" ", "%20");
            sb.Append(mailString);
            return sb.ToString();
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
                JudgesInfo = Repository.JudgesInfo,
                TshirtSizes =
                    new SelectList(
                    new[] { "S", "M", "L", "XL", "XXL", "XXXL" }.Select(x => new { value = x, text = x }),
                    "value",
                    "text"),
                ProblemChoices = new SelectList(Repository.ProblemChoices, "ProblemID", "ProblemName"),
            };

            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page02.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="nextButton">
        /// The next Button.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page02(int id, /*String previousButton,*/ string nextButton, FormCollection collection)
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                var viewData = new Page02ViewData();
                this.UpdateModel(viewData);

                viewData.Judge.TshirtSize = collection["tshirtSize"];
                viewData.Judge.ProblemChoice1 = collection["ProblemChoice1"];
                viewData.Judge.ProblemChoice2 = collection["ProblemChoice2"];
                viewData.Judge.ProblemChoice3 = collection["ProblemChoice3"];
                viewData.Judge.ProblemCOI1 = collection["ProblemCOI1"];
                viewData.Judge.ProblemCOI2 = collection["ProblemCOI2"];
                viewData.Judge.ProblemCOI3 = collection["ProblemCOI3"];
                viewData.Judge.CEU = collection["ceuRadioGroup"];
                viewData.Judge.PreviousPositions = GetPreviousPositions(collection);

                //// If the judge did not provide an e-mail address, make sure "None" is written to the database
                ////if (String.IsNullOrEmpty(viewData.Judge.JEmail1))
                ////{
                ////    viewData.Judge.JEmail1 = "None";
                ////}

                // TODO: if case: Send an e-mail reporting database failure; could not find the record already added to the database
                Repository.UpdateJudge(id, 2, viewData.Judge);

                // Display debugging information.
                ////Response.Write("<p>Head Judge: " + collection["PreviouslyHeadJudge"] + "</p>");
                ////Response.Write("<p>Problem Judge: " + collection["PreviouslyProblemJudge"] + "</p>");
                ////Response.Write("<p>Style Judge: " + collection["PreviouslyStyleJudge"] + "</p>");
                ////Response.Write("<p>Staging Judge: " + collection["PreviouslyStagingJudge"] + "</p>");
                ////Response.Write("<p>Timekeeper: " + collection["PreviouslyTimekeeper"] + "</p>");
                ////Response.Write("<p>Scorechecker: " + collection["PreviouslyScorechecker"] + "</p>");
                ////Response.Write("<p>WeighIn Judge: " + collection["PreviouslyWeighInJudge"] + "</p>");
                ////Response.Write("Previous Positions: " + viewData.Judge.PreviousPositions);
                ////return null;

                return this.RedirectToAction("Page03", new { id });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);

                // TODO: Replace with Error Message
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
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            var viewData = new Page03ViewData
            {
                JudgesInfo = Repository.JudgesInfo,
                TournamentInfo = Repository.TournamentInfo
            };
            this.SetBaseViewData(viewData);

            // Update the DateTime of the registration in the Judge record
            viewData.Judge = Repository.GetJudgeById(id).FirstOrDefault();

            // This should NEVER happen!
            if (viewData.Judge == null)
            {
                // Judge not found; return error
                viewData.ErrorMessge = "Your registration failed.  Please try the registration process over again.";
                return this.View(viewData);
            }

            Repository.UpdateJudge(id, 3, viewData.Judge);

            viewData.MailBody = this.GenerateEmailBody(viewData);

            if (!string.IsNullOrWhiteSpace(viewData.Judge.EmailAddress) && viewData.Judge.EmailAddress != "None")
            {
                viewData.EmailAddressWasSpecified = true;

                // Instantiate a new instance of MailMessage
                var mailMessage = BuildMessage(
                    viewData.Config["WebmasterEmail"],
                    viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " Judges Registration",
                    viewData.MailBody,
                    viewData.Judge.EmailAddress,
                    null,
                    null);

                if (mailMessage == null)
                {
                    return this.RedirectToAction("BadEmail");
                }

                // Instantiate a new instance of SmtpClient
                viewData.MailErrorMessage = this.SendMessage(viewData, mailMessage);
            }
            else
            {
                viewData.EmailAddressWasSpecified = false;
            }

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP GET requests for the BadEmail page.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult BadEmail()
        {
            return this.View();
        }

        /// <summary>
        /// Handle HTTP POST requests for Page03.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="submitButton">
        /// Only contains a value when resubmitting an e-mail address.
        /// </param>
        /// <param name="homePageButton">
        /// The home Page Button.
        /// </param>
        /// <param name="nextButton">
        /// The next Button.
        /// </param>
        /// <param name="restartRegistrationButton">
        /// The restart Registration Button.
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
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            if (!string.IsNullOrEmpty(restartRegistrationButton))
            {
                return this.RedirectToAction("Page01");
            }

            // User submitted a new e-mail address after mailing the previous one failed
            if (!string.IsNullOrEmpty(submitButton))
            {
                // Update Judge e-mail in the database
                Repository.UpdateJudgeEmail(id, collection["NewEmailTextBox"]);
                return this.Page03(id);
            }

            var viewData = new BaseViewData();
            this.SetBaseViewData(viewData);

            return new RedirectResult(viewData.Config["HomePage"]);
            ////return RedirectToAction("Index", "Home");
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
        protected string GenerateEmailBody(Page03ViewData viewData)
        {
            StringBuilder mailBody = new StringBuilder();

            mailBody.Append("<div id=\"contentstart\"><p style=\"text-align: center\"><b>You have been assigned Judge ID ");
            mailBody.Append(viewData.Judge.JudgeID);
            mailBody.Append("</b></p>\n\n");
            mailBody.Append("<p><b>Congratulations, your registration is complete.&nbsp; Please print this page for your records.</b></p>\n\n");
            mailBody.Append("<p>Our system has registered you with the following information: \n\n");
            mailBody.Append("<ul><li>Judge ID:   ");
            mailBody.Append(viewData.Judge.JudgeID);
            mailBody.Append("</li>\n");
            mailBody.Append("<li>First Name: ");
            mailBody.Append(viewData.Judge.FirstName);
            mailBody.Append("</li>\n");
            mailBody.Append("<li>Last Name:  ");
            mailBody.Append(viewData.Judge.LastName);
            mailBody.Append("</li>\n\n");
            mailBody.Append("</ul><p>Should a coach request that you be a judge for their team, please provide them with all of this information.&nbsp; \n");
            mailBody.Append("Please do not give out your number to more than one team since you may only judge on behalf of <b>ONE</b> team.</p>\n\n");
            mailBody.Append("<p>As a reminder, you have agreed to attend the following two events:</p>\n");
            mailBody.Append("<ul>\n<li><b>Region ");
            mailBody.Append(viewData.RegionNumber);
            mailBody.Append(" Odyssey of the Mind Judges Training</b>\n<br /><br /><ul>\n<li> <b>Location:</b> ");

            // Begin the hyperlink code, if one is present in the database for Judges Training
            if (!string.IsNullOrEmpty(viewData.JudgesInfo.LocationURL))
            {
                mailBody.Append("<a href=\"");
                mailBody.Append(viewData.JudgesInfo.LocationURL);
                mailBody.Append("\" target=\"_blank\">");
            }

            // Display the name of the Judges Training location (as a link if a URL is present in the database)
            mailBody.Append(viewData.JudgesInfo.Location);

            // End the hyperlink code, if one is present in the database for Judges Training
            if (!string.IsNullOrEmpty(viewData.JudgesInfo.LocationURL))
            {
                mailBody.Append("</a>");
            }

            mailBody.Append("</li>\n<li> <b>Date:</b> ");

            if (viewData.JudgesInfo.StartDate != null)
            {
                mailBody.Append(viewData.JudgesInfo.StartDate.Value.ToLongDateString());
            }
            else
            {
                mailBody.Append("TBA");
            }

            mailBody.Append("</li>\n<li> <b>Time:</b> ");

            if (!string.IsNullOrEmpty(viewData.JudgesInfo.Time))
            {
                mailBody.Append(viewData.JudgesInfo.Time);
            }
            else
            {
                mailBody.Append("TBA");
            }

            mailBody.Append("</li>\n</ul>\n</li>\n</ul>\n");

            mailBody.Append("<ul>\n<li><b>Region ");
            mailBody.Append(viewData.RegionNumber);
            mailBody.Append(" Odyssey of the Mind Tournament</b>\n<br /><br /><ul>\n<li> <b>Location:</b> ");

            // Begin the hyperlink code, if one is present in the database for the Regional Tournament
            if (!string.IsNullOrEmpty(viewData.TournamentInfo.LocationURL))
            {
                mailBody.Append("<a href=\"");
                mailBody.Append(viewData.TournamentInfo.LocationURL);
                mailBody.Append("\" target=\"_blank\">");
            }

            // Display the name of the Regional Tournament location (as a link if a URL is present in the database)
            mailBody.Append(viewData.TournamentInfo.Location);

            if (!string.IsNullOrEmpty(viewData.TournamentInfo.LocationURL))
            {
                // End the hyperlink code, if one is present in the database for the Regional Tournament
                mailBody.Append("</a>");
            }

            mailBody.Append("</li>\n<li> <b>Date:</b> ");

            if (viewData.TournamentInfo.StartDate != null)
            {
                mailBody.Append(viewData.TournamentInfo.StartDate.Value.ToLongDateString());
            }
            else
            {
                mailBody.Append("TBA");
            }

            mailBody.Append("</li>\n<li> <b>Time:</b> ");

            if (!string.IsNullOrEmpty(viewData.TournamentInfo.Time))
            {
                mailBody.Append(viewData.TournamentInfo.Time);
            }
            else
            {
                mailBody.Append("TBA");
            }

            mailBody.Append("</li>\n</ul>\n</li>\n</ul>\n");

            mailBody.Append("<p>Towards ");
            mailBody.Append(viewData.Config["JudgePacketTimeframe"]);
            mailBody.Append(", you will receive information about Judges ");
            mailBody.Append("Training  which will include the problem you have been assigned to and any other information that you will need.</p>\n\n");
            mailBody.Append("<p>We will serve breakfast on the morning of Judges Training, including coffee and juice. &nbsp;Breakfast typically ");
            mailBody.Append("consists of bagels and muffins. &nbsp;<span style=\"font-weight: bold;\">You will need to bring a packed lunch to Judges Training.</span></p>\n\n");
            mailBody.Append("<p>If you have any questions or find you cannot attend either judges training or the tournament, please use our ");
            mailBody.Append("<a href=\"http://www.novanorth.org/wp/?page_id=129\" target=\"_blank\">Contact Us</a> page ");
            mailBody.Append("to reach the Judges Coordinator.</p>\n\n");

            mailBody.Append("<p>If you agreed to judge on behalf of a team and find that you cannot ");
            mailBody.Append("attend either Judges Training or the tournament, you must contact the ");
            mailBody.Append("coach and advise him/her so that the team knows they need to find another judge.</p>\n\n");

            mailBody.Append("<p align=\"center\"><b>You have been assigned Judge ID ");
            mailBody.Append(viewData.Judge.JudgeID);
            mailBody.Append("</b></p>\n\n");

            return mailBody.ToString();
        }

        /// <summary>
        /// Concatenate all of the "Previous Positions Held" checked box values on Judges
        /// Registration Page 2.
        /// </summary>
        /// <param name="collection">
        /// The parameters passed in from Page02.
        /// </param>
        /// <returns>
        /// A concatenated, semicolon-separated string of previously-held positions.
        /// </returns>
        private static string GetPreviousPositions(NameValueCollection collection)
        {
            StringBuilder previousPositions = new StringBuilder();

            if (collection.AllKeys.Contains("PreviouslyHeadJudge"))
            {
                if (collection["PreviouslyHeadJudge"].ToLower().Contains("true"))
                {
                    previousPositions.Append("Head Judge");
                }
            }

            if (collection.AllKeys.Contains("PreviouslyProblemJudge"))
            {
                if (collection["PreviouslyProblemJudge"].ToLower().Contains("true"))
                {
                    previousPositions.Append(";Problem Judge");
                }
            }

            if (collection.AllKeys.Contains("PreviouslyStyleJudge"))
            {
                if (collection["PreviouslyStyleJudge"].ToLower().Contains("true"))
                {
                    previousPositions.Append(";Style Judge");
                }
            }

            if (collection.AllKeys.Contains("PreviouslyStagingJudge"))
            {
                if (collection["PreviouslyStagingJudge"].ToLower().Contains("true"))
                {
                    previousPositions.Append(";Staging Judge");
                }
            }

            if (collection.AllKeys.Contains("PreviouslyTimekeeper"))
            {
                if (collection["PreviouslyTimekeeper"].ToLower().Contains("true"))
                {
                    previousPositions.Append(";Timekeeper");
                }
            }

            if (collection.AllKeys.Contains("PreviouslyScorechecker"))
            {
                if (collection["PreviouslyScorechecker"].ToLower().Contains("true"))
                {
                    previousPositions.Append(";Scorechecker");
                }
            }

            if (collection.AllKeys.Contains("PreviouslyWeighInJudge"))
            {
                if (collection["PreviouslyWeighInJudge"].ToLower().Contains("true"))
                {
                    previousPositions.Append(";Weigh-In Judge");
                }
            }

            // If the first checked item wasn't "Head Judge", trim the leading ';'.
            if (previousPositions.Length > 0)
            {
                if (previousPositions[0] == ';')
                {
                    previousPositions.Remove(0, 1);
                }
            }
            else
            {
                // If the StringBuilder was empty after processing, return null so a NULL
                // is written to SQL Server.
                return null;
            }

            return previousPositions.ToString().Trim();
        }
    }
}
