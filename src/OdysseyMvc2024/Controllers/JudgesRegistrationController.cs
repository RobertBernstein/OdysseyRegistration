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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Encodings.Web;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.JudgesRegistration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElmahCore;

namespace OdysseyMvc2024.Controllers
{
    [SuppressMessage("Design", "CA1515:Consider making public types internal", Justification = "ASP.NET MVC controller types are intentionally public.")]
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Legacy registration flow logs via ELMAH and redirects to preserve UX.")]
    [SuppressMessage("Major Code Smell", "S6967", Justification = "Legacy MVC action patterns are intentionally preserved.")]
    [SuppressMessage("Minor Code Smell", "S6932", Justification = "Legacy request access pattern retained.")]
    [SuppressMessage("Minor Code Smell", "S6608", Justification = "Legacy initialization style retained.")]
    [SuppressMessage("Minor Code Smell", "S2325", Justification = "Instance method retained for testability and override flexibility.")]
    [SuppressMessage("Best Practices", "MA0009:Regular expressions should not be vulnerable to Denial of Service attacks", Justification = "Generated static expressions are fixed string placeholders.")]
    public partial class JudgesRegistrationController : BaseRegistrationController
    {
        // [SetsRequiredMembers] tells the compiler that this constructor initializes all 'required' members
        // (specifically FriendlyRegistrationName from BaseRegistrationController). Without this attribute,
        // external assemblies (such as the unit test project) cannot instantiate this class because the
        // compiler enforces that callers set 'required' properties — even though this constructor already
        // does so internally.
        [SetsRequiredMembers]
        public JudgesRegistrationController(IOdysseyRepository repository)
            : base(repository)
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

            var subject = $"I would like to help at the Region {viewData.RegionNumber} Tournament";
            const string body = "I cannot be a judge this year, but would like to help in some other way.\r\n\r\nMy name is ______________________.\r\n\r\nMy phone number is ______________________.\r\n\r\n";

            var encodedSubject = Uri.EscapeDataString(subject);
            var encodedBody = Uri.EscapeDataString(body);

            return $"mailto:{regionalDirectorEmail}?subject={encodedSubject}&body={encodedBody}";
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
            ArgumentNullException.ThrowIfNull(page03ViewData);

            Event judgesInfo = page03ViewData.JudgesInfo ?? throw new ArgumentNullException(nameof(page03ViewData));
            Judge judge = page03ViewData.Judge ?? throw new ArgumentNullException(nameof(page03ViewData));
            Event tournamentInfo = page03ViewData.TournamentInfo ?? throw new ArgumentNullException(nameof(page03ViewData));
            IDictionary<string, string> config = page03ViewData.Config ?? throw new ArgumentNullException(nameof(page03ViewData));

            var body = judgesInfo.EventMailBody;
            body = ReplaceJudgeTokens(body, page03ViewData.RegionNumber, judge);
            body = ReplaceLocationTokens(body, judgesInfo, tournamentInfo, config);
            return body;
        }

        private static string ReplaceJudgeTokens(string body, string regionNumber, Judge judge)
        {
            var replaced = JudgeIdRegex().Replace(body, judge.JudgeID.ToString(CultureInfo.InvariantCulture));
            replaced = FirstNameRegex().Replace(replaced, judge.FirstName);
            replaced = LastNameRegex().Replace(replaced, judge.LastName);
            return RegionRegex().Replace(replaced, "Region " + regionNumber);
        }

