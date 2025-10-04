// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JudgesRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2025 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Controller for Judges Registration workflow and related actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.JudgesRegistrationController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using OdysseyMvc2024.ViewData;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.JudgesRegistration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElmahCore;

namespace OdysseyMvc2024.Controllers
{
    public partial class JudgesRegistrationController : BaseRegistrationController
    {
        public JudgesRegistrationController(IOdysseyEntities context)
            : base(context)
        {
            CurrentRegistrationType = RegistrationType.Judges;
            FriendlyRegistrationName = GetFriendlyRegistrationName();
        }

        public string BuildMailRegionalDirectorHyperLink(Page01ViewData viewData)
        {
            if (viewData == null)
            {
                throw new ArgumentNullException(nameof(viewData), "viewData is null.");
            }

            if (viewData.Config == null)
            {
                throw new ArgumentNullException(nameof(viewData), "Config dictionary is null.");
            }

            if (!viewData.Config.TryGetValue("RegionalDirectorEmail", out var regionalDirectorEmail))
            {
                throw new ArgumentException("RegionalDirectorEmail key is missing in the Config dictionary.", nameof(viewData));
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("mailto:");
            stringBuilder.Append(regionalDirectorEmail);
            string subject = $"?subject=I would like to help at the Region {viewData.RegionNumber} Tournament";
            string body = "&body=I cannot be a judge this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A";

            // TODO: (05/26/2025) Test that this works correctly with the new URL encoding.
            stringBuilder.Append(subject.Replace(" ", "%20"));

            stringBuilder.Append(body);
            return stringBuilder.ToString();
        }

        // TODO: (05/26/2025) Test that this works correctly. You may want to revert to the previous code if it does not.
        // Fix for SYSLIB1045: Use 'GeneratedRegexAttribute' to generate the regular expression implementation at compile-time.
        // Replace all instances of Regex.Replace with precompiled regex using the GeneratedRegexAttribute.
        [GeneratedRegex("<span>JudgeID</span>")]
        private static partial Regex JudgeIdRegex();

        [GeneratedRegex("<span>FirstName</span>")]
        private static partial Regex FirstNameRegex();

        [GeneratedRegex("<span>LastName</span>")]
        private static partial Regex LastNameRegex();

        [GeneratedRegex("<span>Region</span>")]
        private static partial Regex RegionRegex();

        [GeneratedRegex("<span>JudgesTrainingLocation</span>")]
        private static partial Regex JudgesTrainingLocationRegex();

        [GeneratedRegex("<span>JudgesTrainingDate</span>")]
        private static partial Regex JudgesTrainingDateRegex();

        [GeneratedRegex("<span>JudgesTrainingTime</span>")]
        private static partial Regex JudgesTrainingTimeRegex();

        [GeneratedRegex("<span>TournamentLocation</span>")]
        private static partial Regex TournamentLocationRegex();

        [GeneratedRegex("<span>TournamentDate</span>")]
        private static partial Regex TournamentDateRegex();

        [GeneratedRegex("<span>TournamentTime</span>")]
        private static partial Regex TournamentTimeRegex();

        [GeneratedRegex("<span>ContactUsURL</span>")]
        private static partial Regex ContactUsUrlRegex();

        protected string GenerateEmailBody(Page03ViewData page03ViewData)
        {
            string input1 = JudgeIdRegex().Replace(
                FirstNameRegex().Replace(
                    LastNameRegex().Replace(
                        RegionRegex().Replace(
                            page03ViewData.JudgesInfo.EventMailBody,
                            page03ViewData.Judge.JudgeID.ToString(CultureInfo.InvariantCulture)
                        ),
                        page03ViewData.Judge.FirstName
                    ),
                    page03ViewData.Judge.LastName
                ),
                "Region " + page03ViewData.Config["RegionNumber"]
            );

            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationURL))
            {
                stringBuilder.Append("<a href=\"" + page03ViewData.JudgesInfo.LocationURL + "\" target=\"_blank\">");
            }

