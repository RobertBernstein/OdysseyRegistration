// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2025 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Controller responsible for handling tournament registration process for Odyssey of the Mind competitions.
//   Manages a multi-step registration flow from initial sign up through confirmation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.TournamentRegistrationController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData;
using OdysseyMvc2024.ViewData.TournamentRegistration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ElmahCore;

namespace OdysseyMvc2024.Controllers
{
    /// <summary>
    /// Controller responsible for handling tournament registration process for Odyssey of the Mind competitions.
    /// Manages a multi-step registration flow from initial sign up through confirmation.
    /// </summary>
    public class TournamentRegistrationController : BaseRegistrationController
    {
        /// <summary>
        /// Initializes a new instance of the TournamentRegistrationController.
        /// </summary>
        /// <param name="context">
        /// Database context for Odyssey entities
        /// </param>
        // [SetsRequiredMembers] tells the compiler that this constructor initializes all 'required' members
        // (specifically FriendlyRegistrationName from BaseRegistrationController). Without this attribute,
        // external assemblies (such as the unit test project) cannot instantiate this class because the
        // compiler enforces that callers set 'required' properties — even though this constructor already
        // does so internally.
        [SetsRequiredMembers]
        public TournamentRegistrationController(IOdysseyRepository repository)
            : base(repository)
        {
            // Set the registration type to Tournament and initialize the friendly name
            CurrentRegistrationType = BaseRegistrationController.RegistrationType.Tournament;
            FriendlyRegistrationName = GetFriendlyRegistrationName();
        }

        /// <summary>
        /// Returns an error view when the alternate coach's email address is invalid.
        /// This action is called during the registration validation process.
        /// </summary>
        /// <returns>View for invalid alternate coach email</returns>
        public ActionResult BadAltCoachEmail() => (ActionResult)View();

        /// <summary>
        /// Returns an error view when the primary coach's email address is invalid.
        /// </summary>
        /// <returns>
        /// View for invalid primary coach email.
        /// </returns>
        public ActionResult BadCoachEmail() => (ActionResult)View();

        /// <summary>
        /// Determines the division of the team based on the grades of its members.
        /// </summary>
        /// <param name="gradesOfTeamMembers">
        /// List of grades for each team member, where each grade is a string.
        /// </param>
        /// <returns>
        /// The highest division number found among team members.
        /// </returns>
        public int DetermineDivisionOfTeam(List<string> gradesOfTeamMembers)
        {
            int divisionOfTeam = -1;
            foreach (string gradeOfTeamMember in gradesOfTeamMembers)
            {
                if (!string.IsNullOrEmpty(gradeOfTeamMember))
                {
                    int divisionOfTeamMember = GetDivisionOfTeamMember(gradeOfTeamMember);
                    if (divisionOfTeamMember > divisionOfTeam)
                    {
                        divisionOfTeam = divisionOfTeamMember;
                    }
                }
            }
            return divisionOfTeam;
        }

        /// <summary>
        /// Gets the division of a team member based on their grade.
        /// </summary>
        /// <param name="memberGrade">
        /// The grade of the team member.
        /// </param>
        /// <returns>
        /// The division number for the team member.
        /// </returns>
        public int GetDivisionOfTeamMember(string memberGrade)
        {
            // Determine member's grade in school; Kindergarten is grade 0.
            int division = memberGrade == "Kindergarten" ? 0 : int.Parse(memberGrade);

            // Return division based on the grade, with special handling for grades 0-2, i.e., Primary.
            // divisionOfTeamMember = 0 when the team member is Primary, divisionOfTeamMember = 1, 2, or 3 when the
            // team member is Division 1, 2, or 3.
            return division < 0 || division > 2 ? (division <= 5 ? 1 : (division <= 8 ? 2 : 3)) : 0;
        }

        /// <summary>
        /// Generates an HTML list of the Odyssey problems for the tournament registration.
        /// </summary>
        /// <param name="thisTeamIsPrimary">
        /// Indicates if the current team is the primary team.
        /// </param>
        /// <returns>
        /// Formatted HTML list of problems.
        /// </returns>
        public string GetProblemsAsHtmlList(bool thisTeamIsPrimary)
        {
            IQueryable<Problem>? problemsWithoutPrimaryOrSpontaneous = Repository.ProblemsWithoutPrimaryOrSpontaneous;
            StringBuilder stringBuilder = new();

            stringBuilder.Append("<ol>\n");

            if (thisTeamIsPrimary)
            {
                stringBuilder.Append("<li>" + Repository.PrimaryProblem.First<Problem>().ProblemName + " (The Primary Problem)</li>\n");
            }

            foreach (Problem problem in (IEnumerable<Problem>)problemsWithoutPrimaryOrSpontaneous)
            {
                stringBuilder.Append("<li>" + problem.ProblemName + "</li>\n");
            }

            stringBuilder.Append("</ol>\n");

            return stringBuilder.ToString();
        }

