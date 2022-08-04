namespace OdysseyMvc4.Controllers
{
    using Elmah;
    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData.CoachesTrainingRegistration;
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public class CoachesTrainingRegistrationController2 : BaseRegistrationController
    {
        public CoachesTrainingRegistrationController2()
        {
            base.CurrentRegistrationType = BaseRegistrationController.RegistrationType.CoachesTraining;
            base.FriendlyRegistrationName = base.GetFriendlyRegistrationName();
        }

        protected string GenerateEmailBody(Page02ViewData viewData)
        {
            string input = Regex.Replace(viewData.CoachesTrainingInfo.EventMailBody, "<span>Region</span>", "Region " + viewData.Config["RegionNumber"]);
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationURL))
            {
                builder.Append("<a href=\"" + viewData.CoachesTrainingInfo.LocationURL + "\" target=\"_blank\">");
            }
            builder.Append(viewData.CoachesTrainingInfo.Location);
            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationAddress))
            {
                builder.Append(", " + viewData.CoachesTrainingInfo.LocationAddress);
            }
            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationCity))
            {
                builder.Append(", " + viewData.CoachesTrainingInfo.LocationCity);
            }
            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationState))
            {
                builder.Append(", " + viewData.CoachesTrainingInfo.LocationState);
            }
            if (!string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.LocationURL))
            {
                builder.Append("</a>");
            }
            input = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(input, "<span>Location</span>", builder.ToString()), "<span>Date</span>", viewData.CoachesTrainingInfo.StartDate.HasValue ? viewData.CoachesTrainingInfo.StartDate.Value.ToLongDateString() : "TBA"), "<span>Time</span>", !string.IsNullOrWhiteSpace(viewData.CoachesTrainingInfo.Time) ? viewData.CoachesTrainingInfo.Time : "TBA"), "<span>Years</span>", viewData.Config["Year"] + " - " + viewData.Config["EndYear"]), "<span>ProgramGuide</span>", "<a href=\"" + viewData.Config["ProgramGuideURL"] + "\" target=\"_blank\">" + viewData.Config["ProgramGuideURL"] + "</a>");
            builder.Clear();
            if (viewData.Config.ContainsKey("CoachesHandbookURL") && !string.IsNullOrWhiteSpace(viewData.Config["CoachesHandbookURL"]))
            {
                builder.Append("<li>\ta copy of the Coaches Handbook from the Virginia state website (<a href=\"" + viewData.Config["CoachesHandbookURL"] + "\" target=\"blank\">" + viewData.Config["CoachesHandbookURL"] + "</a>),</li>\n");
            }
            input = Regex.Replace(Regex.Replace(Regex.Replace(input, "<span>VirginiaHandbook</span>", builder.ToString()), "<span>Fee</span>", viewData.CoachesTrainingInfo.EventCost), "<span>MakeChecksOutTo</span>", viewData.CoachesTrainingInfo.EventMakeChecksOutTo);
            builder.Clear();
            if (viewData.Config.ContainsKey("CoordinatorsDoNotPayCoachesTrainingRegistrationFee") && (viewData.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"].ToLower() == "true"))
            {
                builder.Append(viewData.Config["SchoolCoordinatorsDoNotPayMessage"]);
            }
            return Regex.Replace(Regex.Replace(input, "<span>CoordinatorsDoNotPay</span>", builder.ToString()), "<span>RegionalDirectorEmail</span>", "<a href=\"mailto:" + viewData.Config["RegionalDirectorEmail"] + "\">" + viewData.Config["RegionalDirectorEmail"] + "</a>");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return base.RedirectToAction("Page01");
        }

        [HttpGet]
        public ActionResult Page01()
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page01ViewData viewData = new Page01ViewData {
                CoachesTrainingInfo = base.Repository.CoachesTrainingInfo,
                RoleList = new SelectList(base.Repository.Roles, "ID", "Name"),
                DivisionList = new SelectList(base.Repository.Divisions, "ID", "Name"),
                ProblemList = new SelectList(base.Repository.ProblemChoicesWithoutSpontaneous, "ProblemID", "ProblemName"),
                RegionList = new SelectList(base.Repository.Regions, "Name", "Name")
            };
            base.SetBaseViewData(viewData);
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page01(Page01ViewData viewData)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                CoachesTrainingRegistration newRegistration = new CoachesTrainingRegistration {
                    FirstName = viewData.FirstName,
                    LastName = viewData.LastName,
                    SchoolName = viewData.SchoolName,
                    Role = viewData.SelectedRole,
                    Division = viewData.SelectedDivision,
                    SelectedProblem = viewData.SelectedProblem,
                    EmailAddress = viewData.EmailAddress,
                    YearsInvolved = viewData.YearsInvolved,
                    RegionNumber = viewData.SelectedRegion,
                    TimeRegistered = new DateTime?(DateTime.Now),
                    UserAgent = base.Request.UserAgent
                };
                base.Repository.AddCoachesTrainingRegistration(newRegistration);
                return base.RedirectToAction("Page02", new { id = newRegistration.RegistrationID });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page02(int id)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page02ViewData viewData = new Page02ViewData {
                CoachesTrainingInfo = base.Repository.CoachesTrainingInfo
            };
            base.SetBaseViewData(viewData);
            viewData.CoachesTraining = base.Repository.GetCoachesTrainingRegistrationById(id).FirstOrDefault<CoachesTrainingRegistration>();
            if (viewData.CoachesTraining == null)
            {
                viewData.ErrorMessage = "Your registration failed.  Please try the registration process over again.";
                return base.View(viewData);
            }
            viewData.MailBody = this.GenerateEmailBody(viewData);
            MailMessage mailMessage = base.BuildMessage(viewData.Config["WebmasterEmail"], viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " " + viewData.FriendlyRegistrationName, viewData.MailBody, viewData.CoachesTraining.EmailAddress, null, null);
            viewData.MailErrorMessage = base.SendMessage(viewData, mailMessage);
            return base.View(viewData);
        }
    }
}