            stringBuilder.Append(page03ViewData.JudgesInfo.Location);

            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationAddress))
            {
                stringBuilder.Append(", " + page03ViewData.JudgesInfo.LocationAddress);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationCity))
            {
                stringBuilder.Append(", " + page03ViewData.JudgesInfo.LocationCity);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationState))
            {
                stringBuilder.Append(", " + page03ViewData.JudgesInfo.LocationState);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationURL))
            {
                stringBuilder.Append("</a>");
            }

            // TODO: (05/26/2025) Test that this works correctly. You may want to revert to the previous code if it does not.
            string input2 = JudgesTrainingLocationRegex().Replace(input1, stringBuilder.ToString());

            string replacement1 = page03ViewData.JudgesInfo.StartDate.HasValue
                ? page03ViewData.JudgesInfo.StartDate.Value.ToLongDateString()
                : "TBA";

            string input3 = JudgesTrainingDateRegex().Replace(
                JudgesTrainingTimeRegex().Replace(input2, !string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.Time) ? page03ViewData.JudgesInfo.Time : "TBA"),
                replacement1
            );

            stringBuilder.Clear();

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationURL))
            {
                stringBuilder.Append("<a href=\"" + page03ViewData.TournamentInfo.LocationURL + "\" target=\"_blank\">");
            }

            stringBuilder.Append(page03ViewData.TournamentInfo.Location);

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationAddress))
            {
                stringBuilder.Append(", " + page03ViewData.TournamentInfo.LocationAddress);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationCity))
            {
                stringBuilder.Append(", " + page03ViewData.TournamentInfo.LocationCity);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationState))
            {
                stringBuilder.Append(", " + page03ViewData.TournamentInfo.LocationState);
            }

            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationURL))
            {
                stringBuilder.Append("</a>");
            }

            string input4 = Regex.Replace(input3, "<span>TournamentLocation</span>", stringBuilder.ToString());

            var startDate = page03ViewData.TournamentInfo.StartDate;

            string replacement2;
            if (!startDate.HasValue)
            {
                replacement2 = "TBA";
            }
            else
            {
                startDate = page03ViewData.TournamentInfo.StartDate;
                replacement2 = startDate.Value.ToLongDateString();
            }

            // TODO: Generate a compiled regex for this.
            return Regex.Replace(Regex.Replace(Regex.Replace(input4, "<span>TournamentDate</span>", replacement2), "<span>TournamentTime</span>", !string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.Time) ? page03ViewData.TournamentInfo.Time : "TBA"), "<span>ContactUsURL</span>", page03ViewData.Config["HomePage"] + page03ViewData.Config["ContactUsURL"]);
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
            // TODO: Add curly braces to all of this.
            StringBuilder previousPositions = new();
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
            if (previousPositions.Length <= 0)
            {
                // If the StringBuilder was empty after processing, return null so a NULL
                // is written to SQL Server.
                return null;
            }

            if (previousPositions[0] == ';')
            {
                previousPositions.Remove(0, 1);
            }

            return previousPositions.ToString().Trim();
        }

        /// <summary>
        /// Handles HTTP GET requests and redirects the user to the "Page01" action, i.e., the index page.
        /// GET: /JudgesRegistration/
        /// </summary>
        /// <returns>
        /// A <see cref="RedirectToActionResult"/> that redirects the user to the "Page01" action.
        /// </returns>
        [HttpGet]
        public ActionResult Index() => RedirectToAction("Page01");

        private void InitializePage02ViewData(Page02ViewData page02ViewData)
        {
            // TODO: (06/20/2025) Test that this works correctly. You may want to revert to the OdysseyMvc4 code if it does not.
            page02ViewData.TshirtSizes = (IEnumerable<SelectListItem>)new SelectList(Enumerable.Select((IEnumerable<string>)new string[6] { "S", "M", "L", "XL", "XXL", "XXXL" }, x => { var data = new { value = x, text = x }; return data; }), "value", "text");
            page02ViewData.ProblemChoices = (IEnumerable<SelectListItem>)new SelectList(Repository.ProblemChoices, "ProblemID", "ProblemName");
            SetBaseViewData(page02ViewData);
        }

        /// <summary>
        /// Handles HTTP GET requests for the first page of the Judges Registration process.
        /// </summary>
        /// <returns>
        /// A <see cref="ActionResult"/> for the first page.
        /// </returns>
        [HttpGet]
        public ActionResult Page01()
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page01ViewData page01ViewData = new Page01ViewData(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                JudgesInfo = Repository.JudgesInfo,

                // TODO: Do I need to set these to actual values here?
                MailRegionalDirectorHyperLink = string.Empty,
                MailRegionalDirectorHyperLinkText = string.Empty
            };

            SetBaseViewData(page01ViewData);
            page01ViewData.MailRegionalDirectorHyperLink = BuildMailRegionalDirectorHyperLink(page01ViewData);
            page01ViewData.MailRegionalDirectorHyperLinkText = "send an e-mail to " + page01ViewData.Config["RegionalDirectorText"];
            return View(page01ViewData);
        }

        /// <summary>
        /// Handles the HTTP POST request for the "Page01" action, initiating the registration process for a new judge.
        /// </summary>
        /// <remarks>
        /// This method checks the current registration state before proceeding. If registration
        /// is unavailable,  the user is redirected to the appropriate action based on the current state. If
        /// registration is available,  a new judge is created, and the user is redirected to the next step in the
        /// registration process.
        /// </remarks>
        /// <returns>
        /// An <see cref="ActionResult"/> that redirects the user to the next step in the registration process if
        /// successful, or to an appropriate action or the home page in case of an error.</returns>
        [HttpPost]
        [ActionName("Page01")]
        public ActionResult Page01Post()
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                Judge newJudge = new Judge()
                {
                    TimeRegistrationStarted = new DateTime?(DateTime.Now),
                    UserAgent = Request.Headers["User-Agent"].ToString()
                };

                // TODO: else case: Send an e-mail reporting database failure; could not create the record

                Repository.AddJudge(newJudge);
                return RedirectToAction("Page02", (object)new { id = newJudge.JudgeID });
            }
            catch (Exception exception)
            {
                // TODO: Replace with Error Message?
                // Log the error using Elmah.
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handles HTTP GET requests for the second page of the Judges Registration process.
        /// </summary>
        /// <param name="id">
        /// The ID of the judge being processed.
        /// </param>
        /// <returns>
        /// A <see cref="ActionResult"/> for the second page of the registration process.
        /// </returns>
        [HttpGet]
        public ActionResult Page02(int id)
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page02ViewData page02ViewData = new Page02ViewData(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,

                // TODO: These two cannot be set here like this. Find a better way.
                // TODO: (06/20/2025) Also, the following two lines are not set in the OdysseyMvc4 code. Why are they set here in this code?
                TshirtSizes = (IEnumerable<SelectListItem>)new SelectList(Enumerable.Select((IEnumerable<string>)["S", "M", "L", "XL", "XXL", "XXXL"], x => { var data = new { value = x, text = x }; return data; }), "value", "text"),
                ProblemChoices = (IEnumerable<SelectListItem>)new SelectList(Repository.ProblemChoices, "ProblemID", "ProblemName")
            };

            InitializePage02ViewData(page02ViewData);
            return View(page02ViewData);
        }

        /// <summary>
        /// Handles the HTTP POST request for the "Page02" action, processing the registration data submitted by the user.
        /// </summary>
        /// <param name="id">
        /// The ID of the judge being processed.
        /// </param>
        /// <param name="page02ViewData">
        /// The view data containing the registration information submitted by the user.
        /// </param>
        /// <returns>
        /// A redirect to the next action if successful; otherwise, returns the view with validation errors.
        /// </returns>
        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                // TODO: What should we do here if the ModelState isn't valid? - Rob, 09/30/2014
                if (ModelState.IsValid)
                {
                    Judge newJudgeData = new()
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
                        ProblemCOI1 = page02ViewData.ProblemConflict1,
                        ProblemCOI2 = page02ViewData.ProblemConflict2,
                        ProblemCOI3 = page02ViewData.ProblemConflict3,
                        YearsOfLongTermJudgingExperience = page02ViewData.YearsOfLongTermJudgingExperience,
                        YearsOfSpontaneousJudgingExperience = page02ViewData.YearsOfSpontaneousJudgingExperience,
                        PreviousPositions = JudgesRegistrationController.GetPreviousPositions(page02ViewData),
                        WillingToBeScorechecker = page02ViewData.WillingToBeScorechecker,
                        TshirtSize = page02ViewData.TshirtSize,
                        WantsCEUCredit = page02ViewData.WantsCeuCredit,
                        Notes = page02ViewData.Notes
                    };

                    // If the judge did not provide an e-mail address, make sure "None" is written to the database
                    if (string.IsNullOrWhiteSpace(newJudgeData.EmailAddress))
                    {
                        newJudgeData.EmailAddress = "None";
                    }

                    // TODO: if case: Send an e-mail reporting database failure; could not find the record already added to the database
                    Repository.UpdateJudge(id, 2, newJudgeData);

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

                    return RedirectToAction("Page03", (object)new { id = id });
                }

                InitializePage02ViewData(page02ViewData);
                
                return View(page02ViewData);
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);

                // TODO: Replace with Error Message
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Displays the Page03 view for a judge's registration process and handles associated logic.
        /// </summary>
        /// <remarks>
        /// This method performs several tasks as part of the judge registration process: <list
        /// type="bullet"> <item>Validates the current registration state and redirects if registration is
        /// unavailable.</item> <item>Loads and prepares the necessary view data, including judge information and
        /// configuration details.</item> <item>Handles errors if the judge data is invalid or missing.</item>
        /// <item>Sends an email to the judge if a valid email address is provided, or redirects to an error page if
        /// email sending fails.</item> </list>
        /// </remarks>
        /// <param name="id">
        /// The unique identifier of the judge whose registration data is being processed.
        /// </param>
        /// <returns>
        /// An <see cref="ActionResult"/> that renders the Page03 view with the appropriate data, redirects to another
        /// action if the registration state is unavailable, or handles errors such as invalid judge data or email
        /// issues.
        /// </returns>
        [HttpGet]
        public ActionResult Page03(int id)
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page03ViewData page03ViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                JudgesInfo = Repository.JudgesInfo
            };

            SetBaseViewData(page03ViewData);

            // Update the DateTime of the registration in the Judge record.
            page03ViewData.Judge = Repository.GetJudgeById(id).FirstOrDefault<Judge>();

            // This should NEVER happen!
            if (page03ViewData.Judge == null)
            {
                // Judge not found; return error
                page03ViewData.ErrorMessage = "Your registration failed.  Please try the registration process over again.";
                return View(page03ViewData);
            }

            Repository.UpdateJudge(id, 3, page03ViewData.Judge);
            
            page03ViewData.MailBody = GenerateEmailBody(page03ViewData);

            if (!string.IsNullOrWhiteSpace(page03ViewData.Judge.EmailAddress) && page03ViewData.Judge.EmailAddress != "None")
            {
                page03ViewData.EmailAddressWasSpecified = true;
                MailMessage mailMessage = BuildMessage(page03ViewData.Config["WebmasterEmail"], page03ViewData.RegionName + " Odyssey Region " + page03ViewData.RegionNumber + " " + page03ViewData.FriendlyRegistrationName, page03ViewData.MailBody, page03ViewData.Judge.EmailAddress, (string)null, (string)null);
                if (mailMessage == null)
                {
                    return RedirectToAction("BadEmail");
                }
                // Instantiate a new instance of SmtpClient to send the e-mail to the judge.
                page03ViewData.MailErrorMessage = SendMessage(page03ViewData, mailMessage);
            }
            else
            {
                page03ViewData.EmailAddressWasSpecified = false;
            }

            return View(page03ViewData);
        }

        /// <summary>
        /// Handles the HTTP POST request for the "Page03" action, processing the judge's registration data and
        /// updating necessary information based on the submitted data.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the judge whose registration data is being processed.
        /// </param>
        /// <param name="submitButton">
        /// Only contains a value when resubmitting an e-mail address.
        /// </param>
        /// <param name="homePageButton">
        /// </param>
        /// <param name="nextButton">
        /// The next Button.
        /// </param>
        /// <param name="restartRegistrationButton">
        /// The restart Registration Button.
        /// </param>
        /// <param name="collection">
        /// The collection of form values submitted.
        /// </param>
        /// <returns>
        /// An <see cref="ActionResult"/> that represents the result of the action.
        /// </returns>
        [HttpPost]
        public ActionResult Page03(
          int id,
          string submitButton,
          string homePageButton,
          string nextButton,
          string restartRegistrationButton,
          FormCollection collection)
        {
            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            if (!string.IsNullOrEmpty(restartRegistrationButton))
            {
                return RedirectToAction("Page01");
            }

            // User submitted a new e-mail address after mailing the previous one failed.
            if (!string.IsNullOrEmpty(submitButton))
            {
                // Update the Judge e-mail address in the database.
                Repository.UpdateJudgeEmail(id, collection["NewEmailTextBox"]);
                return Page03(id);
            }

            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return new RedirectResult(baseViewData.Config["HomePage"]);
            // return RedirectToAction("Index", "Home");
        }
    }
}
