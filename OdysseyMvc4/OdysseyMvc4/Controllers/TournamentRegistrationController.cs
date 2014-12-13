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

    /// <summary>
    /// The tournament registration controller.
    /// </summary>
    public class TournamentRegistrationController : BaseRegistrationController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TournamentRegistrationController"/> class.
        /// </summary>
        public TournamentRegistrationController()
        {
            this.CurrentRegistrationType = RegistrationType.Tournament;
            this.FriendlyRegistrationName = this.GetFriendlyRegistrationName();
        }

        /// <summary>
        /// Handle HTTP GET requests for the BadAltCoachEmail page.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult BadAltCoachEmail()
        {
            return this.View();
        }

        /// <summary>
        /// Handle HTTP GET requests for the BadCoachEmail page.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult BadCoachEmail()
        {
            return this.View();
        }

        /// <summary>
        /// The determine division of team.
        /// </summary>
        /// <param name="gradesOfTeamMembers">
        /// The grades of team members.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int DetermineDivisionOfTeam(List<string> gradesOfTeamMembers)
        {
            int divisionOfTeam = -1;
            foreach (string grade in gradesOfTeamMembers)
            {
                if (!string.IsNullOrEmpty(grade))
                {
                    int divisionOfTeamMember = this.GetDivisionOfTeamMember(grade);
                    if (divisionOfTeamMember > divisionOfTeam)
                    {
                        divisionOfTeam = divisionOfTeamMember;
                    }
                }
            }

            return divisionOfTeam;
        }

        /// <summary>
        /// The get division of team member.
        /// </summary>
        /// <param name="memberGrade">
        /// The member grade.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetDivisionOfTeamMember(string memberGrade)
        {
            // Determine member's grade in school; Kindergarten is grade 0.
            int division = (memberGrade == "Kindergarten") ? 0 : int.Parse(memberGrade);

            // divisionOfTeamMember = 0 when the team member is Primary, divisionOfTeamMember = 1, 2, or 3 when the
            // team member is Division 1, 2, or 3.
            return (division >= 0) && (division <= 2) ? 0 : ((division <= 5) ? 1 : ((division <= 8) ? 2 : 3));
        }

        /// <summary>
        /// Get the list of Odyssey problems as an HTML list.
        /// </summary>
        /// <param name="thisTeamIsPrimary">
        /// The is primary.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetProblemsAsHtmlList(bool thisTeamIsPrimary)
        {
            IQueryable<Problem> problemsWithoutPrimaryOrSpontaneous = this.Repository.ProblemsWithoutPrimaryOrSpontaneous;
            StringBuilder sb = new StringBuilder();

            sb.Append("<ol>\n");

            if (thisTeamIsPrimary)
            {
                sb.Append("<li>" + this.Repository.PrimaryProblem.First().ProblemName + " (The Primary Problem)</li>\n");
            }

            foreach (Problem problem in problemsWithoutPrimaryOrSpontaneous)
            {
                sb.Append("<li>" + problem.ProblemName + "</li>\n");
            }

            sb.Append("</ol>\n");

            return sb.ToString();
        }

        /// <summary>
        /// GET: /TournamentRegistration/
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction("Page01");
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
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page01ViewData viewData = new Page01ViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page01.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ActionName("Page01")]
        public ActionResult Page01Post()
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page01ViewData viewData = new Page01ViewData();
            this.SetBaseViewData(viewData);
            try
            {
                TournamentRegistration newRegistration = new TournamentRegistration
                {
                    TimeRegistrationStarted = DateTime.Now,
                    TeamRegistrationFee = viewData.TeamRegistrationFee,
                    UserAgent = this.Request.UserAgent
                };

                // TODO: else case: Send an e-mail reporting database failure; could not create the record
                this.Repository.AddTournamentRegistration(newRegistration);
                return this.RedirectToAction("Page02", new { id = newRegistration.TeamID });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page02.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page02(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page02ViewData viewData = new Page02ViewData
            {
                SchoolList = new SelectList(this.Repository.Schools, "ID", "Name")
            };

            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page02.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <param name="page02ViewData">
        /// The page 02 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page02(int id, Page02ViewData page02ViewData)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration
                {
                    SchoolID = page02ViewData.SelectedSchool
                };

                // TODO: if case: Send an e-mail reporting database failure; could not find the record already added to the database
                this.Repository.UpdateTournamentRegistration(id, 2, newRegistrationData);
                return this.RedirectToAction("Page03", new { id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page03.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page03(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page03ViewData viewData = new Page03ViewData();
            this.SetBaseViewData(viewData);
            viewData.NoJudgesFound = false;

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page03.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <param name="page03ViewData">
        /// The page 03 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page03(int id, Page03ViewData page03ViewData)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                this.SetBaseViewData(page03ViewData);
                this.UpdateModel(page03ViewData);

                page03ViewData.ListOfJudgesFound = this.Repository.GetJudgeByIdAndName(
                    int.Parse(page03ViewData.JudgeId),
                    page03ViewData.JudgeFirstName,
                    page03ViewData.JudgeLastName);

                if (!page03ViewData.ListOfJudgesFound.Any())
                {
                    page03ViewData.NoJudgesFound = true;
                    return this.View(page03ViewData);
                }

                // Make sure any judge found is not already assigned to a team.
                if ((page03ViewData.ListOfJudgesFound.First() != null) &&
                    (page03ViewData.ListOfJudgesFound.First().TeamID != null))
                {
                    page03ViewData.JudgeAlreadyTaken = true;
                    return this.View(page03ViewData);
                }

                TournamentRegistration newRegistrationData = new TournamentRegistration
                {
                    JudgeID = short.Parse(page03ViewData.JudgeId)
                };

                // TODO: Send an e-mail reporting database failure; could not find the record already added to the database
                this.Repository.UpdateTournamentRegistration(id, 3, newRegistrationData);

                return this.RedirectToAction("Page05", new { id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page04.  If the user navigates to this page
        /// without a specified Tournament Registration ID as a parameter, he/she will be
        /// redirected to a generic error page.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// A view for page 4, coming soon, closed, down, or the generic error page.
        /// </returns>
        [HttpGet]
        public ActionResult Page04(int? id)
        {
            // If the user navigated to this page without a record number as parameter,
            // redirect him/her to a generic error page.
            // if (id == null)
            // TODO: Does HasValue work correctly? - Rob, 12/10/2014
            if (!id.HasValue)
            {
                return this.RedirectToAction("Error");
            }

            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page04ViewData viewData = new Page04ViewData();
            this.SetBaseViewData(viewData);
            viewData.NoVolunteersFound = false;

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page04.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <param name="page04ViewData">
        /// The page 04 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page04(int? id, Page04ViewData page04ViewData)
        {
            // If the user navigated to this page without a record number as parameter,
            // redirect him/her to a generic error page.
            // if (id == null)
            // TODO: Does HasValue work correctly? - Rob, 12/10/2014
            if (!id.HasValue)
            {
                return this.RedirectToAction("Error");
            }

            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                this.SetBaseViewData(page04ViewData);
                this.UpdateModel(page04ViewData);

                page04ViewData.VolunteerFound =
                    this.Repository.GetVolunteerByIdAndName(
                        int.Parse(page04ViewData.VolunteerId),
                        page04ViewData.VolunteerFirstName,
                        page04ViewData.VolunteerLastName);

                if (page04ViewData.VolunteerFound == null)
                {
                    page04ViewData.NoVolunteersFound = true;
                    return this.View(page04ViewData);
                }

                // Make sure the volunteer found is not already assigned to a team.
                if (page04ViewData.VolunteerFound.TeamID.HasValue)
                {
                    page04ViewData.VolunteerAlreadyTaken = true;
                    return this.View(page04ViewData);
                }

                TournamentRegistration newRegistrationData = new TournamentRegistration
                {
                    VolunteerID = page04ViewData.VolunteerFound.VolunteerID
                };

                // TODO: Send an e-mail reporting database failure; could not find the record already added to the database.
                this.Repository.UpdateTournamentRegistration(id.Value, 4, newRegistrationData);

                return this.RedirectToAction("Page05", new { id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
                return this.RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page05.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page05(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page05ViewData viewData = new Page05ViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page05.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <param name="page05ViewData">
        /// The page 05 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page05(int id, Page05ViewData page05ViewData)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                this.UpdateModel(page05ViewData);
                this.SetBaseViewData(page05ViewData);

                // Instantiate a new instance of MailMessage to test the legitimacy of the coach e-mail address entered.
                if (this.BuildMessage(
                    page05ViewData.Config["WebmasterEmail"],
                    "test",
                    "test",
                    page05ViewData.CoachEmailAddress,
                    null,
                    null) == null)
                {
                    return this.RedirectToAction("BadCoachEmail");
                }

                // Instantiate a new instance of MailMessage to test the legitimacy of the alternate coach e-mail address entered.
                if (this.BuildMessage(
                    page05ViewData.Config["WebmasterEmail"],
                    "test",
                    "test",
                    page05ViewData.AltCoachEmailAddress,
                    null,
                    null) == null)
                {
                    return this.RedirectToAction("BadAltCoachEmail");
                }

                TournamentRegistration newRegistrationData = new TournamentRegistration
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

                // TODO: Send an e-mail reporting database failure; could not find the record already added to the database
                this.Repository.UpdateTournamentRegistration(id, 5, newRegistrationData);

                return this.RedirectToAction("Page06", new { id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page06.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page06(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            List<string> gradesList = new List<string> { "Kindergarten" };

            for (int grade = 1; grade <= 12; grade++)
            {
                gradesList.Add(grade.ToString(CultureInfo.InvariantCulture));
            }

            Page06ViewData viewData = new Page06ViewData { GradeChoices = new SelectList(gradesList) };

            this.SetBaseViewData(viewData);

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page06.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <param name="page06ViewData">
        /// The page 06 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page06(int id, Page06ViewData page06ViewData)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                // Retrieve the data entered on the page
                this.UpdateModel(page06ViewData);

                // Copy drop-down list values into TournamentRegistration object in the viewData to pass to the
                // repository.
                TournamentRegistration newRegistrationData = new TournamentRegistration
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

                // TODO: Send an e-mail reporting database failure; could not find the record already added to the database.
                this.Repository.UpdateTournamentRegistration(id, 6, newRegistrationData);

                return this.RedirectToAction("Page07", new { id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page07.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page07(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            List<string> memberGradesByRegistration = this.Repository.GetMemberGradesByRegistration(id);

            Page07ViewData viewData = new Page07ViewData
            {
                // DivisionOfTeam will equal 0 when it is a Primary Division team.
                DivisionOfTeam = this.DetermineDivisionOfTeam(memberGradesByRegistration),
                Division123ProblemDropDown = new SelectList(this.Repository.ProblemsWithoutPrimaryOrSpontaneous, "ProblemID", "ProblemName"),
                IsDoingSpontaneousDropDown = new SelectList(new List<string> { "Yes", "No" }),
                Division123ListOfProblemsAsHtmlList = this.GetProblemsAsHtmlList(false),
                Division123AndPrimaryListOfProblemsAsHtmlList = this.GetProblemsAsHtmlList(true),
                PrimaryProblemName = this.Repository.PrimaryProblem.First().ProblemName
            };

            this.SetBaseViewData(viewData);

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page07.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <param name="page07ViewData">
        /// The page 07 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page07(int id, Page07ViewData page07ViewData)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration
                {
                    Division = page07ViewData.DivisionRadioGroup ?? page07ViewData.DivisionOfTeam.ToString(CultureInfo.InvariantCulture)
                };

                // TODO: Get rid of all these labels, Rob - 12/10/2014
                string divisionRadioGroup = page07ViewData.DivisionRadioGroup;
                if (divisionRadioGroup == null)
                {
                    goto Label_00C9;
                }

                if (divisionRadioGroup != "0")
                {
                    if (divisionRadioGroup == "1")
                    {
                        goto Label_00B0;
                    }

                    goto Label_00C9;
                }

                // Only record Spontaneous if the team is competing in the Primary Division
                newRegistrationData.ProblemID = 6;
                newRegistrationData.Spontaneous = page07ViewData.IsDoingSpontaneous == "Yes";
                goto Label_00E2;
            Label_00B0:
                newRegistrationData.ProblemID = int.Parse(page07ViewData.Division123ProblemChoice);
                goto Label_00E2;
            Label_00C9:
                newRegistrationData.ProblemID = int.Parse(page07ViewData.SelectedProblem);
            Label_00E2:
                this.Repository.UpdateTournamentRegistration(id, 7, newRegistrationData);

                return this.RedirectToAction("Page08", new { id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page08.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page08(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page08ViewData viewData = new Page08ViewData();
            this.SetBaseViewData(viewData);

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page08.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <param name="page08ViewData">
        /// The page 08 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page08(int id, Page08ViewData page08ViewData)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            try
            {
                TournamentRegistration newRegistrationData = new TournamentRegistration
                {
                    SchedulingIssues = page08ViewData.SchedulingIssues,
                    SpecialConsiderations = page08ViewData.SpecialConsiderations
                };

                // TODO: Send an e-mail reporting database failure; could not find the record already added to the database.
                this.Repository.UpdateTournamentRegistration(id, 8, newRegistrationData);

                return this.RedirectToAction("Page09", new { id });
            }
            catch (Exception exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                // TODO: Replace with Error Message
                return this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handle HTTP GET requests for Page09.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page09(int id)
        {
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            Page09ViewData viewData = new Page09ViewData
            {
                TournamentRegistration = this.Repository.GetTournamentRegistrationById(id)
            };

            this.SetBaseViewData(viewData);
            viewData.SchoolName = this.Repository.GetSchoolNameFromSchoolId(viewData.TournamentRegistration.SchoolID);

            string judgeFirstName;
            string judgeLastName;
            this.Repository.GetJudgeNameFromJudgeId(viewData.TournamentRegistration.JudgeID, out judgeFirstName, out judgeLastName);

            viewData.JudgeFirstName = judgeFirstName;
            viewData.JudgeLastName = judgeLastName;

            ////Volunteer volunteerById = this.Repository.GetVolunteerById(viewData.TournamentRegistration.VolunteerID);

            ////viewData.VolunteerFirstName = volunteerById.FirstName;
            ////viewData.VolunteerLastName = volunteerById.LastName;

            viewData.Division = (viewData.TournamentRegistration.Division == "0") ? "Primary" : viewData.TournamentRegistration.Division;
            viewData.ProblemName = this.Repository.GetProblemNameFromProblemId(viewData.TournamentRegistration.ProblemID);

            if (viewData.TournamentRegistration.Spontaneous.HasValue)
            {
                viewData.IsDoingSpontaneous = viewData.TournamentRegistration.Spontaneous.Value ? "Yes" : "No";
            }

            if (string.IsNullOrEmpty(viewData.ProblemName))
            {
                // TODO: Send error e-mail.
                viewData.ProblemName = "(Could not obtain problem name)";
            }

            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for Page09.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <param name="homePageButton">
        /// The home page button.
        /// </param>
        /// <param name="nextButton">
        /// The next button.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Page09(int id, string homePageButton, string nextButton, FormCollection collection)
        {
            return (this.CurrentRegistrationState != RegistrationState.Available)
                ? this.RedirectToAction(this.CurrentRegistrationState.ToString())
                : this.RedirectToAction("Page10", new { id });
        }

        /// <summary>
        /// Handle HTTP GET requests for Page10.
        /// </summary>
        /// <param name="id">
        /// The Tournament Registration ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Page10(int id)
        {
            Page10ViewData viewData;
            string errorMessage;
            MailMessage errorMailMessage;
            string judgeFirstName;
            string judgeLastName;
            if (this.CurrentRegistrationState != RegistrationState.Available)
            {
                return this.RedirectToAction(this.CurrentRegistrationState.ToString());
            }

            short? judgeId = this.Repository.GetJudgeIdFromTournamentRegistrationId(id);
            short? nullable3 = judgeId;
            int? nullable4 = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault()) : null;
            if (nullable4.HasValue)
            {
                // Attempt to write the Tournament Registration ID into the correct Judge record.
                this.Repository.UpdateJudgeRecordWithTournamentRegistrationId(judgeId, id, out errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    viewData = new Page10ViewData
                    {
                        JudgeErrorMessage = errorMessage
                    };

                    this.SetBaseViewData(viewData);

                    // Instantiate a new instance of MailMessage
                    errorMailMessage = this.BuildMessage(
                        viewData.Config["WebmasterEmail"],
                        "Error: " + viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " Tournament Registration",
                        string.Concat(new object[] { "<p>Team with ID # ", id, " attempted to re-register after its judge was assigned to the team.</p><p>", errorMessage, "</p>" }),
                        viewData.Config["WebmasterEmail"],
                        null,
                        null);

                    // Instantiate a new instance of SmtpClient.
                    viewData.MailErrorMessage = this.SendMessage(viewData, errorMailMessage);

                    return this.View(viewData);
                }
            }

            int? volunteerId = this.Repository.GetVolunteerIdFromTournamentRegistrationId(id);
            if (volunteerId.HasValue)
            {
                // Attempt to write the Tournament Registration ID into the correct Volunteer record.
                this.Repository.UpdateVolunteerRecordWithTournamentRegistrationId(volunteerId.Value, id, out errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    viewData = new Page10ViewData
                    {
                        VolunteerErrorMessage = errorMessage
                    };

                    this.SetBaseViewData(viewData);

                    // Instantiate a new instance of MailMessage.
                    errorMailMessage = this.BuildMessage(
                        viewData.Config["WebmasterEmail"],
                        "Error: " + viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " Tournament Registration",
                        string.Concat(new object[] { "<p>Team with ID # ", id, " attempted to re-register after its volunteer was assigned to the team.</p><p>", errorMessage, "</p>" }),
                        viewData.Config["WebmasterEmail"],
                        null,
                        null);

                    // Instantiate a new instance of SmtpClient.
                    viewData.MailErrorMessage = this.SendMessage(viewData, errorMailMessage);

                    return this.View(viewData);
                }
            }

            // The Tournament Registration ID was successfully written into the correct Judge and Volunteer records.
            viewData = new Page10ViewData
            {
                TournamentInfo = this.Repository.TournamentInfo,
                TournamentRegistration = this.Repository.GetTournamentRegistrationById(id)
            };

            // Update the DateTime of the registration in the TournamentRegistration record.
            viewData.TournamentRegistration.TimeRegistered = DateTime.Now;
            this.Repository.UpdateTournamentRegistration(id, 10, viewData.TournamentRegistration);

            this.SetBaseViewData(viewData);
            this.Repository.GetJudgeNameFromJudgeId(judgeId, out judgeFirstName, out judgeLastName);
            viewData.JudgeFirstName = judgeFirstName;
            viewData.JudgeLastName = judgeLastName;

            ////Volunteer volunteerById = this.Repository.GetVolunteerById(viewData.TournamentRegistration.VolunteerID);
            ////viewData.VolunteerFirstName = volunteerById.FirstName;
            ////viewData.VolunteerLastName = volunteerById.LastName;

            viewData.SchoolName = this.Repository.GetSchoolNameFromSchoolId(viewData.TournamentRegistration.SchoolID);
            viewData.ProblemName = this.Repository.GetProblemNameFromProblemId(viewData.TournamentRegistration.ProblemID);
            viewData.Division = (viewData.TournamentRegistration.Division == "0") ? "Primary" : viewData.TournamentRegistration.Division;
            viewData.MailBody = this.GenerateEmailBody(viewData);

            // Instantiate a new instance of MailMessage.
            MailMessage mailMessage = this.BuildMessage(
                viewData.Config["WebmasterEmail"],
                viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " Tournament Registration",
                viewData.MailBody,
                viewData.TournamentRegistration.CoachEmailAddress,
                null,
                null);

            // Instantiate a new instance of SmtpClient.
            viewData.MailErrorMessage = this.SendMessage(viewData, mailMessage);
            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP GET requests for the ResendEmail page.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult ResendEmail()
        {
            ResendEmailViewData viewData = new ResendEmailViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        /// <summary>
        /// Handle HTTP POST requests for the ResendEmail page.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult ResendEmail(FormCollection collection)
        {
            ResendEmailViewData model = new ResendEmailViewData();
            this.UpdateModel(model);

            Page10ViewData viewData = new Page10ViewData
            {
                TournamentInfo = this.Repository.TournamentInfo,
                TournamentRegistration = this.Repository.GetTournamentRegistrationById(model.TeamNumber)
            };

            this.SetBaseViewData(viewData);

            // Make sure at least one of the two check boxes was checked.
            if ((model.CoachCheckbox == "false") && (model.AltCoachCheckbox == "false"))
            {
                model.ErrorMessage = "No one was selected to resend the registration information to, so no e-mail was sent.";
                model.Success = false;
                return this.View(model);
            }

            string recipientList = string.Empty;
            if (model.CoachCheckbox == "true")
            {
                recipientList += viewData.TournamentRegistration.CoachEmailAddress;
            }

            if (model.AltCoachCheckbox == "true")
            {
                if (!string.IsNullOrEmpty(recipientList))
                {
                    recipientList = recipientList + ",";
                }

                recipientList += viewData.TournamentRegistration.AltCoachEmailAddress;
            }

            string judgeFirstName;
            string judgeLastName;
            this.Repository.GetJudgeNameFromJudgeId(viewData.TournamentRegistration.JudgeID, out judgeFirstName, out judgeLastName);

            viewData.JudgeFirstName = judgeFirstName;
            viewData.JudgeLastName = judgeLastName;

            ////Volunteer volunteerById = this.Repository.GetVolunteerById(viewData.TournamentRegistration.VolunteerID);
            ////viewData.VolunteerFirstName = volunteerById.FirstName;
            ////viewData.VolunteerLastName = volunteerById.LastName;

            viewData.SchoolName = this.Repository.GetSchoolNameFromSchoolId(viewData.TournamentRegistration.SchoolID);
            viewData.ProblemName = this.Repository.GetProblemNameFromProblemId(viewData.TournamentRegistration.ProblemID);
            viewData.MailBody = this.GenerateEmailBody(viewData);

            // Instantiate a new instance of MailMessage.
            MailMessage mailMessage = this.BuildMessage(
                viewData.Config["WebmasterEmail"],
                viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " Tournament Registration",
                viewData.MailBody,
                recipientList,
                "rob@tardistech.com",
                null);

            // Instantiate a new instance of SmtpClient.
            model.ErrorMessage = this.SendMessage(model, mailMessage);

            model.Success = true;

            return this.View(model);
        }

        #region Test Methods

        /// <summary>
        /// Clear the Team ID from Carolina's Judge Registration in order to test Tournament Registration.
        /// </summary>
        /// <remarks>
        /// This function is intended for testing only.
        /// </remarks>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Carolina()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(0x26, "Carolina", "Deschapelles");
            return this.RedirectToAction("Page01", "TournamentRegistration");
        }

        /// <summary>
        /// Clear the Team ID from Joyce Ghen's Judge Registration in order to test Tournament Registration.
        /// </summary>
        /// <remarks>
        /// This function is intended for testing only.
        /// </remarks>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Joyce()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(30, "Joyce", "Ghen");
            return this.RedirectToAction("Page01", "TournamentRegistration");
        }

        /// <summary>
        /// Clear the Team ID from Margaret Eccles' Judge Registration in order to test Tournament Registration.
        /// </summary>
        /// <remarks>
        /// This function is intended for testing only.
        /// </remarks>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Margaret()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(0x11, "Margaret", "Eccles");
            return this.RedirectToAction("Page01", "TournamentRegistration");
        }

        /// <summary>
        /// Clear the Team ID from Rob's Judge Registration in order to test Tournament Registration.
        /// </summary>
        /// <remarks>
        /// This function is intended for testing only.
        /// </remarks>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Rob()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(5, "Rob", "Bernstein");
            return this.RedirectToAction("Page01", "TournamentRegistration");
        }

        /// <summary>
        /// Clear the Team ID from Ron Ghen's Judge Registration in order to test Tournament Registration.
        /// </summary>
        /// <remarks>
        /// This function is intended for testing only.
        /// </remarks>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Ron()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(0x1f, "Ron", "Ghen");
            return this.RedirectToAction("Page01", "TournamentRegistration");
        }

        /// <summary>
        /// Clear the Team ID from Sarah Tate's Judge Registration in order to test Tournament Registration.
        /// </summary>
        /// <remarks>
        /// This function is intended for testing only.
        /// </remarks>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Sarah()
        {
            this.Repository.ClearTeamIdFromJudgeRecord(0x10, "Sarah", "Tate");
            return this.RedirectToAction("Page01", "TournamentRegistration");
        }

        #endregion

        /// <summary>
        /// The generate email body.
        /// </summary>
        /// <param name="page10ViewData">
        /// The page 10 View Data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GenerateEmailBody(Page10ViewData page10ViewData)
        {
            using (StringWriter writer = new StringWriter())
            {
                this.ViewData.Model = page10ViewData;
                ViewEngineResult result = ViewEngines.Engines.FindPartialView(this.ControllerContext, "TournamentRegistration/EmailPartial");
                ViewContext viewContext = new ViewContext(this.ControllerContext, result.View, this.ViewData, this.TempData, writer);
                result.View.Render(viewContext, writer);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
