// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.TournamentRegistrationController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using Elmah;
using OdysseyMvc2023.Models;
using OdysseyMvc2023.ViewData;
using OdysseyMvc2023.ViewData.TournamentRegistration;

namespace OdysseyMvc2023.Controllers
{
    public class TournamentRegistrationController : BaseRegistrationController
    {
        public TournamentRegistrationController()
        {
            this.CurrentRegistrationType = BaseRegistrationController.RegistrationType.Tournament;
            this.FriendlyRegistrationName = this.GetFriendlyRegistrationName();
        }

        public ActionResult BadAltCoachEmail() => (ActionResult)this.View();

        public ActionResult BadCoachEmail() => (ActionResult)this.View();

        public int DetermineDivisionOfTeam(List<string> gradesOfTeamMembers)
        {
            int divisionOfTeam = -1;
            foreach (string gradesOfTeamMember in gradesOfTeamMembers)
            {
                if (!string.IsNullOrEmpty(gradesOfTeamMember))
                {
                    int divisionOfTeamMember = this.GetDivisionOfTeamMember(gradesOfTeamMember);
                    if (divisionOfTeamMember > divisionOfTeam)
                        divisionOfTeam = divisionOfTeamMember;
                }
            }
            return divisionOfTeam;
        }

        public int GetDivisionOfTeamMember(string memberGrade)
        {
            int num = memberGrade == "Kindergarten" ? 0 : int.Parse(memberGrade);
            return num < 0 || num > 2 ? (num <= 5 ? 1 : (num <= 8 ? 2 : 3)) : 0;
        }

        public string GetProblemsAsHtmlList(bool thisTeamIsPrimary)
        {
            IQueryable<Problem> primaryOrSpontaneous = this.Repository.ProblemsWithoutPrimaryOrSpontaneous;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ol>\n");
            if (thisTeamIsPrimary)
                stringBuilder.Append("<li>" + this.Repository.PrimaryProblem.First<Problem>().ProblemName + " (The Primary Problem)</li>\n");
            foreach (Problem problem in (IEnumerable<Problem>)primaryOrSpontaneous)
                stringBuilder.Append("<li>" + problem.ProblemName + "</li>\n");
            stringBuilder.Append("</ol>\n");
            return stringBuilder.ToString();
        }

        [HttpGet]
        public ActionResult Index() => (ActionResult)this.RedirectToAction("Page01");

        [HttpGet]
        public ActionResult Page01()
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page01ViewData page01ViewData = new Page01ViewData();
            this.SetBaseViewData((BaseViewData)page01ViewData);
            return (ActionResult)this.View((object)page01ViewData);
        }