        [HttpGet]
        public ActionResult Index() => (ActionResult)RedirectToAction("Page01");

        [HttpGet]
        public ActionResult Page01()
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page01ViewData page01ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(page01ViewData);
            return View(page01ViewData);
        }

        [HttpPost]
        [ActionName("Page01")]
        public ActionResult Page01Post()
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page01ViewData viewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(viewData);
            
            try
            {
                TournamentRegistration newRegistration = new()
                {
                    TimeRegistrationStarted = new DateTime?(DateTime.Now),
                    TeamRegistrationFee = viewData.TeamRegistrationFee,
                    UserAgent = Request.Headers["User-Agent"].ToString()
                };

                Repository.AddTournamentRegistration(newRegistration);
                
                return RedirectToAction("Page02", new
                {
                    id = newRegistration.Id
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
            {
                return (ActionResult)RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page02ViewData page02ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                SchoolList = (IEnumerable<SelectListItem>)new SelectList(Repository.Schools, "ID", "Name")
            };

            SetBaseViewData((BaseViewData)page02ViewData);
            return (ActionResult)View((object)page02ViewData);
        }

        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return (ActionResult)RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                TournamentRegistration? newRegistrationData = new TournamentRegistration()
                {
                    SchoolID = new int?(page02ViewData.SelectedSchool)
                };
                Repository.UpdateTournamentRegistration(id, 2, newRegistrationData);
                return (ActionResult)RedirectToAction("Page03", (object)new
                {
                    id = id
                });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return (ActionResult)RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page03(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page03ViewData page03ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                ListOfJudgesFound = new List<Judge>().AsQueryable()
            };

            SetBaseViewData(page03ViewData);
            page03ViewData.NoJudgesFound = false;
            
            return View(page03ViewData);
        }

        [HttpPost]
        public ActionResult Page03(int id, Page03ViewData page03ViewData)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                SetBaseViewData((BaseViewData)page03ViewData);
                TryUpdateModelAsync<Page03ViewData>(page03ViewData);
                page03ViewData.ListOfJudgesFound = Repository.GetJudgeByIdAndName(int.Parse(page03ViewData.JudgeId), page03ViewData.JudgeFirstName, page03ViewData.JudgeLastName);
                
                if (!page03ViewData.ListOfJudgesFound.Any<Judge>())
                {
                    page03ViewData.NoJudgesFound = true;
                    return View(page03ViewData);
                }
                
                if (page03ViewData.ListOfJudgesFound.First<Judge>() != null && page03ViewData.ListOfJudgesFound.First<Judge>().TeamID != null)
                {
                    page03ViewData.JudgeAlreadyTaken = true;
                    return View(page03ViewData);
                }
                
                TournamentRegistration? newRegistrationData = new TournamentRegistration()
                {
                    JudgeID = new short?(short.Parse(page03ViewData.JudgeId))
                };
                
                Repository.UpdateTournamentRegistration(id, 3, newRegistrationData);
                
                return RedirectToAction("Page05", new
                {
                    id
                });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page04(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Error");
            }

            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page04ViewData page04ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(page04ViewData);
            page04ViewData.NoVolunteersFound = false;
            
            return View(page04ViewData);
        }

