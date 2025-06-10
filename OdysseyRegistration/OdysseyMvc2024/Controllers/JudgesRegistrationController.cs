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
    public class JudgesRegistrationController : BaseRegistrationController
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
                stringBuilder.Append("<a href=\"" + page03ViewData.JudgesInfo.LocationURL + "\" target=\"_blank\">");
            stringBuilder.Append(page03ViewData.JudgesInfo.Location);
            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationAddress))
                stringBuilder.Append(", " + page03ViewData.JudgesInfo.LocationAddress);
            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationCity))
                stringBuilder.Append(", " + page03ViewData.JudgesInfo.LocationCity);
            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationState))
                stringBuilder.Append(", " + page03ViewData.JudgesInfo.LocationState);
            if (!string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.LocationURL))
                stringBuilder.Append("</a>");

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
                stringBuilder.Append("<a href=\"" + page03ViewData.TournamentInfo.LocationURL + "\" target=\"_blank\">");
            stringBuilder.Append(page03ViewData.TournamentInfo.Location);
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationAddress))
                stringBuilder.Append(", " + page03ViewData.TournamentInfo.LocationAddress);
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationCity))
                stringBuilder.Append(", " + page03ViewData.TournamentInfo.LocationCity);
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationState))
                stringBuilder.Append(", " + page03ViewData.TournamentInfo.LocationState);
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationURL))
                stringBuilder.Append("</a>");
            string input4 = Regex.Replace(input3, "<span>TournamentLocation</span>", stringBuilder.ToString());
            startDate = page03ViewData.TournamentInfo.StartDate;
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
            return Regex.Replace(Regex.Replace(Regex.Replace(input4, "<span>TournamentDate</span>", replacement2), "<span>TournamentTime</span>", !string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.Time) ? page03ViewData.TournamentInfo.Time : "TBA"), "<span>ContactUsURL</span>", page03ViewData.Config["HomePage"] + page03ViewData.Config["ContactUsURL"]);
        }

        private static string GetPreviousPositions(Page02ViewData page02ViewData)
        {
            // TODO: Add curly braces to all of this.
            StringBuilder stringBuilder = new();
            if (page02ViewData.PreviouslyHeadJudge)
                stringBuilder.Append("Head Judge");
            if (page02ViewData.PreviouslyProblemJudge)
                stringBuilder.Append(";Problem Judge");
            if (page02ViewData.PreviouslyStyleJudge)
                stringBuilder.Append(";Style Judge");
            if (page02ViewData.PreviouslyStagingJudge)
                stringBuilder.Append(";Staging Judge");
            if (page02ViewData.PreviouslyTimekeeper)
                stringBuilder.Append(";Timekeeper");
            if (page02ViewData.PreviouslyScorechecker)
                stringBuilder.Append(";Scorechecker");
            if (page02ViewData.PreviouslyWeighInJudge)
                stringBuilder.Append(";Weigh-In Judge");
            if (stringBuilder.Length <= 0)
                return null;
            if (stringBuilder[0] == ';')
                stringBuilder.Remove(0, 1);
            return stringBuilder.ToString().Trim();
        }

        [HttpGet]
        public ActionResult Index() => RedirectToAction("Page01");

        private void InitializePage02ViewData(Page02ViewData page02ViewData)
        {
            page02ViewData.TshirtSizes = (IEnumerable<SelectListItem>)new SelectList(Enumerable.Select((IEnumerable<string>)new string[6] { "S", "M", "L", "XL", "XXL", "XXXL" }, x => { var data = new { value = x, text = x }; return data; }), "value", "text");
            page02ViewData.ProblemChoices = (IEnumerable<SelectListItem>)new SelectList(Repository.ProblemChoices, "ProblemID", "ProblemName");
            SetBaseViewData(page02ViewData);
        }

        [HttpGet]
        public ActionResult Page01()
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return RedirectToAction(CurrentRegistrationState.ToString());

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

        [HttpPost]
        [ActionName("Page01")]
        public ActionResult Page01Post()
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return RedirectToAction(CurrentRegistrationState.ToString());
            try
            {
                Judge newJudge = new Judge()
                {
                    TimeRegistrationStarted = new DateTime?(DateTime.Now),
                    UserAgent = Request.Headers["User-Agent"].ToString()
                };
                Repository.AddJudge(newJudge);
                return RedirectToAction("Page02", (object)new
                {
                    id = newJudge.JudgeID
                });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page02(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return RedirectToAction(CurrentRegistrationState.ToString());

            Page02ViewData page02ViewData = new Page02ViewData(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,

                // TODO: These two cannot be set here like this. Find a better way.
                TshirtSizes = (IEnumerable<SelectListItem>)new SelectList(Enumerable.Select((IEnumerable<string>)["S", "M", "L", "XL", "XXL", "XXXL"], x => { var data = new { value = x, text = x }; return data; }), "value", "text"),
                ProblemChoices = (IEnumerable<SelectListItem>)new SelectList(Repository.ProblemChoices, "ProblemID", "ProblemName")
            };

            InitializePage02ViewData(page02ViewData);
            return View(page02ViewData);
        }

        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return RedirectToAction(CurrentRegistrationState.ToString());
            try
            {
                if (ModelState.IsValid)
                {
                    Judge newRegistrationData = new Judge()
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
                    if (string.IsNullOrWhiteSpace(newRegistrationData.EmailAddress))
                        newRegistrationData.EmailAddress = "None";
                    Repository.UpdateJudge(id, 2, newRegistrationData);
                    return RedirectToAction("Page03", (object)new
                    {
                        id = id
                    });
                }
                InitializePage02ViewData(page02ViewData);
                return View(page02ViewData);
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page03(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return RedirectToAction(CurrentRegistrationState.ToString());

            Page03ViewData page03ViewData = new Page03ViewData(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                JudgesInfo = Repository.JudgesInfo
            };

            SetBaseViewData(page03ViewData);
            page03ViewData.Judge = Repository.GetJudgeById(id).FirstOrDefault<Judge>();
            if (page03ViewData.Judge == null)
            {
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
                    return RedirectToAction("BadEmail");
                page03ViewData.MailErrorMessage = SendMessage(page03ViewData, mailMessage);
            }
            else
                page03ViewData.EmailAddressWasSpecified = false;
            return View(page03ViewData);
        }

        [HttpPost]
        public ActionResult Page03(
          int id,
          string submitButton,
          string homePageButton,
          string nextButton,
          string restartRegistrationButton,
          FormCollection collection)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return RedirectToAction(CurrentRegistrationState.ToString());
            if (!string.IsNullOrEmpty(restartRegistrationButton))
                return RedirectToAction("Page01");
            if (!string.IsNullOrEmpty(submitButton))
            {
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
        }
    }
}