        [HttpPost]
        [ActionName("Page01")]
        public ActionResult Page01Post()
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page01ViewData viewData = new Page01ViewData();
            this.SetBaseViewData((BaseViewData)viewData);
            try
            {
                TournamentRegistration newRegistration = new TournamentRegistration()
                {
                    TimeRegistrationStarted = new DateTime?(DateTime.Now),
                    TeamRegistrationFee = viewData.TeamRegistrationFee,
                    UserAgent = this.Request.UserAgent
                };
                this.Repository.AddTournamentRegistration(newRegistration);
                return (ActionResult)this.RedirectToAction("Page02", (object)new
                {
                    id = newRegistration.TeamID
                });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page02(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page02ViewData page02ViewData = new Page02ViewData()
            {
                SchoolList = (IEnumerable<SelectListItem>)new SelectList(this.Repository.Schools, "ID", "Name")
            };
            this.SetBaseViewData((BaseViewData)page02ViewData);
            return (ActionResult)this.View((object)page02ViewData);
        }

        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration()
                {
                    SchoolID = new int?(page02ViewData.SelectedSchool)
                };
                this.Repository.UpdateTournamentRegistration(id, 2, newRegistrationData);
                return (ActionResult)this.RedirectToAction("Page03", (object)new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page03(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page03ViewData page03ViewData = new Page03ViewData();
            this.SetBaseViewData((BaseViewData)page03ViewData);
            page03ViewData.NoJudgesFound = false;
            return (ActionResult)this.View((object)page03ViewData);
        }

        [HttpPost]
        public ActionResult Page03(int id, Page03ViewData page03ViewData)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                this.SetBaseViewData((BaseViewData)page03ViewData);
                this.UpdateModel<Page03ViewData>(page03ViewData);
                page03ViewData.ListOfJudgesFound = this.Repository.GetJudgeByIdAndName(int.Parse(page03ViewData.JudgeId), page03ViewData.JudgeFirstName, page03ViewData.JudgeLastName);
                if (!page03ViewData.ListOfJudgesFound.Any<Judge>())
                {
                    page03ViewData.NoJudgesFound = true;
                    return (ActionResult)this.View((object)page03ViewData);
                }
                if (page03ViewData.ListOfJudgesFound.First<Judge>() != null && page03ViewData.ListOfJudgesFound.First<Judge>().TeamID != null)
                {
                    page03ViewData.JudgeAlreadyTaken = true;
                    return (ActionResult)this.View((object)page03ViewData);
                }
                TournamentRegistration newRegistrationData = new TournamentRegistration()
                {
                    JudgeID = new short?(short.Parse(page03ViewData.JudgeId))
                };
                this.Repository.UpdateTournamentRegistration(id, 3, newRegistrationData);
                return (ActionResult)this.RedirectToAction("Page05", (object)new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page04(int? id)
        {
            if (!id.HasValue)
                return (ActionResult)this.RedirectToAction("Error");
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page04ViewData page04ViewData = new Page04ViewData();
            this.SetBaseViewData((BaseViewData)page04ViewData);
            page04ViewData.NoVolunteersFound = false;
            return (ActionResult)this.View((object)page04ViewData);
        }

        [HttpPost]
        public ActionResult Page04(int? id, Page04ViewData page04ViewData)
        {
            if (!id.HasValue)
                return (ActionResult)this.RedirectToAction("Error");
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                this.SetBaseViewData((BaseViewData)page04ViewData);
                this.UpdateModel<Page04ViewData>(page04ViewData);

                // TODO: Did I comment out too much?
                //page04ViewData.VolunteerFound = this.Repository.GetVolunteerByIdAndName(int.Parse(page04ViewData.VolunteerId), page04ViewData.VolunteerFirstName, page04ViewData.VolunteerLastName);
                //if (page04ViewData.VolunteerFound == null)
                //{
                //    page04ViewData.NoVolunteersFound = true;
                //    return (ActionResult)this.View((object)page04ViewData);
                //}
                //if (page04ViewData.VolunteerFound.TeamID.HasValue)
                //{
                //    page04ViewData.VolunteerAlreadyTaken = true;
                //    return (ActionResult)this.View((object)page04ViewData);
                //}
                //TournamentRegistration newRegistrationData = new TournamentRegistration()
                //{
                //    VolunteerID = new int?(page04ViewData.VolunteerFound.VolunteerID)
                //};
                //this.Repository.UpdateTournamentRegistration(id.Value, 4, newRegistrationData);
                return (ActionResult)this.RedirectToAction("Page05", (object)new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Page05(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page05ViewData page05ViewData = new Page05ViewData();
            this.SetBaseViewData((BaseViewData)page05ViewData);
            return (ActionResult)this.View((object)page05ViewData);
        }

        [HttpPost]
        public ActionResult Page05(int id, Page05ViewData page05ViewData)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                this.UpdateModel<Page05ViewData>(page05ViewData);
                this.SetBaseViewData((BaseViewData)page05ViewData);
                if (this.BuildMessage(page05ViewData.Config["WebmasterEmail"], "test", "test", page05ViewData.CoachEmailAddress, (string)null, (string)null) == null)
                    return (ActionResult)this.RedirectToAction("BadCoachEmail");
                if (this.BuildMessage(page05ViewData.Config["WebmasterEmail"], "test", "test", page05ViewData.AltCoachEmailAddress, (string)null, (string)null) == null)
                    return (ActionResult)this.RedirectToAction("BadAltCoachEmail");
                TournamentRegistration newRegistrationData = new TournamentRegistration()
                {
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
                this.Repository.UpdateTournamentRegistration(id, 5, newRegistrationData);
                return (ActionResult)this.RedirectToAction("Page06", (object)new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page06(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            List<string> items = new List<string>()
      {
        "Kindergarten"
      };
            for (int index = 1; index <= 12; ++index)
                items.Add(index.ToString((IFormatProvider)CultureInfo.InvariantCulture));
            Page06ViewData page06ViewData = new Page06ViewData()
            {
                GradeChoices = (IEnumerable<SelectListItem>)new SelectList((IEnumerable)items)
            };
            this.SetBaseViewData((BaseViewData)page06ViewData);
            return (ActionResult)this.View((object)page06ViewData);
        }

        [HttpPost]
        public ActionResult Page06(int id, Page06ViewData page06ViewData)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                this.UpdateModel<Page06ViewData>(page06ViewData);
                TournamentRegistration newRegistrationData = new TournamentRegistration()
                {
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
                this.Repository.UpdateTournamentRegistration(id, 6, newRegistrationData);
                return (ActionResult)this.RedirectToAction("Page07", (object)new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page07(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            List<string> gradesByRegistration = this.Repository.GetMemberGradesByRegistration(id);
            Page07ViewData page07ViewData = new Page07ViewData()
            {
                DivisionOfTeam = this.DetermineDivisionOfTeam(gradesByRegistration),
                Division123ProblemDropDown = (IEnumerable<SelectListItem>)new SelectList((IEnumerable)this.Repository.ProblemsWithoutPrimaryOrSpontaneous, "ProblemID", "ProblemName"),
                IsDoingSpontaneousDropDown = (IEnumerable<SelectListItem>)new SelectList((IEnumerable)new List<string>()
        {
          "Yes",
          "No"
        }),
                Division123ListOfProblemsAsHtmlList = this.GetProblemsAsHtmlList(false),
                Division123AndPrimaryListOfProblemsAsHtmlList = this.GetProblemsAsHtmlList(true),
                PrimaryProblemName = this.Repository.PrimaryProblem.First<Problem>().ProblemName
            };
            this.SetBaseViewData((BaseViewData)page07ViewData);
            return (ActionResult)this.View((object)page07ViewData);
        }

        [HttpPost]
        public ActionResult Page07(int id, Page07ViewData page07ViewData)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration()
                {
                    Division = page07ViewData.DivisionRadioGroup ?? page07ViewData.DivisionOfTeam.ToString((IFormatProvider)CultureInfo.InvariantCulture)
                };
                string divisionRadioGroup = page07ViewData.DivisionRadioGroup;
                if (divisionRadioGroup != null)
                {
                    if (divisionRadioGroup != "0")
                    {
                        if (divisionRadioGroup == "1")
                        {
                            newRegistrationData.ProblemID = new int?(int.Parse(page07ViewData.Division123ProblemChoice));
                            goto label_8;
                        }
                    }
                    else
                    {
                        newRegistrationData.ProblemID = new int?(6);
                        newRegistrationData.Spontaneous = new bool?(page07ViewData.IsDoingSpontaneous == "Yes");
                        goto label_8;
                    }
                }
                newRegistrationData.ProblemID = new int?(int.Parse(page07ViewData.SelectedProblem));
            label_8:
                this.Repository.UpdateTournamentRegistration(id, 7, newRegistrationData);
                return (ActionResult)this.RedirectToAction("Page08", (object)new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page08(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page08ViewData page08ViewData = new Page08ViewData();
            this.SetBaseViewData((BaseViewData)page08ViewData);
            return (ActionResult)this.View((object)page08ViewData);
        }

        [HttpPost]
        public ActionResult Page08(int id, Page08ViewData page08ViewData)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration()
                {
                    SchedulingIssues = page08ViewData.SchedulingIssues,
                    SpecialConsiderations = page08ViewData.SpecialConsiderations
                };
                this.Repository.UpdateTournamentRegistration(id, 8, newRegistrationData);
                return (ActionResult)this.RedirectToAction("Page09", (object)new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return (ActionResult)this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page09(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            Page09ViewData page09ViewData1 = new Page09ViewData()
            {
                TournamentRegistration = this.Repository.GetTournamentRegistrationById(id)
            };
            this.SetBaseViewData((BaseViewData)page09ViewData1);
            page09ViewData1.SchoolName = this.Repository.GetSchoolNameFromSchoolId(page09ViewData1.TournamentRegistration.SchoolID);
            string judgeFirstName;
            string judgeLastName;
            this.Repository.GetJudgeNameFromJudgeId(page09ViewData1.TournamentRegistration.JudgeID, out judgeFirstName, out judgeLastName);
            page09ViewData1.JudgeFirstName = judgeFirstName;
            page09ViewData1.JudgeLastName = judgeLastName;
            page09ViewData1.Division = page09ViewData1.TournamentRegistration.Division == "0" ? "Primary" : page09ViewData1.TournamentRegistration.Division;
            page09ViewData1.ProblemName = this.Repository.GetProblemNameFromProblemId(page09ViewData1.TournamentRegistration.ProblemID);
            bool? spontaneous = page09ViewData1.TournamentRegistration.Spontaneous;
            if (spontaneous.HasValue)
            {
                Page09ViewData page09ViewData2 = page09ViewData1;
                spontaneous = page09ViewData1.TournamentRegistration.Spontaneous;
                string str = spontaneous.Value ? "Yes" : "No";
                page09ViewData2.IsDoingSpontaneous = str;
            }
            if (string.IsNullOrEmpty(page09ViewData1.ProblemName))
                page09ViewData1.ProblemName = "(Could not obtain problem name)";
            return (ActionResult)this.View((object)page09ViewData1);
        }

        [HttpPost]
        public ActionResult Page09(
          int id,
          string homePageButton,
          string nextButton,
          FormCollection collection)
        {
            return this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available ? (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString()) : (ActionResult)this.RedirectToAction("Page10", (object)new
            {
                id = id
            });
        }

        [HttpGet]
        public ActionResult Page10(int id)
        {
            if (this.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
                return (ActionResult)this.RedirectToAction(this.CurrentRegistrationState.ToString());
            short? tournamentRegistrationId1 = this.Repository.GetJudgeIdFromTournamentRegistrationId(id);
            short? nullable = tournamentRegistrationId1;
            string errorMessage;
            if ((nullable.HasValue ? new int?((int)nullable.GetValueOrDefault()) : new int?()).HasValue)
            {
                this.Repository.UpdateJudgeRecordWithTournamentRegistrationId(tournamentRegistrationId1, id, out errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Page10ViewData page10ViewData = new Page10ViewData()
                    {
                        JudgeErrorMessage = errorMessage
                    };
                    this.SetBaseViewData((BaseViewData)page10ViewData);
                    MailMessage mailMessage = this.BuildMessage(page10ViewData.Config["WebmasterEmail"], "Error: " + page10ViewData.RegionName + " Odyssey Region " + page10ViewData.RegionNumber + " Tournament Registration", "<p>Team with ID # " + (object)id + " attempted to re-register after its judge was assigned to the team.</p><p>" + errorMessage + "</p>", page10ViewData.Config["WebmasterEmail"], (string)null, (string)null);
                    page10ViewData.MailErrorMessage = this.SendMessage((BaseViewData)page10ViewData, mailMessage);
                    return (ActionResult)this.View((object)page10ViewData);
                }
            }

            // TODO: Did I comment out too much?
            //int? tournamentRegistrationId2 = this.Repository.GetVolunteerIdFromTournamentRegistrationId(id);
            //if (tournamentRegistrationId2.HasValue)
            //{
            //    this.Repository.UpdateVolunteerRecordWithTournamentRegistrationId(tournamentRegistrationId2.Value, id, out errorMessage);
            //    if (!string.IsNullOrEmpty(errorMessage))
            //    {
            //        Page10ViewData page10ViewData = new Page10ViewData()
            //        {
            //            VolunteerErrorMessage = errorMessage
            //        };
            //        this.SetBaseViewData((BaseViewData)page10ViewData);
            //        MailMessage mailMessage = this.BuildMessage(page10ViewData.Config["WebmasterEmail"], "Error: " + page10ViewData.RegionName + " Odyssey Region " + page10ViewData.RegionNumber + " Tournament Registration", "<p>Team with ID # " + (object)id + " attempted to re-register after its volunteer was assigned to the team.</p><p>" + errorMessage + "</p>", page10ViewData.Config["WebmasterEmail"], (string)null, (string)null);
            //        page10ViewData.MailErrorMessage = this.SendMessage((BaseViewData)page10ViewData, mailMessage);
            //        return (ActionResult)this.View((object)page10ViewData);
            //    }
            //}

            Page10ViewData page10ViewData1 = new Page10ViewData();
            page10ViewData1.TournamentInfo = this.Repository.TournamentInfo;
            page10ViewData1.TournamentRegistration = this.Repository.GetTournamentRegistrationById(id);
            Page10ViewData page10ViewData2 = page10ViewData1;
            page10ViewData2.TournamentRegistration.TimeRegistered = new DateTime?(DateTime.Now);
            this.Repository.UpdateTournamentRegistration(id, 10, page10ViewData2.TournamentRegistration);
            this.SetBaseViewData((BaseViewData)page10ViewData2);
            string judgeFirstName;
            string judgeLastName;
            this.Repository.GetJudgeNameFromJudgeId(tournamentRegistrationId1, out judgeFirstName, out judgeLastName);
            page10ViewData2.JudgeFirstName = judgeFirstName;
            page10ViewData2.JudgeLastName = judgeLastName;
            page10ViewData2.SchoolName = this.Repository.GetSchoolNameFromSchoolId(page10ViewData2.TournamentRegistration.SchoolID);
            page10ViewData2.ProblemName = this.Repository.GetProblemNameFromProblemId(page10ViewData2.TournamentRegistration.ProblemID);
            page10ViewData2.Division = page10ViewData2.TournamentRegistration.Division == "0" ? "Primary" : page10ViewData2.TournamentRegistration.Division;
            page10ViewData2.MailBody = this.GenerateEmailBody(page10ViewData2);
            MailMessage mailMessage1 = this.BuildMessage(page10ViewData2.Config["WebmasterEmail"], page10ViewData2.RegionName + " Odyssey Region " + page10ViewData2.RegionNumber + " Tournament Registration", page10ViewData2.MailBody, page10ViewData2.TournamentRegistration.CoachEmailAddress, (string)null, (string)null);
            page10ViewData2.MailErrorMessage = this.SendMessage((BaseViewData)page10ViewData2, mailMessage1);
            return (ActionResult)this.View((object)page10ViewData2);
        }

        [HttpGet]
        public ActionResult ResendEmail()
        {
            ResendEmailViewData resendEmailViewData = new ResendEmailViewData();
            this.SetBaseViewData((BaseViewData)resendEmailViewData);
            return (ActionResult)this.View((object)resendEmailViewData);
        }

        [HttpPost]
        public ActionResult ResendEmail(FormCollection collection)
        {
            ResendEmailViewData resendEmailViewData = new ResendEmailViewData();
            this.UpdateModel<ResendEmailViewData>(resendEmailViewData);
            Page10ViewData page10ViewData1 = new Page10ViewData();
            page10ViewData1.TournamentInfo = this.Repository.TournamentInfo;
            page10ViewData1.TournamentRegistration = this.Repository.GetTournamentRegistrationById(resendEmailViewData.TeamNumber);
            Page10ViewData page10ViewData2 = page10ViewData1;
            this.SetBaseViewData((BaseViewData)page10ViewData2);
            if (resendEmailViewData.CoachCheckbox == "false" && resendEmailViewData.AltCoachCheckbox == "false")
            {
                resendEmailViewData.ErrorMessage = "No one was selected to resend the registration information to, so no e-mail was sent.";
                resendEmailViewData.Success = false;
                return (ActionResult)this.View((object)resendEmailViewData);
            }
            string empty = string.Empty;
            if (resendEmailViewData.CoachCheckbox == "true")
                empty += page10ViewData2.TournamentRegistration.CoachEmailAddress;
            if (resendEmailViewData.AltCoachCheckbox == "true")
            {
                if (!string.IsNullOrEmpty(empty))
                    empty += ",";
                empty += page10ViewData2.TournamentRegistration.AltCoachEmailAddress;
            }
            string judgeFirstName;
            string judgeLastName;
            this.Repository.GetJudgeNameFromJudgeId(page10ViewData2.TournamentRegistration.JudgeID, out judgeFirstName, out judgeLastName);
            page10ViewData2.JudgeFirstName = judgeFirstName;
            page10ViewData2.JudgeLastName = judgeLastName;
            page10ViewData2.SchoolName = this.Repository.GetSchoolNameFromSchoolId(page10ViewData2.TournamentRegistration.SchoolID);
            page10ViewData2.ProblemName = this.Repository.GetProblemNameFromProblemId(page10ViewData2.TournamentRegistration.ProblemID);
            page10ViewData2.MailBody = this.GenerateEmailBody(page10ViewData2);
            MailMessage mailMessage = this.BuildMessage(page10ViewData2.Config["WebmasterEmail"], page10ViewData2.RegionName + " Odyssey Region " + page10ViewData2.RegionNumber + " Tournament Registration", page10ViewData2.MailBody, empty, "rob@tardistech.com", (string)null);
            resendEmailViewData.ErrorMessage = this.SendMessage((BaseViewData)resendEmailViewData, mailMessage);
            resendEmailViewData.Success = true;
            return (ActionResult)this.View((object)resendEmailViewData);
        }

        public ActionResult Carolina()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(38, nameof(Carolina), "Deschapelles");
            return (ActionResult)this.RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Joyce()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(30, nameof(Joyce), "Ghen");
            return (ActionResult)this.RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Margaret()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(17, nameof(Margaret), "Eccles");
            return (ActionResult)this.RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Rob()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(5, nameof(Rob), "Bernstein");
            return (ActionResult)this.RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Ron()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(31, nameof(Ron), "Ghen");
            return (ActionResult)this.RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Sarah()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(16, nameof(Sarah), "Tate");
            return (ActionResult)this.RedirectToAction("Page01", "TournamentRegistration");
        }

        private string GenerateEmailBody(Page10ViewData page10ViewData)
        {
            using (StringWriter writer = new StringWriter())
            {
                this.ViewData.Model = (object)page10ViewData;
                ViewEngineResult partialView = ViewEngines.Engines.FindPartialView(this.ControllerContext, "TournamentRegistration/EmailPartial");
                ViewContext viewContext = new ViewContext(this.ControllerContext, partialView.View, this.ViewData, this.TempData, (TextWriter)writer);
                partialView.View.Render(viewContext, (TextWriter)writer);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
