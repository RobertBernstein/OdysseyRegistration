// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.JudgesRegistrationController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

//using Elmah;
using OdysseyMvc2023.ViewData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using OdysseyMvc2023.Models;
using OdysseyMvc2023.ViewData.JudgesRegistration;

namespace OdysseyMvc2023.Controllers
{
    public class JudgesRegistrationController : BaseRegistrationController
    {
        public JudgesRegistrationController()
        {
            this.CurrentRegistrationType = BaseRegistrationController.RegistrationType.Judges;
            this.FriendlyRegistrationName = this.GetFriendlyRegistrationName();
        }

        public string BuildMailRegionalDirectorHyperLink(Page01ViewData viewData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("mailto:");
            stringBuilder.Append(viewData.Config["RegionalDirectorEmail"]);
            string str = ("?subject=I would like to help at the Region " + viewData.RegionNumber + " Tournament&body=I cannot be a judge this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A").Replace(" ", "%20");
            stringBuilder.Append(str);
            return stringBuilder.ToString();
        }

        protected string GenerateEmailBody(Page03ViewData page03ViewData)
        {
            string input1 = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(page03ViewData.JudgesInfo.EventMailBody, "<span>JudgeID</span>", page03ViewData.Judge.JudgeID.ToString((IFormatProvider)CultureInfo.InvariantCulture)), "<span>FirstName</span>", page03ViewData.Judge.FirstName), "<span>LastName</span>", page03ViewData.Judge.LastName), "<span>Region</span>", "Region " + page03ViewData.Config["RegionNumber"]);
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
            string input2 = Regex.Replace(input1, "<span>JudgesTrainingLocation</span>", stringBuilder.ToString());
            DateTime? startDate;
            string replacement1;
            if (!page03ViewData.JudgesInfo.StartDate.HasValue)
            {
                replacement1 = "TBA";
            }
            else
            {
                startDate = page03ViewData.JudgesInfo.StartDate;
                replacement1 = startDate.Value.ToLongDateString();
            }
            string input3 = Regex.Replace(Regex.Replace(input2, "<span>JudgesTrainingDate</span>", replacement1), "<span>JudgesTrainingTime</span>", !string.IsNullOrWhiteSpace(page03ViewData.JudgesInfo.Time) ? page03ViewData.JudgesInfo.Time : "TBA");
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
            StringBuilder stringBuilder = new StringBuilder();
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
                return (string)null;
            if (stringBuilder[0] == ';')
                stringBuilder.Remove(0, 1);
            return stringBuilder.ToString().Trim();
        }

        [HttpGet]
        public ActionResult Index() => (ActionResult)this.RedirectToAction("Page01");

        private void InitializePage02ViewData(Page02ViewData page02ViewData)
        {
            page02ViewData.TshirtSizes = (IEnumerable<SelectListItem>)new SelectList((IEnumerable)Enumerable.Select((IEnumerable<string>)new string[6]
            {
        "S",
        "M",
        "L",
        "XL",
        "XXL",
        "XXXL"
            }, x =>
            {
                var data = new { value = x, text = x };
                return data;
            }), "value", "text");
            page02ViewData.ProblemChoices = (IEnumerable<SelectListItem>)new SelectList((IEnumerable)this.Repository.ProblemChoices, "ProblemID", "ProblemName");
            this.SetBaseViewData((BaseViewData)page02ViewData);
        }

        [HttpGet]
        public ActionResult Page01()
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page01ViewData page01ViewData = new Page01ViewData()
            {
                JudgesInfo = this.Repository.JudgesInfo
            };
            this.SetBaseViewData((BaseViewData)page01ViewData);
            page01ViewData.MailRegionalDirectorHyperLink = this.BuildMailRegionalDirectorHyperLink(page01ViewData);
            page01ViewData.MailRegionalDirectorHyperLinkText = "send an e-mail to " + page01ViewData.Config["RegionalDirectorText"];
            return (ActionResult)this.View((object)page01ViewData);
        }

        [HttpPost]
        [ActionName("Page01")]
        public ActionResult Page01Post()
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                Judge newJudge = new Judge()
                {
                    TimeRegistrationStarted = new DateTime?(DateTime.Now),
                    UserAgent = this.Request.UserAgent
                };
                this.Repository.AddJudge(newJudge);
                return (ActionResult)this.RedirectToAction("Page02", (object)new
                {
                    id = newJudge.JudgeID
                });
            }
            catch (Exception ex)
            {
                // TODO: Install ELMAH.
                //ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page02(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page02ViewData page02ViewData = new Page02ViewData();
            this.InitializePage02ViewData(page02ViewData);
            return (ActionResult)this.View((object)page02ViewData);
        }

        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                if (this.ModelState.IsValid)
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
                    this.Repository.UpdateJudge(id, 2, newRegistrationData);
                    return (ActionResult)this.RedirectToAction("Page03", (object)new
                    {
                        id = id
                    });
                }
                this.InitializePage02ViewData(page02ViewData);
                return (ActionResult)this.View((object)page02ViewData);
            }
            catch (Exception ex)
            {
                // TODO: Install ELMAH.
                //ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page03(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page03ViewData page03ViewData = new Page03ViewData()
            {
                JudgesInfo = this.Repository.JudgesInfo
            };
            this.SetBaseViewData((BaseViewData)page03ViewData);
            page03ViewData.Judge = this.Repository.GetJudgeById(id).FirstOrDefault<Judge>();
            if (page03ViewData.Judge == null)
            {
                page03ViewData.ErrorMessage = "Your registration failed.  Please try the registration process over again.";
                return (ActionResult)this.View((object)page03ViewData);
            }
            this.Repository.UpdateJudge(id, 3, page03ViewData.Judge);
            page03ViewData.MailBody = this.GenerateEmailBody(page03ViewData);
            if (!string.IsNullOrWhiteSpace(page03ViewData.Judge.EmailAddress) && page03ViewData.Judge.EmailAddress != "None")
            {
                page03ViewData.EmailAddressWasSpecified = true;
                MailMessage mailMessage = this.BuildMessage(page03ViewData.Config["WebmasterEmail"], page03ViewData.RegionName + " Odyssey Region " + page03ViewData.RegionNumber + " " + page03ViewData.FriendlyRegistrationName, page03ViewData.MailBody, page03ViewData.Judge.EmailAddress, (string)null, (string)null);
                if (mailMessage == null)
                    return (ActionResult)this.RedirectToAction("BadEmail");
                page03ViewData.MailErrorMessage = this.SendMessage((BaseViewData)page03ViewData, mailMessage);
            }
            else
                page03ViewData.EmailAddressWasSpecified = false;
            return (ActionResult)this.View((object)page03ViewData);
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
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            if (!string.IsNullOrEmpty(restartRegistrationButton))
                return (ActionResult)this.RedirectToAction("Page01");
            if (!string.IsNullOrEmpty(submitButton))
            {
                this.Repository.UpdateJudgeEmail(id, collection["NewEmailTextBox"]);
                return this.Page03(id);
            }
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            return (ActionResult)new RedirectResult(viewData.Config["HomePage"]);
        }
    }
}