        private static string ReplaceLocationTokens(string body, Event judgesInfo, Event tournamentInfo, IDictionary<string, string> config)
        {
            var htmlEncoder = HtmlEncoder.Default;
            var trainingLocation = BuildLocationHtml(judgesInfo.Location, judgesInfo.LocationURL, judgesInfo.LocationAddress, judgesInfo.LocationCity, judgesInfo.LocationState, htmlEncoder);
            var tournamentLocation = BuildLocationHtml(tournamentInfo.Location, tournamentInfo.LocationURL, tournamentInfo.LocationAddress, tournamentInfo.LocationCity, tournamentInfo.LocationState, htmlEncoder);

            var replaced = JudgesTrainingLocationRegex().Replace(body, trainingLocation);
            replaced = JudgesTrainingDateRegex().Replace(replaced, judgesInfo.StartDate.HasValue ? judgesInfo.StartDate.Value.ToLongDateString() : "TBA");
            replaced = JudgesTrainingTimeRegex().Replace(replaced, !string.IsNullOrWhiteSpace(judgesInfo.Time) ? judgesInfo.Time : "TBA");
            replaced = TournamentLocationRegex().Replace(replaced, tournamentLocation);
            replaced = TournamentDateRegex().Replace(replaced, tournamentInfo.StartDate.HasValue ? tournamentInfo.StartDate.Value.ToLongDateString() : "TBA");
            replaced = TournamentTimeRegex().Replace(replaced, !string.IsNullOrWhiteSpace(tournamentInfo.Time) ? tournamentInfo.Time : "TBA");
            return ContactUsUrlRegex().Replace(replaced, config["HomePage"] + config["ContactUsURL"]);
        }

        private static string BuildLocationHtml(string location, string locationUrl, string locationAddress, string locationCity, string locationState, HtmlEncoder htmlEncoder)
        {
            var builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(locationUrl))
            {
                builder.Append("<a href=\"")
                    .Append(htmlEncoder.Encode(locationUrl))
                    .Append("\" target=\"_blank\">");
            }

            builder.Append(htmlEncoder.Encode(location));

            if (!string.IsNullOrWhiteSpace(locationAddress))
            {
                builder.Append(", ").Append(htmlEncoder.Encode(locationAddress));
            }

            if (!string.IsNullOrWhiteSpace(locationCity))
            {
                builder.Append(", ").Append(htmlEncoder.Encode(locationCity));
            }

            if (!string.IsNullOrWhiteSpace(locationState))
            {
                builder.Append(", ").Append(htmlEncoder.Encode(locationState));
            }

