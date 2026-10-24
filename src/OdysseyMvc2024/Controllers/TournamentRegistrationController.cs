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
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Legacy registration flow logs to ELMAH and redirects to preserve user experience.")]
    [SuppressMessage("Design", "CA1515:Consider making public types internal", Justification = "ASP.NET MVC controller types are intentionally public.")]
    [SuppressMessage("Minor Code Smell", "S3267", Justification = "Legacy code path retained to preserve behavior.")]
    [SuppressMessage("Minor Code Smell", "S3358", Justification = "Legacy conditional logic retained to preserve behavior.")]
    [SuppressMessage("Minor Code Smell", "S6932", Justification = "Legacy request access pattern retained.")]
    [SuppressMessage("Major Code Smell", "S6967", Justification = "Legacy MVC action patterns are intentionally preserved.")]
    [SuppressMessage("Style", "S907", Justification = "Legacy decompiled control flow retained to minimize behavior changes.")]
    [SuppressMessage("Best Practices", "MA0134:Observe result of async calls", Justification = "Legacy non-async action flow retained for compatibility.")]
    [SuppressMessage("Code Smell", "S1172", Justification = "Method signature retained for legacy compatibility.")]
    public class TournamentRegistrationController : BaseRegistrationController
    {
        /// <summary>
        /// Initializes a new instance of the TournamentRegistrationController.
        /// </summary>
        /// <param name="repository">
        /// Repository for Odyssey registration data access.
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
        public int DetermineDivisionOfTeam(IReadOnlyCollection<string> gradesOfTeamMembers)
        {
            ArgumentNullException.ThrowIfNull(gradesOfTeamMembers);

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
            ArgumentNullException.ThrowIfNull(memberGrade);

            // Determine member's grade in school; Kindergarten is grade 0.
            int division = memberGrade == "Kindergarten" ? 0 : int.Parse(memberGrade, CultureInfo.InvariantCulture);

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

            foreach (Problem problem in problemsWithoutPrimaryOrSpontaneous ?? Enumerable.Empty<Problem>().AsQueryable())
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
                    UserAgent = Request.Headers.UserAgent.ToString()
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
            ArgumentNullException.ThrowIfNull(page02ViewData);

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
            ArgumentNullException.ThrowIfNull(page03ViewData);

            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                SetBaseViewData((BaseViewData)page03ViewData);
                TryUpdateModelAsync<Page03ViewData>(page03ViewData);
                page03ViewData.ListOfJudgesFound = Repository.GetJudgeByIdAndName(int.Parse(page03ViewData.JudgeId, CultureInfo.InvariantCulture), page03ViewData.JudgeFirstName, page03ViewData.JudgeLastName);
                
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
                    JudgeID = new short?(short.Parse(page03ViewData.JudgeId, CultureInfo.InvariantCulture))
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
            ArgumentNullException.ThrowIfNull(page05ViewData);

            if (CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return RedirectToAction(CurrentRegistrationState.ToString());
            }

            try
            {
                TryUpdateModelAsync<Page05ViewData>(page05ViewData);
                SetBaseViewData(page05ViewData);
                
                using MailMessage? coachEmailCheck = BuildMessage(page05ViewData.Config["WebmasterEmail"], "test", "test", page05ViewData.CoachEmailAddress, string.Empty, string.Empty);
                if (coachEmailCheck == null)
                {
                    return RedirectToAction("BadCoachEmail");
                }

                using MailMessage? altCoachEmailCheck = BuildMessage(page05ViewData.Config["WebmasterEmail"], "test", "test", page05ViewData.AltCoachEmailAddress, string.Empty, string.Empty);
                if (altCoachEmailCheck == null)
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
            ArgumentNullException.ThrowIfNull(page06ViewData);

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

            IReadOnlyList<string> gradesByRegistration = Repository.GetMemberGradesByRegistration(id);

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
            ArgumentNullException.ThrowIfNull(page07ViewData);

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
                
                string? divisionRadioGroup = page07ViewData.DivisionRadioGroup;
                
                if (divisionRadioGroup != null)
                {
                    if (divisionRadioGroup != "0")
                    {
                        if (divisionRadioGroup == "1")
                        {
                            newRegistrationData.ProblemID = new int?(int.Parse(page07ViewData.Division123ProblemChoice, CultureInfo.InvariantCulture));
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
                
                newRegistrationData.ProblemID = new int?(int.Parse(page07ViewData.SelectedProblem, CultureInfo.InvariantCulture));
            
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
            ArgumentNullException.ThrowIfNull(page08ViewData);

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
            if (spontaneous == true)
            {
                page09ViewData.IsDoingSpontaneous = "Yes";
            }
            else if (spontaneous == false)
            {
                page09ViewData.IsDoingSpontaneous = "No";
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

            var page10ViewData = CreatePage10ViewData(id);
            SetBaseViewData(page10ViewData);

            if (TryHandleJudgeAssignmentError(page10ViewData, id, out var errorResult))
            {
                return errorResult;
            }

            PopulatePage10Summary(page10ViewData, id);
            return View(page10ViewData);
        }

        private Page10ViewData CreatePage10ViewData(int id)
        {
            return new Page10ViewData
            {
                TournamentRegistration = Repository.GetTournamentRegistrationById(id)
            };
        }

        private bool TryHandleJudgeAssignmentError(Page10ViewData page10ViewData, int id, out ActionResult errorResult)
        {
            errorResult = null!;
            short? tournamentRegistrationId = Repository.GetJudgeIdFromTournamentRegistrationId(id);
            if (!tournamentRegistrationId.HasValue)
            {
                return false;
            }

            Repository.UpdateJudgeRecordWithTournamentRegistrationId(tournamentRegistrationId, id, out string errorMessage);
            if (string.IsNullOrEmpty(errorMessage))
            {
                return false;
            }

            page10ViewData.JudgeErrorMessage = errorMessage;
            using MailMessage? mailMessage = BuildMessage(page10ViewData.Config["WebmasterEmail"], "Error: " + page10ViewData.RegionName + " Odyssey Region " + page10ViewData.RegionNumber + " Tournament Registration", "<p>Team with ID # " + (object)id + " attempted to re-register after its judge was assigned to the team.</p><p>" + errorMessage + "</p>", page10ViewData.Config["WebmasterEmail"], string.Empty, string.Empty);
            if (mailMessage != null)
            {
                page10ViewData.MailErrorMessage = SendMessage((BaseViewData)page10ViewData, mailMessage);
            }

            errorResult = View(page10ViewData);
            return true;
        }

        private void PopulatePage10Summary(Page10ViewData page10ViewData, int id)
        {
            var tournamentRegistrationId = Repository.GetJudgeIdFromTournamentRegistrationId(id);
            page10ViewData.TournamentRegistration.TimeRegistered = DateTime.Now;
            Repository.UpdateTournamentRegistration(id, 10, page10ViewData.TournamentRegistration);

            Repository.GetJudgeNameFromJudgeId(tournamentRegistrationId, out string judgeFirstName, out string judgeLastName);
            page10ViewData.JudgeFirstName = judgeFirstName;
            page10ViewData.JudgeLastName = judgeLastName;
            page10ViewData.SchoolName = Repository.GetSchoolNameFromSchoolId(page10ViewData.TournamentRegistration.SchoolID);
            page10ViewData.ProblemName = Repository.GetProblemNameFromProblemId(page10ViewData.TournamentRegistration.ProblemID);
            page10ViewData.Division = page10ViewData.TournamentRegistration.Division == "0" ? "Primary" : page10ViewData.TournamentRegistration.Division;
            page10ViewData.MailBody = GenerateEmailBody(page10ViewData);

            using MailMessage? mailMessage = BuildMessage(page10ViewData.Config["WebmasterEmail"], page10ViewData.RegionName + " Odyssey Region " + page10ViewData.RegionNumber + " Tournament Registration", page10ViewData.MailBody, page10ViewData.TournamentRegistration.CoachEmailAddress, string.Empty, string.Empty);
            if (mailMessage != null)
            {
                page10ViewData.MailErrorMessage = SendMessage((BaseViewData)page10ViewData, mailMessage);
            }
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
            var page10ViewData = CreateResendEmailPageViewData(resendEmailViewData.TeamNumber);
            SetBaseViewData(page10ViewData);

            if (NoRecipientsSelected(resendEmailViewData))
            {
                resendEmailViewData.ErrorMessage = "No one was selected to resend the registration information to, so no e-mail was sent.";
                resendEmailViewData.Success = false;
                return View(resendEmailViewData);
            }

            var recipients = BuildResendRecipientList(resendEmailViewData, page10ViewData);
            PopulateResendEmailSummary(page10ViewData, recipients);
            return View(resendEmailViewData);
        }

        private static bool NoRecipientsSelected(ResendEmailViewData resendEmailViewData)
        {
            return resendEmailViewData.CoachCheckbox == "false" && resendEmailViewData.AltCoachCheckbox == "false";
        }

        private Page10ViewData CreateResendEmailPageViewData(int teamNumber)
        {
            return new Page10ViewData
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo,
                TournamentRegistration = Repository.GetTournamentRegistrationById(teamNumber)
            };
        }

        private static string BuildResendRecipientList(ResendEmailViewData resendEmailViewData, Page10ViewData page10ViewData)
        {
            var recipients = string.Empty;
            if (resendEmailViewData.CoachCheckbox == "true")
            {
                recipients += page10ViewData.TournamentRegistration.CoachEmailAddress;
            }

            if (resendEmailViewData.AltCoachCheckbox == "true")
            {
                if (!string.IsNullOrEmpty(recipients))
                {
                    recipients += ",";
                }

                recipients += page10ViewData.TournamentRegistration.AltCoachEmailAddress;
            }

            return recipients;
        }

        private void PopulateResendEmailSummary(Page10ViewData page10ViewData, string recipients)
        {
            string judgeFirstName;
            string judgeLastName;
            Repository.GetJudgeNameFromJudgeId(page10ViewData.TournamentRegistration.JudgeID, out judgeFirstName, out judgeLastName);
            page10ViewData.JudgeFirstName = judgeFirstName;
            page10ViewData.JudgeLastName = judgeLastName;
            page10ViewData.SchoolName = Repository.GetSchoolNameFromSchoolId(page10ViewData.TournamentRegistration.SchoolID);
            page10ViewData.ProblemName = Repository.GetProblemNameFromProblemId(page10ViewData.TournamentRegistration.ProblemID);
            page10ViewData.MailBody = GenerateEmailBody(page10ViewData);

            using MailMessage? mailMessage = BuildMessage(page10ViewData.Config["WebmasterEmail"], page10ViewData.RegionName + " Odyssey Region " + page10ViewData.RegionNumber + " Tournament Registration", page10ViewData.MailBody, recipients, "rob@tardistech.com", string.Empty);
            if (mailMessage != null)
            {
                page10ViewData.MailErrorMessage = SendMessage((BaseViewData)page10ViewData, mailMessage);
            }
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
