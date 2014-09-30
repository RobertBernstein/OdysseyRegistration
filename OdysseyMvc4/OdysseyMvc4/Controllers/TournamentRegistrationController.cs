// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the TournamentRegistrationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Web.Mvc;

    using Elmah;

    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData.TournamentRegistration;

    public class TournamentRegistrationController : BaseRegistrationController
    {
        public TournamentRegistrationController()
        {
            base.CurrentRegistrationType = BaseRegistrationController.RegistrationType.Tournament;
            base.FriendlyRegistrationName = base.GetFriendlyRegistrationName();
        }

        public ActionResult BadAltCoachEmail()
        {
            return base.View();
        }

        public ActionResult BadCoachEmail()
        {
            return base.View();
        }

        public ActionResult Carolina()
        {
            base.Repository.ClearTeamIdFromJudgeRecord(0x26, "Carolina", "Deschapelles");
            return base.RedirectToAction("Page01", "TournamentRegistration");
        }

        public int DetermineDivisionOfTeam(List<string> gradesOfTeamMembers)
        {
            int num = -1;
            foreach (string str in gradesOfTeamMembers)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    int divisionOfTeamMember = this.GetDivisionOfTeamMember(str);
                    if (divisionOfTeamMember > num)
                    {
                        num = divisionOfTeamMember;
                    }
                }
            }
            return num;
        }

        private string GenerateEmailBody(Page10ViewData page10ViewData)
        {
            using (StringWriter writer = new StringWriter())
            {
                base.ViewData.Model = page10ViewData;
                ViewEngineResult result = ViewEngines.Engines.FindPartialView(base.ControllerContext, "TournamentRegistration/EmailPartial");
                ViewContext viewContext = new ViewContext(base.ControllerContext, result.View, base.ViewData, base.TempData, writer);
                result.View.Render(viewContext, writer);
                return writer.GetStringBuilder().ToString();
            }
        }

        public int GetDivisionOfTeamMember(string memberGrade)
        {
            int num = (memberGrade == "Kindergarten") ? 0 : int.Parse(memberGrade);
            return (((num >= 0) && (num <= 2)) ? 0 : ((num <= 5) ? 1 : ((num <= 8) ? 2 : 3)));
        }

        public string GetProblemsAsHtmlList(bool thisTeamIsPrimary)
        {
            IQueryable<Problem> problemsWithoutPrimaryOrSpontaneous = base.Repository.ProblemsWithoutPrimaryOrSpontaneous;
            StringBuilder builder = new StringBuilder();
            builder.Append("<ol>\n");
            if (thisTeamIsPrimary)
            {
                builder.Append("<li>" + base.Repository.PrimaryProblem.First<Problem>().ProblemName + " (The Primary Problem)</li>\n");
            }
            foreach (Problem problem in problemsWithoutPrimaryOrSpontaneous)
            {
                builder.Append("<li>" + problem.ProblemName + "</li>\n");
            }
            builder.Append("</ol>\n");
            return builder.ToString();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return base.RedirectToAction("Page01");
        }

        public ActionResult Joyce()
        {
            base.Repository.ClearTeamIdFromJudgeRecord(30, "Joyce", "Ghen");
            return base.RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Margaret()
        {
            base.Repository.ClearTeamIdFromJudgeRecord(0x11, "Margaret", "Eccles");
            return base.RedirectToAction("Page01", "TournamentRegistration");
        }

        [HttpGet]
        public ActionResult Page01()
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page01ViewData viewData = new Page01ViewData();
            base.SetBaseViewData(viewData);
            return base.View(viewData);
        }

        [HttpPost, ActionName("Page01")]
        public ActionResult Page01Post()
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page01ViewData viewData = new Page01ViewData();
            base.SetBaseViewData(viewData);
            try
            {
                TournamentRegistration newRegistration = new TournamentRegistration {
                    TimeRegistrationStarted = new DateTime?(DateTime.Now),
                    TeamRegistrationFee = viewData.TeamRegistrationFee,
                    UserAgent = base.Request.UserAgent
                };
                base.Repository.AddTournamentRegistration(newRegistration);
                return base.RedirectToAction("Page02", new { id = newRegistration.TeamID });
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
                SchoolList = new SelectList(base.Repository.Schools, "ID", "Name")
            };
            base.SetBaseViewData(viewData);
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration {
                    SchoolID = new int?(page02ViewData.SelectedSchool)
                };
                base.Repository.UpdateTournamentRegistration(id, 2, newRegistrationData);
                return base.RedirectToAction("Page03", new { id = id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page03(int id)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page03ViewData viewData = new Page03ViewData();
            base.SetBaseViewData(viewData);
            viewData.NoJudgesFound = false;
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page03(int id, Page03ViewData page03ViewData)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                base.SetBaseViewData(page03ViewData);
                base.UpdateModel<Page03ViewData>(page03ViewData);
                page03ViewData.ListOfJudgesFound = base.Repository.GetJudgeByIdAndName(int.Parse(page03ViewData.JudgeId), page03ViewData.JudgeFirstName, page03ViewData.JudgeLastName);
                if (!page03ViewData.ListOfJudgesFound.Any<Judge>())
                {
                    page03ViewData.NoJudgesFound = true;
                    return base.View(page03ViewData);
                }
                if ((page03ViewData.ListOfJudgesFound.First<Judge>() != null) && (page03ViewData.ListOfJudgesFound.First<Judge>().TeamID != null))
                {
                    page03ViewData.JudgeAlreadyTaken = true;
                    return base.View(page03ViewData);
                }
                TournamentRegistration newRegistrationData = new TournamentRegistration {
                    JudgeID = new short?(short.Parse(page03ViewData.JudgeId))
                };
                base.Repository.UpdateTournamentRegistration(id, 3, newRegistrationData);
                return base.RedirectToAction("Page04", new { id = id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page04(int? id)
        {
            if (!id.HasValue)
            {
                return base.RedirectToAction("Error");
            }
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page04ViewData viewData = new Page04ViewData();
            base.SetBaseViewData(viewData);
            viewData.NoVolunteersFound = false;
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page04(int? id, Page04ViewData page04ViewData)
        {
            if (!id.HasValue)
            {
                return base.RedirectToAction("Error");
            }
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                base.SetBaseViewData(page04ViewData);
                base.UpdateModel<Page04ViewData>(page04ViewData);
                page04ViewData.VolunteerFound = base.Repository.GetVolunteerByIdAndName(int.Parse(page04ViewData.VolunteerId), page04ViewData.VolunteerFirstName, page04ViewData.VolunteerLastName);
                if (page04ViewData.VolunteerFound == null)
                {
                    page04ViewData.NoVolunteersFound = true;
                    return base.View(page04ViewData);
                }
                if (page04ViewData.VolunteerFound.TeamID.HasValue)
                {
                    page04ViewData.VolunteerAlreadyTaken = true;
                    return base.View(page04ViewData);
                }
                TournamentRegistration newRegistrationData = new TournamentRegistration {
                    VolunteerID = new int?(page04ViewData.VolunteerFound.VolunteerID)
                };
                base.Repository.UpdateTournamentRegistration(id.Value, 4, newRegistrationData);
                return base.RedirectToAction("Page05", new { id = id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return base.RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Page05(int id)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page05ViewData viewData = new Page05ViewData();
            base.SetBaseViewData(viewData);
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page05(int id, Page05ViewData page05ViewData)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                base.UpdateModel<Page05ViewData>(page05ViewData);
                base.SetBaseViewData(page05ViewData);
                if (base.BuildMessage(page05ViewData.Config["WebmasterEmail"], "test", "test", page05ViewData.CoachEmailAddress, null, null) == null)
                {
                    return base.RedirectToAction("BadCoachEmail");
                }
                if (base.BuildMessage(page05ViewData.Config["WebmasterEmail"], "test", "test", page05ViewData.AltCoachEmailAddress, null, null) == null)
                {
                    return base.RedirectToAction("BadAltCoachEmail");
                }
                TournamentRegistration newRegistrationData = new TournamentRegistration {
                    CoachFirstName = page05ViewData.CoachFirstName,
                    CoachLastName = page05ViewData.CoachLastName,
                    CoachAddress = page05ViewData.CoachAddress,
                    CoachCity = page05ViewData.CoachCity,
                    CoachState = page05ViewData.CoachState,
                    CoachZipCode = page05ViewData.CoachZipCode,
                    CoachEveningPhone = page05ViewData.CoachEveningPhone,
                    CoachDaytimePhone = page05ViewData.CoachDaytimePhone,
                    CoachMobilePhone = page05ViewData.CoachMobilePhone,
                    CoachEmailAddress = page05ViewData.CoachEmailAddress,
                    AltCoachFirstName = page05ViewData.AltCoachFirstName,
                    AltCoachLastName = page05ViewData.AltCoachLastName,
                    AltCoachEveningPhone = page05ViewData.AltCoachEveningPhone,
                    AltCoachDaytimePhone = page05ViewData.AltCoachDaytimePhone,
                    AltCoachMobilePhone = page05ViewData.AltCoachMobilePhone,
                    AltCoachEmailAddress = page05ViewData.AltCoachEmailAddress
                };
                base.Repository.UpdateTournamentRegistration(id, 5, newRegistrationData);
                return base.RedirectToAction("Page06", new { id = id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page06(int id)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            List<string> items = new List<string> { "Kindergarten" };
            for (int i = 1; i <= 12; i++)
            {
                items.Add(i.ToString(CultureInfo.InvariantCulture));
            }
            Page06ViewData viewData = new Page06ViewData {
                GradeChoices = new SelectList(items)
            };
            base.SetBaseViewData(viewData);
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page06(int id, Page06ViewData page06ViewData)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                base.UpdateModel<Page06ViewData>(page06ViewData);
                TournamentRegistration newRegistrationData = new TournamentRegistration {
                    MemberFirstName1 = page06ViewData.MemberFirstName1,
                    MemberLastName1 = page06ViewData.MemberLastName1,
                    MemberGrade1 = page06ViewData.MemberGrade1,
                    MemberFirstName2 = page06ViewData.MemberFirstName2,
                    MemberLastName2 = page06ViewData.MemberLastName2,
                    MemberGrade2 = page06ViewData.MemberGrade2,
                    MemberFirstName3 = page06ViewData.MemberFirstName3,
                    MemberLastName3 = page06ViewData.MemberLastName3,
                    MemberGrade3 = page06ViewData.MemberGrade3,
                    MemberFirstName4 = page06ViewData.MemberFirstName4,
                    MemberLastName4 = page06ViewData.MemberLastName4,
                    MemberGrade4 = page06ViewData.MemberGrade4,
                    MemberFirstName5 = page06ViewData.MemberFirstName5,
                    MemberLastName5 = page06ViewData.MemberLastName5,
                    MemberGrade5 = page06ViewData.MemberGrade5,
                    MemberFirstName6 = page06ViewData.MemberFirstName6,
                    MemberLastName6 = page06ViewData.MemberLastName6,
                    MemberGrade6 = page06ViewData.MemberGrade6,
                    MemberFirstName7 = page06ViewData.MemberFirstName7,
                    MemberLastName7 = page06ViewData.MemberLastName7,
                    MemberGrade7 = page06ViewData.MemberGrade7
                };
                base.Repository.UpdateTournamentRegistration(id, 6, newRegistrationData);
                return base.RedirectToAction("Page07", new { id = id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page07(int id)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            List<string> memberGradesByRegistration = base.Repository.GetMemberGradesByRegistration(id);
            Page07ViewData viewData = new Page07ViewData {
                DivisionOfTeam = this.DetermineDivisionOfTeam(memberGradesByRegistration),
                Division123ProblemDropDown = new SelectList(base.Repository.ProblemsWithoutPrimaryOrSpontaneous, "ProblemID", "ProblemName"),
                IsDoingSpontaneousDropDown = new SelectList(new List<string> { "Yes", "No" }),
                Division123ListOfProblemsAsHtmlList = this.GetProblemsAsHtmlList(false),
                Division123AndPrimaryListOfProblemsAsHtmlList = this.GetProblemsAsHtmlList(true),
                PrimaryProblemName = base.Repository.PrimaryProblem.First<Problem>().ProblemName
            };
            base.SetBaseViewData(viewData);
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page07(int id, Page07ViewData page07ViewData)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration {
                    Division = page07ViewData.DivisionRadioGroup ?? page07ViewData.DivisionOfTeam.ToString(CultureInfo.InvariantCulture)
                };
                string divisionRadioGroup = page07ViewData.DivisionRadioGroup;
                if (divisionRadioGroup == null)
                {
                    goto Label_00C9;
                }
                if (!(divisionRadioGroup == "0"))
                {
                    if (divisionRadioGroup == "1")
                    {
                        goto Label_00B0;
                    }
                    goto Label_00C9;
                }
                newRegistrationData.ProblemID = 6;
                newRegistrationData.Spontaneous = new bool?(page07ViewData.IsDoingSpontaneous == "Yes");
                goto Label_00E2;
            Label_00B0:
                newRegistrationData.ProblemID = new int?(int.Parse(page07ViewData.Division123ProblemChoice));
                goto Label_00E2;
            Label_00C9:
                newRegistrationData.ProblemID = new int?(int.Parse(page07ViewData.SelectedProblem));
            Label_00E2:
                base.Repository.UpdateTournamentRegistration(id, 7, newRegistrationData);
                return base.RedirectToAction("Page08", new { id = id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page08(int id)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page08ViewData viewData = new Page08ViewData();
            base.SetBaseViewData(viewData);
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page08(int id, Page08ViewData page08ViewData)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration {
                    SchedulingIssues = page08ViewData.SchedulingIssues,
                    SpecialConsiderations = page08ViewData.SpecialConsiderations
                };
                base.Repository.UpdateTournamentRegistration(id, 8, newRegistrationData);
                return base.RedirectToAction("Page09", new { id = id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page09(int id)
        {
            string str;
            string str2;
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            Page09ViewData viewData = new Page09ViewData {
                TournamentRegistration = base.Repository.GetTournamentRegistrationById(id)
            };
            base.SetBaseViewData(viewData);
            viewData.SchoolName = base.Repository.GetSchoolNameFromSchoolId(viewData.TournamentRegistration.SchoolID);
            base.Repository.GetJudgeNameFromJudgeId(viewData.TournamentRegistration.JudgeID, out str, out str2);
            viewData.JudgeFirstName = str;
            viewData.JudgeLastName = str2;
            Volunteer volunteerById = base.Repository.GetVolunteerById(viewData.TournamentRegistration.VolunteerID);
            viewData.VolunteerFirstName = volunteerById.FirstName;
            viewData.VolunteerLastName = volunteerById.LastName;
            viewData.Division = (viewData.TournamentRegistration.Division == "0") ? "Primary" : viewData.TournamentRegistration.Division;
            viewData.ProblemName = base.Repository.GetProblemNameFromProblemId(viewData.TournamentRegistration.ProblemID);
            if (viewData.TournamentRegistration.Spontaneous.HasValue)
            {
                viewData.IsDoingSpontaneous = viewData.TournamentRegistration.Spontaneous.Value ? "Yes" : "No";
            }
            if (string.IsNullOrEmpty(viewData.ProblemName))
            {
                viewData.ProblemName = "(Could not obtain problem name)";
            }
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page09(int id, string homePageButton, string nextButton, FormCollection collection)
        {
            return ((base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available) ? base.RedirectToAction(base.CurrentRegistrationState.ToString()) : base.RedirectToAction("Page10", new { id = id }));
        }

        [HttpGet]
        public ActionResult Page10(int id)
        {
            Page10ViewData data;
            string str;
            MailMessage message;
            string str2;
            string str3;
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            short? judgeIdFromTournamentRegistrationId = base.Repository.GetJudgeIdFromTournamentRegistrationId(id);
            short? nullable3 = judgeIdFromTournamentRegistrationId;
            int? nullable4 = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault()) : null;
            if (nullable4.HasValue)
            {
                base.Repository.UpdateJudgeRecordWithTournamentRegistrationId(judgeIdFromTournamentRegistrationId, id, out str);
                if (!string.IsNullOrEmpty(str))
                {
                    data = new Page10ViewData {
                        JudgeErrorMessage = str
                    };
                    base.SetBaseViewData(data);
                    message = base.BuildMessage(data.Config["WebmasterEmail"], "Error: " + data.RegionName + " Odyssey Region " + data.RegionNumber + " Tournament Registration", string.Concat(new object[] { "<p>Team with ID # ", id, " attempted to re-register after its judge was assigned to the team.</p><p>", str, "</p>" }), data.Config["WebmasterEmail"], null, null);
                    data.MailErrorMessage = base.SendMessage(data, message);
                    return base.View(data);
                }
            }
            int? volunteerIdFromTournamentRegistrationId = base.Repository.GetVolunteerIdFromTournamentRegistrationId(id);
            if (volunteerIdFromTournamentRegistrationId.HasValue)
            {
                base.Repository.UpdateVolunteerRecordWithTournamentRegistrationId(volunteerIdFromTournamentRegistrationId.Value, id, out str);
                if (!string.IsNullOrEmpty(str))
                {
                    data = new Page10ViewData {
                        VolunteerErrorMessage = str
                    };
                    base.SetBaseViewData(data);
                    message = base.BuildMessage(data.Config["WebmasterEmail"], "Error: " + data.RegionName + " Odyssey Region " + data.RegionNumber + " Tournament Registration", string.Concat(new object[] { "<p>Team with ID # ", id, " attempted to re-register after its volunteer was assigned to the team.</p><p>", str, "</p>" }), data.Config["WebmasterEmail"], null, null);
                    data.MailErrorMessage = base.SendMessage(data, message);
                    return base.View(data);
                }
            }
            data = new Page10ViewData {
                TournamentInfo = base.Repository.TournamentInfo,
                TournamentRegistration = base.Repository.GetTournamentRegistrationById(id)
            };
            data.TournamentRegistration.TimeRegistered = new DateTime?(DateTime.Now);
            base.Repository.UpdateTournamentRegistration(id, 10, data.TournamentRegistration);
            base.SetBaseViewData(data);
            base.Repository.GetJudgeNameFromJudgeId(judgeIdFromTournamentRegistrationId, out str2, out str3);
            data.JudgeFirstName = str2;
            data.JudgeLastName = str3;
            Volunteer volunteerById = base.Repository.GetVolunteerById(data.TournamentRegistration.VolunteerID);
            data.VolunteerFirstName = volunteerById.FirstName;
            data.VolunteerLastName = volunteerById.LastName;
            data.SchoolName = base.Repository.GetSchoolNameFromSchoolId(data.TournamentRegistration.SchoolID);
            data.ProblemName = base.Repository.GetProblemNameFromProblemId(data.TournamentRegistration.ProblemID);
            data.Division = (data.TournamentRegistration.Division == "0") ? "Primary" : data.TournamentRegistration.Division;
            data.MailBody = this.GenerateEmailBody(data);
            MailMessage mailMessage = base.BuildMessage(data.Config["WebmasterEmail"], data.RegionName + " Odyssey Region " + data.RegionNumber + " Tournament Registration", data.MailBody, data.TournamentRegistration.CoachEmailAddress, null, null);
            data.MailErrorMessage = base.SendMessage(data, mailMessage);
            return base.View(data);
        }

        public ActionResult ResendEmail()
        {
            ResendEmailViewData viewData = new ResendEmailViewData();
            base.SetBaseViewData(viewData);
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult ResendEmail(FormCollection collection)
        {
            string str2;
            string str3;
            ResendEmailViewData model = new ResendEmailViewData();
            base.UpdateModel<ResendEmailViewData>(model);
            Page10ViewData viewData = new Page10ViewData {
                TournamentInfo = base.Repository.TournamentInfo,
                TournamentRegistration = base.Repository.GetTournamentRegistrationById(model.TeamNumber)
            };
            base.SetBaseViewData(viewData);
            if ((model.CoachCheckbox == "false") && (model.AltCoachCheckbox == "false"))
            {
                model.ErrorMessage = "No one was selected to resend the registration information to, so no e-mail was sent.";
                model.Success = false;
                return base.View(model);
            }
            string str = string.Empty;
            if (model.CoachCheckbox == "true")
            {
                str = str + viewData.TournamentRegistration.CoachEmailAddress;
            }
            if (model.AltCoachCheckbox == "true")
            {
                if (!string.IsNullOrEmpty(str))
                {
                    str = str + ",";
                }
                str = str + viewData.TournamentRegistration.AltCoachEmailAddress;
            }
            base.Repository.GetJudgeNameFromJudgeId(viewData.TournamentRegistration.JudgeID, out str2, out str3);
            viewData.JudgeFirstName = str2;
            viewData.JudgeLastName = str3;
            Volunteer volunteerById = base.Repository.GetVolunteerById(viewData.TournamentRegistration.VolunteerID);
            viewData.VolunteerFirstName = volunteerById.FirstName;
            viewData.VolunteerLastName = volunteerById.LastName;
            viewData.SchoolName = base.Repository.GetSchoolNameFromSchoolId(viewData.TournamentRegistration.SchoolID);
            viewData.ProblemName = base.Repository.GetProblemNameFromProblemId(viewData.TournamentRegistration.ProblemID);
            viewData.MailBody = this.GenerateEmailBody(viewData);
            MailMessage mailMessage = base.BuildMessage(viewData.Config["WebmasterEmail"], viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " Tournament Registration", viewData.MailBody, str, "rob@tardistech.com", null);
            model.ErrorMessage = base.SendMessage(model, mailMessage);
            model.Success = true;
            return base.View(model);
        }

        public ActionResult Rob()
        {
            base.Repository.ClearTeamIdFromJudgeRecord(5, "Rob", "Bernstein");
            return base.RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Ron()
        {
            base.Repository.ClearTeamIdFromJudgeRecord(0x1f, "Ron", "Ghen");
            return base.RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Sarah()
        {
            base.Repository.ClearTeamIdFromJudgeRecord(0x10, "Sarah", "Tate");
            return base.RedirectToAction("Page01", "TournamentRegistration");
        }
    }
}