            if (!string.IsNullOrWhiteSpace(locationUrl))
            {
                builder.Append("</a>");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Concatenate all the "Previous Positions Held" checked box values on Judges
        /// Registration Page 2.
        /// </summary>
        /// <param name="page02ViewData">
        /// The Page02 View Data.
        /// </param>
        /// <returns>
        /// A concatenated, semicolon-separated string of previously-held positions.
        /// </returns>
        private static string? GetPreviousPositions(Page02ViewData page02ViewData)
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
        /// <remarks>
        /// If you don't pass the selected value into SelectList(...), the prior selection won't show when re-rendering.
        /// </remarks>
        [HttpGet]
        public ActionResult Index() => RedirectToAction("Page01");

        /// <summary>
        /// Populates the dropdown lists for the Page02 view with available options and preserves the selected values.
        /// </summary>
        /// <remarks>
        /// This method populates the <see cref="Page02ViewData.TshirtSizes"/> and <see
        /// cref="Page02ViewData.ProblemChoices"/> properties with <see cref="SelectList"/> objects based on predefined
        /// choices and repository data. The selected values in the view data are preserved in the generated
        /// lists.
        /// </remarks>
        /// <param name="page02ViewData">
        /// The view data object for Page02, which contains the selected values and will be updated with the populated
        /// dropdown lists.
        /// </param>
        private void PopulatePage02ViewData(Page02ViewData page02ViewData)
        {
            SetBaseViewData(page02ViewData);

            page02ViewData.TshirtSizes = new SelectList(TshirtSizeChoices, page02ViewData.SelectedTshirtSize);
            
            // Preserve selected values for each dropdown
            page02ViewData.ProblemChoices = new SelectList(
                Repository.ProblemChoices,
                "ProblemID",
                "ProblemName",
                page02ViewData.ProblemChoice1 // the selected value for the main dropdown; others can reuse the same list
            );
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

            var page01ViewData = new Page01ViewData()
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
            IDictionary<string, string> config = page01ViewData.Config ?? throw new InvalidOperationException("Configuration is missing for Page01.");
            page01ViewData.MailRegionalDirectorHyperLinkText = "send an e-mail to " + config["RegionalDirectorText"];
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
                    TimeRegistrationStarted = DateTime.Now,
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

            // Validate that tournament info is available
            var tournamentInfo = Repository.TournamentInfo;
            if (tournamentInfo == null)
            {
                // Log the error and redirect to an error page
                var exception = new InvalidOperationException("Tournament information is not available. Please ensure the database contains tournament data for the current region.");
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }

            var page02ViewData = new Page02ViewData
            {
                Config = Repository.Config,
                TournamentInfo = tournamentInfo,
                TshirtSizes = new SelectList(TshirtSizeChoices, TshirtSizeChoices[0]),
                ProblemChoices = new SelectList(
                    Repository.ProblemChoices,
                    "ProblemID",
                    "ProblemName"
                )
            };

            // Set base view data including CSS file path
            PopulatePage02ViewData(page02ViewData);
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
        /// <remarks>
        /// Html.DropDownListFor uses the items you pass but picks the selected value from ModelState; always repopulate items on a POST redisplay.
        /// </remarks>
        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            ArgumentNullException.ThrowIfNull(page02ViewData);

            // If registration is currently closed, down, or coming soon, redirect to the appropriate page.
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                // TODO: Is this what we should do here if the ModelState isn't valid? - Rob, 09/30/2014
                if (!ModelState.IsValid)
                {
                    // Rebuild lists before re-rendering the view so razor helpers have items
                    PopulatePage02ViewData(page02ViewData);
                    return View(page02ViewData);
                }

                var newJudgeData = CreateJudgeFromPage02(page02ViewData);
                Repository.UpdateJudge(id, 2, newJudgeData);
                return RedirectToAction("Page03", new { id });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        private static Judge CreateJudgeFromPage02(Page02ViewData page02ViewData)
        {
            var newJudgeData = new Judge
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
                PreviousPositions = GetPreviousPositions(page02ViewData),
                WillingToBeScorechecker = page02ViewData.WillingToBeScorechecker,
                TshirtSize = page02ViewData.SelectedTshirtSize,
                WantsCEUCredit = page02ViewData.WantsCeuCredit,
                Notes = page02ViewData.Notes
            };

            // If the judge did not provide an e-mail address, make sure "None" is written to the database
            if (string.IsNullOrWhiteSpace(newJudgeData.EmailAddress))
            {
                newJudgeData.EmailAddress = "None";
            }

            return newJudgeData;
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

            Page03ViewData page03ViewData = new()
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

                // TODO: Check for an empty WebmasterEmailPassword here. - 10/30/2025.
                // TODO: Log if pw is empty.
                IDictionary<string, string> config = page03ViewData.Config ?? throw new InvalidOperationException("Configuration is missing for Page03.");
                var mailMessage = BuildMessage(config["WebmasterEmail"], page03ViewData.RegionName + " Odyssey Region " + page03ViewData.RegionNumber + " " + page03ViewData.FriendlyRegistrationName, page03ViewData.MailBody, page03ViewData.Judge.EmailAddress, string.Empty, string.Empty);

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
          IFormCollection collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

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
                string newEmail = collection["NewEmailTextBox"].ToString();
                Repository.UpdateJudgeEmail(id, newEmail);
                return Page03(id);
            }

            BaseViewData baseViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            IDictionary<string, string> config = baseViewData.Config ?? throw new InvalidOperationException("Configuration is missing for redirect.");
            return new RedirectResult(config["HomePage"]);
            // return RedirectToAction("Index", "Home");
        }
    }
}
