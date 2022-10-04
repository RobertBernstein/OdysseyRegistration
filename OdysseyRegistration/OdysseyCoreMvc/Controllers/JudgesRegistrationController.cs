// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JudgesRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the JudgesRegistrationController type.
// </summary>
// <created>
//   October 27th, 2013
// </created>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//using Elmah;

using OdysseyCoreMvc.Models;
using OdysseyCoreMvc.ViewData;
using OdysseyCoreMvc.ViewData.JudgesRegistration;

namespace OdysseyCoreMvc.Controllers
{
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
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

        public string BuildMailRegionalDirectorHyperLink(Page01ViewData viewData)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("mailto:");
            builder.Append(viewData.Config["RegionalDirectorEmail"]);
            string mailString =
                ("?subject=I would like to help at the Region " +
                viewData.RegionNumber +
                " Tournament&body=I cannot be a judge this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A").Replace(" ", "%20");
            builder.Append(mailString);
            return builder.ToString();
        }

        protected string GenerateEmailBody(Page03ViewData page03ViewData)
        {
            string input = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(page03ViewData.JudgesInfo.EventMailBody, "<span>JudgeID</span>", page03ViewData.Judge.JudgeId.ToString(CultureInfo.InvariantCulture)), "<span>FirstName</span>", page03ViewData.Judge.FirstName), "<span>LastName</span>", page03ViewData.Judge.LastName), "<span>Region</span>", "Region " + page03ViewData.Config["RegionNumber"]);
            StringBuilder mailBody = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationUrl))
            {
                mailBody.Append("<a href=\"" + page03ViewData.JudgesInfo.LocationUrl + "\" target=\"_blank\">");
            }

            mailBody.Append(page03ViewData.JudgesInfo.Location);
            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationAddress))
            {
                mailBody.Append(", " + page03ViewData.JudgesInfo.LocationAddress);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationCity))
            {
                mailBody.Append(", " + page03ViewData.JudgesInfo.LocationCity);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationState))
            {
                mailBody.Append(", " + page03ViewData.JudgesInfo.LocationState);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationUrl))
            {
                mailBody.Append("</a>");
            }

            input = Regex.Replace(Regex.Replace(Regex.Replace(input, "<span>JudgesTrainingLocation</span>", mailBody.ToString()), "<span>JudgesTrainingDate</span>", page03ViewData.JudgesInfo.StartDate.HasValue ? page03ViewData.JudgesInfo.StartDate.Value.ToLongDateString() : "TBA"), "<span>JudgesTrainingTime</span>", !string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.Time) ? page03ViewData.JudgesInfo.Time : "TBA");
            mailBody.Clear();
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationUrl))
            {
                mailBody.Append("<a href=\"" + page03ViewData.TournamentInfo.LocationUrl + "\" target=\"_blank\">");
            }

            mailBody.Append(page03ViewData.TournamentInfo.Location);
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationAddress))
            {
                mailBody.Append(", " + page03ViewData.TournamentInfo.LocationAddress);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationCity))
            {
                mailBody.Append(", " + page03ViewData.TournamentInfo.LocationCity);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationState))
            {
                mailBody.Append(", " + page03ViewData.TournamentInfo.LocationState);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationUrl))
            {
                mailBody.Append("</a>");
            }

            return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(input, "<span>TournamentLocation</span>", mailBody.ToString()), "<span>TournamentDate</span>", page03ViewData.TournamentInfo.StartDate.HasValue ? page03ViewData.TournamentInfo.StartDate.Value.ToLongDateString() : "TBA"), "<span>TournamentTime</span>", !string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.Time) ? page03ViewData.TournamentInfo.Time : "TBA"), "<span>ContactUsURL</span>", page03ViewData.Config["HomePage"] + page03ViewData.Config["ContactUsURL"]);
        }

        /// <summary>
        /// Concatenate all of the "Previous Positions Held" checked box values on Judges
        /// Registration Page 2.
        /// </summary>
        /// <param name="page02ViewData">
        /// The Page02 View Data.
        /// </param>
        /// <returns>
        /// A concatenated, semicolon-separated string of previously-held positions.
        /// </returns>
        private static string GetPreviousPositions(Page02ViewData page02ViewData)
        {
            StringBuilder previousPositions = new StringBuilder();
            if (page02ViewData.PreviouslyHeadJudge)
            {
                previousPositions.Append("Head Judge");
            }

            if (page02ViewData.PreviouslyProblemJudge)
            {
                previousPositions.Append(";Problem Judge");
            }

            if (page02ViewData.PreviouslyStyleJudge)
            {
                previousPositions.Append(";Style Judge");
            }

            if (page02ViewData.PreviouslyStagingJudge)
            {
                previousPositions.Append(";Staging Judge");
            }

            if (page02ViewData.PreviouslyTimekeeper)
            {
                previousPositions.Append(";Timekeeper");
            }

            if (page02ViewData.PreviouslyScorechecker)
            {
                previousPositions.Append(";Scorechecker");
            }

            if (page02ViewData.PreviouslyWeighInJudge)
            {
                previousPositions.Append(";Weigh-In Judge");
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

        private void InitializePage02ViewData(Page02ViewData page02ViewData)
        {
            page02ViewData.TshirtSizes = new SelectList(from x in new[] { "S", "M", "L", "XL", "XXL", "XXXL" } select new { value = x, text = x }, "value", "text");
            page02ViewData.ProblemChoices = new SelectList(this.Repository.ProblemChoices, "ProblemID", "ProblemName");
            this.SetBaseViewData(page02ViewData);
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

            Page01ViewData viewData = new Page01ViewData
            {
                JudgesInfo = this.Repository.JudgesInfo
            };

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
        [HttpPost, ActionName("Page01")]
        public ActionResult Page01Post()
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                Judges newJudge = new Judges
                {
                    TimeRegistrationStarted = DateTime.Now,
                    UserAgent = /*this.Request.UserAgent*/ Request.Headers["User-Agent"].ToString()
                };

                // TODO: else case: Send an e-mail reporting database failure; could not create the record

                this.Repository.AddJudge(newJudge);
                return this.RedirectToAction("Page02", new { id = newJudge.JudgeId });
            }
            catch (Exception exception)
            {
                // TODO: Re-add Elmah.
                //ErrorSignal.FromCurrentContext().Raise(exception);

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

            Page02ViewData data = new Page02ViewData();
            this.InitializePage02ViewData(data);
            return this.View(data);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page02.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="page02ViewData">
        /// The Page02 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }
            try
            {
                // TODO: What should we do here if the ModelState isn't valid? - Rob, 09/30/2014
                if (this.ModelState.IsValid)
                {
                    Judges newJudgeData = new Judges
                    {
                        FirstName = page02ViewData.FirstName,
                        LastName = page02ViewData.LastName,
                        Address = page02ViewData.Address,
                        AddressLine2 = page02ViewData.AddressLine2,
                        City = page02ViewData.City,
                        State = page02ViewData.State,
                        ZipCode = page02ViewData.ZipCode,
                        EveningPhone = page02ViewData.EveningPhone,
                        DaytimePhone = page02ViewData.DaytimePhone,
                        MobilePhone = page02ViewData.MobilePhone,
                        EmailAddress = page02ViewData.EmailAddress,
                        ProblemChoice1 = page02ViewData.ProblemChoice1,
                        ProblemChoice2 = page02ViewData.ProblemChoice2,
                        ProblemChoice3 = page02ViewData.ProblemChoice3,
                        HasChildrenCompeting = page02ViewData.HasChildrenCompeting,
                        ProblemCoi1 = page02ViewData.ProblemConflict1,
                        ProblemCoi2 = page02ViewData.ProblemConflict2,
                        ProblemCoi3 = page02ViewData.ProblemConflict3,
                        YearsOfLongTermJudgingExperience = page02ViewData.YearsOfLongTermJudgingExperience,
                        YearsOfSpontaneousJudgingExperience = page02ViewData.YearsOfSpontaneousJudgingExperience,
                        PreviousPositions = GetPreviousPositions(page02ViewData),
                        WillingToBeScorechecker = page02ViewData.WillingToBeScorechecker,
                        TshirtSize = page02ViewData.TshirtSize,
                        WantsCeucredit = page02ViewData.WantsCeuCredit,
                        Notes = page02ViewData.Notes
                    };

                    // If the judge did not provide an e-mail address, make sure "None" is written to the database
                    if (string.IsNullOrWhiteSpace(newJudgeData.EmailAddress))
                    {
                        newJudgeData.EmailAddress = "None";
                    }

                    // TODO: if case: Send an e-mail reporting database failure; could not find the record already added to the database
                    this.Repository.UpdateJudge(id, 2, newJudgeData);

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

                this.InitializePage02ViewData(page02ViewData);
                
                return this.View(page02ViewData);
            }
            catch (Exception exception)
            {
                // TODO: Re-add Elmah.
                //ErrorSignal.FromCurrentContext().Raise(exception);

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

            Page03ViewData viewData = new Page03ViewData
            {
                JudgesInfo = this.Repository.JudgesInfo
            };

            this.SetBaseViewData(viewData);

            // Update the DateTime of the registration in the Judge record
            viewData.Judge = this.Repository.GetJudgeById(id).FirstOrDefault<Judges>();

            // This should NEVER happen!
            if (viewData.Judge == null)
            {
                // Judge not found; return error
                viewData.ErrorMessage = "Your registration failed.  Please try the registration process over again.";
                return this.View(viewData);
            }

            this.Repository.UpdateJudge(id, 3, viewData.Judge);

            viewData.MailBody = this.GenerateEmailBody(viewData);

            if (!string.IsNullOrWhiteSpace(viewData.Judge.EmailAddress) && (viewData.Judge.EmailAddress != "None"))
            {
                viewData.EmailAddressWasSpecified = true;
                MailMessage mailMessage = this.BuildMessage(viewData.Config["WebmasterEmail"], viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " " + viewData.FriendlyRegistrationName, viewData.MailBody, viewData.Judge.EmailAddress, null, null);
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
                this.Repository.UpdateJudgeEmail(id, collection["NewEmailTextBox"]);
                return this.Page03(id);
            }

            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);

            return new RedirectResult(viewData.Config["HomePage"]);
            ////return RedirectToAction("Index", "Home");
        }
    }
}