        [HttpPost]
        public ActionResult Page04(int? id, Page04ViewData page04ViewData)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Error");
            }

            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                SetBaseViewData(page04ViewData);
                TryUpdateModelAsync<Page04ViewData>(page04ViewData);

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
                
                return RedirectToAction("Page05", new
                {
                    id
                });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Page05(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page05ViewData page05ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(page05ViewData);
            
            return View(page05ViewData);
        }

        [HttpPost]
        public ActionResult Page05(int id, Page05ViewData page05ViewData)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                TryUpdateModelAsync<Page05ViewData>(page05ViewData);
                SetBaseViewData(page05ViewData);
                
                if (BuildMessage(page05ViewData.Config["WebmasterEmail"], "test", "test", page05ViewData.CoachEmailAddress, (string)null, (string)null) == null)
                {
                    return RedirectToAction("BadCoachEmail");
                }

                if (BuildMessage(page05ViewData.Config["WebmasterEmail"], "test", "test", page05ViewData.AltCoachEmailAddress, (string)null, (string)null) == null)
                {
                    return RedirectToAction("BadAltCoachEmail");
                }
                
                TournamentRegistration? newRegistrationData = new TournamentRegistration()
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
                
                Repository.UpdateTournamentRegistration(id, 5, newRegistrationData);
                
                return RedirectToAction("Page06", new
                {
                    id
                });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page06(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }
            
            List<string> items = ["Kindergarten"];

            for (int index = 1; index <= 12; ++index)
            {
                items.Add(index.ToString((IFormatProvider)CultureInfo.InvariantCulture));
            }

            Page06ViewData page06ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                GradeChoices = (IEnumerable<SelectListItem>)new SelectList((IEnumerable)items)
            };

            SetBaseViewData(page06ViewData);
            
            return View(page06ViewData);
        }

        [HttpPost]
        public ActionResult Page06(int id, Page06ViewData page06ViewData)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                TryUpdateModelAsync<Page06ViewData>(page06ViewData);

                TournamentRegistration? newRegistrationData = new()
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
                
                Repository.UpdateTournamentRegistration(id, 6, newRegistrationData);
                
                return RedirectToAction("Page07", new
                {
                    id
                });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page07(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            List<string> gradesByRegistration = Repository.GetMemberGradesByRegistration(id);

            Page07ViewData page07ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                DivisionOfTeam = DetermineDivisionOfTeam(gradesByRegistration),

                // TODO: Should this be initialized like this here?  Is there a better option?
                Division123ProblemDropDown = new SelectList(Repository.ProblemsWithoutPrimaryOrSpontaneous, "ProblemID", "ProblemName"),
                IsDoingSpontaneousDropDown = new SelectList(new List<string>()
                {
                  "Yes",
                  "No"
                }),

                Division123ListOfProblemsAsHtmlList = GetProblemsAsHtmlList(false),
                Division123AndPrimaryListOfProblemsAsHtmlList = GetProblemsAsHtmlList(true),
                PrimaryProblemName = Repository.PrimaryProblem.First<Problem>().ProblemName
            };

            SetBaseViewData(page07ViewData);
            
            return View(page07ViewData);
        }

        [HttpPost]
        public ActionResult Page07(int id, Page07ViewData page07ViewData)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                TournamentRegistration? newRegistrationData = new()
                {
                    Division = page07ViewData.DivisionRadioGroup ?? page07ViewData.DivisionOfTeam.ToString(CultureInfo.InvariantCulture)
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
            
            // TODO: What is this label here for?
            label_8:
                Repository.UpdateTournamentRegistration(id, 7, newRegistrationData);
                return RedirectToAction("Page08", new { id });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page08(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page08ViewData page08ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                TournamentRegistration = Repository.GetTournamentRegistrationById(id)
            };

            SetBaseViewData(page08ViewData);
            
            return View(page08ViewData);
        }

        [HttpPost]
        public ActionResult Page08(int id, Page08ViewData page08ViewData)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                TournamentRegistration? newRegistrationData = new TournamentRegistration()
                {
                    SchedulingIssues = page08ViewData.SchedulingIssues,
                    SpecialConsiderations = page08ViewData.SpecialConsiderations
                };
                Repository.UpdateTournamentRegistration(id, 8, newRegistrationData);
                return RedirectToAction("Page09", new
                {
                    id
                });
            }
            catch (Exception exception)
            {
                ElmahExtensions.RaiseError(exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Page09(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return (ActionResult)RedirectToAction(CurrentRegistrationState.ToString());
            }

            Page09ViewData page09ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                TournamentRegistration = Repository.GetTournamentRegistrationById(id)
            };

            SetBaseViewData(page09ViewData);
            page09ViewData.SchoolName = Repository.GetSchoolNameFromSchoolId(page09ViewData.TournamentRegistration.SchoolID);
            string judgeFirstName;
            string judgeLastName;
            Repository.GetJudgeNameFromJudgeId(page09ViewData.TournamentRegistration.JudgeID, out judgeFirstName, out judgeLastName);
            page09ViewData.JudgeFirstName = judgeFirstName;
            page09ViewData.JudgeLastName = judgeLastName;
            page09ViewData.Division = page09ViewData.TournamentRegistration.Division == "0" ? "Primary" : page09ViewData.TournamentRegistration.Division;
            page09ViewData.ProblemName = Repository.GetProblemNameFromProblemId(page09ViewData.TournamentRegistration.ProblemID);
            
            bool? spontaneous = page09ViewData.TournamentRegistration.Spontaneous;
            if (spontaneous.HasValue)
            {
                Page09ViewData page09ViewData2 = page09ViewData;
                spontaneous = page09ViewData.TournamentRegistration.Spontaneous;
                string str = spontaneous.Value ? "Yes" : "No";
                page09ViewData2.IsDoingSpontaneous = str;
            }

            if (string.IsNullOrEmpty(page09ViewData.ProblemName))
            {
                page09ViewData.ProblemName = "(Could not obtain problem name)";
            }

            return View(page09ViewData);
        }

        [HttpPost]
        public ActionResult Page09(
          int id,
          string homePageButton,
          string nextButton,
          FormCollection collection)
        {
            return CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available ? (ActionResult)RedirectToAction(CurrentRegistrationState.ToString()) : (ActionResult)RedirectToAction("Page10", (object)new
            {
                id
            });
        }

        [HttpGet]
        public ActionResult Page10(int id)
        {
            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            short? tournamentRegistrationId1 = Repository.GetJudgeIdFromTournamentRegistrationId(id);
            short? nullable = tournamentRegistrationId1;

            Page10ViewData page10ViewData = new()
            {
                // TODO: (Rob) Is it okay to comment the following two lines out since they will be populated by SetBaseViewData, below?
                // Config = Repository.Config,
                // TournamentInfo = Repository.TournamentInfo,
                TournamentRegistration = Repository.GetTournamentRegistrationById(id)
            };

            SetBaseViewData(page10ViewData);

            if ((nullable.HasValue ? new int?(nullable.GetValueOrDefault()) : new int?()).HasValue)
            {
                Repository.UpdateJudgeRecordWithTournamentRegistrationId(tournamentRegistrationId1, id, out string errorMessage);
                
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    page10ViewData.JudgeErrorMessage = errorMessage;

                    MailMessage mailMessage = BuildMessage(page10ViewData.Config["WebmasterEmail"], "Error: " + page10ViewData.RegionName + " Odyssey Region " + page10ViewData.RegionNumber + " Tournament Registration", "<p>Team with ID # " + (object)id + " attempted to re-register after its judge was assigned to the team.</p><p>" + errorMessage + "</p>", page10ViewData.Config["WebmasterEmail"], (string)null, (string)null);
                    page10ViewData.MailErrorMessage = SendMessage((BaseViewData)page10ViewData, mailMessage);
                    
                    return View(page10ViewData);
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

            // TODO: (Rob) Do we still need this?
            //Page10ViewData page10ViewData2 = page10ViewData;

            page10ViewData.TournamentRegistration.TimeRegistered = new DateTime?(DateTime.Now);
            Repository.UpdateTournamentRegistration(id, 10, page10ViewData.TournamentRegistration);

            // TODO: (Rob) Do we still need this?
            // SetBaseViewData((BaseViewData)page10ViewData);

            Repository.GetJudgeNameFromJudgeId(tournamentRegistrationId1, out string judgeFirstName, out string judgeLastName);
            page10ViewData.JudgeFirstName = judgeFirstName;
            page10ViewData.JudgeLastName = judgeLastName;
            page10ViewData.SchoolName = Repository.GetSchoolNameFromSchoolId(page10ViewData.TournamentRegistration.SchoolID);
            page10ViewData.ProblemName = Repository.GetProblemNameFromProblemId(page10ViewData.TournamentRegistration.ProblemID);
            page10ViewData.Division = page10ViewData.TournamentRegistration.Division == "0" ? "Primary" : page10ViewData.TournamentRegistration.Division;
            page10ViewData.MailBody = GenerateEmailBody(page10ViewData);
            MailMessage mailMessage1 = BuildMessage(page10ViewData.Config["WebmasterEmail"], page10ViewData.RegionName + " Odyssey Region " + page10ViewData.RegionNumber + " Tournament Registration", page10ViewData.MailBody, page10ViewData.TournamentRegistration.CoachEmailAddress, (string)null, (string)null);
            page10ViewData.MailErrorMessage = SendMessage((BaseViewData)page10ViewData, mailMessage1);
            
            return View(page10ViewData);
        }

        [HttpGet]
        public ActionResult ResendEmail()
        {
            ResendEmailViewData resendEmailViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(resendEmailViewData);
            
            return View(resendEmailViewData);
        }

        [HttpPost]
        public ActionResult ResendEmail(FormCollection collection)
        {
            ResendEmailViewData resendEmailViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            TryUpdateModelAsync<ResendEmailViewData>(resendEmailViewData);

            Page10ViewData page10ViewData = new()
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,

                // TODO: Is this the right parameter value?
                TournamentRegistration = Repository.GetTournamentRegistrationById(resendEmailViewData.TeamNumber)
            };

            page10ViewData.TournamentInfo = Repository.TournamentInfo;
            page10ViewData.TournamentRegistration = Repository.GetTournamentRegistrationById(resendEmailViewData.TeamNumber);
            
            Page10ViewData page10ViewData2 = page10ViewData;
            SetBaseViewData(page10ViewData2);
            
            if (resendEmailViewData.CoachCheckbox == "false" && resendEmailViewData.AltCoachCheckbox == "false")
            {
                resendEmailViewData.ErrorMessage = "No one was selected to resend the registration information to, so no e-mail was sent.";
                resendEmailViewData.Success = false;
                
                return View(resendEmailViewData);
            }
            string empty = string.Empty;
            if (resendEmailViewData.CoachCheckbox == "true")
            {
                empty += page10ViewData2.TournamentRegistration.CoachEmailAddress;
            }

            if (resendEmailViewData.AltCoachCheckbox == "true")
            {
                if (!string.IsNullOrEmpty(empty))
                {
                    empty += ",";
                }

                empty += page10ViewData2.TournamentRegistration.AltCoachEmailAddress;
            }

            string judgeFirstName;
            string judgeLastName;
            Repository.GetJudgeNameFromJudgeId(page10ViewData2.TournamentRegistration.JudgeID, out judgeFirstName, out judgeLastName);
            page10ViewData2.JudgeFirstName = judgeFirstName;
            page10ViewData2.JudgeLastName = judgeLastName;
            page10ViewData2.SchoolName = Repository.GetSchoolNameFromSchoolId(page10ViewData2.TournamentRegistration.SchoolID);
            page10ViewData2.ProblemName = Repository.GetProblemNameFromProblemId(page10ViewData2.TournamentRegistration.ProblemID);
            page10ViewData2.MailBody = GenerateEmailBody(page10ViewData2);

            MailMessage mailMessage = BuildMessage(page10ViewData2.Config["WebmasterEmail"], page10ViewData2.RegionName + " Odyssey Region " + page10ViewData2.RegionNumber + " Tournament Registration", page10ViewData2.MailBody, empty, "rob@tardistech.com", (string)null);
            resendEmailViewData.ErrorMessage = SendMessage((BaseViewData)resendEmailViewData, mailMessage);
            resendEmailViewData.Success = true;

            return View(resendEmailViewData);
        }

        public ActionResult Carolina()
        {
            Repository.ClearTeamIdFromJudgeRecord(38, nameof(Carolina), "Deschapelles");
            return RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Joyce()
        {
            Repository.ClearTeamIdFromJudgeRecord(30, nameof(Joyce), "Ghen");
            return RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Margaret()
        {
            Repository.ClearTeamIdFromJudgeRecord(17, nameof(Margaret), "Eccles");
            return RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Rob()
        {
            Repository.ClearTeamIdFromJudgeRecord(5, nameof(Rob), "Bernstein");
            return RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Ron()
        {
            Repository.ClearTeamIdFromJudgeRecord(31, nameof(Ron), "Ghen");
            return RedirectToAction("Page01", "TournamentRegistration");
        }

        public ActionResult Sarah()
        {
            Repository.ClearTeamIdFromJudgeRecord(16, nameof(Sarah), "Tate");
            return RedirectToAction("Page01", "TournamentRegistration");
        }

        private string GenerateEmailBody(Page10ViewData page10ViewData)
        {
            using (StringWriter writer = new())
            {
                ICompositeViewEngine? viewEngine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult partialView = viewEngine.FindView(ControllerContext, "TournamentRegistration/EmailPartial", false);
                ViewContext viewContext = new(ControllerContext, partialView.View, ViewData, TempData, writer, new HtmlHelperOptions());
                partialView.View.RenderAsync(viewContext).Wait();

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
