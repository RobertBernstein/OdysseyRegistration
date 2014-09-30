// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VolunteerRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the VolunteerRegistrationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Controllers
{
    using System;
    using System.Globalization;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using Elmah;

    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;
    using OdysseyMvc4.ViewData.VolunteerRegistration;

    using Page01ViewData = OdysseyMvc4.ViewData.CoachesTrainingRegistration.Page01ViewData;
    using Page02ViewData = OdysseyMvc4.ViewData.CoachesTrainingRegistration.Page02ViewData;

    public class VolunteerRegistrationController : BaseRegistrationController
    {
        public VolunteerRegistrationController()
        {
            base.CurrentRegistrationType = BaseRegistrationController.RegistrationType.Volunteer;
            base.FriendlyRegistrationName = base.GetFriendlyRegistrationName();
        }

        public string BuildMailRegionalDirectorHyperLink(Page01ViewData viewData)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("mailto:");
            builder.Append(viewData.Config["RegionalDirectorEmail"]);
            string str = ("?subject=I would like to help at the Region " + viewData.RegionNumber + " Tournament&body=I cannot be a volunteer this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A").Replace(" ", "%20");
            builder.Append(str);
            return builder.ToString();
        }

        protected string GenerateEmailBody(Page03ViewData page03ViewData)
        {
            string input = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(page03ViewData.VolunteerInfo.EventMailBody, "<span>VolunteerID</span>", page03ViewData.Volunteer.VolunteerID.ToString(CultureInfo.InvariantCulture)), "<span>FirstName</span>", page03ViewData.Volunteer.FirstName), "<span>LastName</span>", page03ViewData.Volunteer.LastName), "<span>Region</span>", "Region " + page03ViewData.Config["RegionNumber"]);
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationURL))
            {
                builder.Append("<a href=\"" + page03ViewData.TournamentInfo.LocationURL + "\" target=\"_blank\">");
            }
            builder.Append(page03ViewData.TournamentInfo.Location);
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationAddress))
            {
                builder.Append(", " + page03ViewData.TournamentInfo.LocationAddress);
            }
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationCity))
            {
                builder.Append(", " + page03ViewData.TournamentInfo.LocationCity);
            }
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationState))
            {
                builder.Append(", " + page03ViewData.TournamentInfo.LocationState);
            }
            if (!string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.LocationURL))
            {
                builder.Append("</a>");
            }
            return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(input, "<span>TournamentLocation</span>", builder.ToString()), "<span>TournamentDate</span>", page03ViewData.TournamentInfo.StartDate.HasValue ? page03ViewData.TournamentInfo.StartDate.Value.ToLongDateString() : "TBA"), "<span>TournamentTime</span>", !string.IsNullOrWhiteSpace(page03ViewData.TournamentInfo.Time) ? page03ViewData.TournamentInfo.Time : "TBA"), "<span>ContactUsURL</span>", page03ViewData.Config["HomePage"] + page03ViewData.Config["ContactUsURL"]);
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
            Page01ViewData viewData = new Page01ViewData();
            base.SetBaseViewData(viewData);
            viewData.MailRegionalDirectorHyperLink = this.BuildMailRegionalDirectorHyperLink(viewData);
            viewData.MailRegionalDirectorHyperLinkText = "send an e-mail to " + viewData.Config["RegionalDirectorText"];
            return base.View(viewData);
        }

        [HttpPost, ActionName("Page01")]
        public ActionResult Page01Post()
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            try
            {
                Volunteer newVolunteer = new Volunteer {
                    TimeRegistrationStarted = new DateTime?(DateTime.Now),
                    UserAgent = base.Request.UserAgent
                };
                base.Repository.AddVolunteer(newVolunteer, null);
                return base.RedirectToAction("Page02", new { id = newVolunteer.VolunteerID });
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
            Page02ViewData viewData = new Page02ViewData();
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
                if (base.ModelState.IsValid)
                {
                    Volunteer newRegistrationData = new Volunteer {
                        FirstName = page02ViewData.FirstName,
                        LastName = page02ViewData.LastName,
                        EveningPhone = page02ViewData.EveningPhone,
                        DaytimePhone = page02ViewData.DaytimePhone,
                        MobilePhone = page02ViewData.MobilePhone,
                        EmailAddress = page02ViewData.EmailAddress,
                        VolunteerWantsToSee = page02ViewData.VolunteerWantsToSee,
                        Notes = page02ViewData.Notes
                    };
                    base.Repository.UpdateVolunteer(id, 2, newRegistrationData);
                    return base.RedirectToAction("Page03", new { id = id });
                }
                base.SetBaseViewData(page02ViewData);
                return base.View(page02ViewData);
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
            Page03ViewData viewData = new Page03ViewData {
                VolunteerInfo = base.Repository.VolunteerInfo
            };
            base.SetBaseViewData(viewData);
            viewData.Volunteer = base.Repository.GetVolunteerById(new int?(id));
            if (viewData.Volunteer == null)
            {
                viewData.ErrorMessage = "Your registration failed.  Please try the registration process over again.";
                return base.View(viewData);
            }
            base.Repository.UpdateVolunteer(id, 3, viewData.Volunteer);
            viewData.MailBody = this.GenerateEmailBody(viewData);
            if (!string.IsNullOrEmpty(viewData.Volunteer.EmailAddress) && (viewData.Volunteer.EmailAddress != "None"))
            {
                viewData.EmailAddressWasSpecified = true;
                MailMessage mailMessage = base.BuildMessage(viewData.Config["WebmasterEmail"], viewData.RegionName + " Odyssey Region " + viewData.RegionNumber + " " + viewData.FriendlyRegistrationName, viewData.MailBody, viewData.Volunteer.EmailAddress, null, null);
                if (mailMessage == null)
                {
                    return base.RedirectToAction("BadEmail");
                }
                viewData.MailErrorMessage = base.SendMessage(viewData, mailMessage);
            }
            else
            {
                viewData.EmailAddressWasSpecified = false;
            }
            return base.View(viewData);
        }

        [HttpPost]
        public ActionResult Page03(int id, string submitButton, string homePageButton, string nextButton, string restartRegistrationButton, FormCollection collection)
        {
            if (base.CurrentRegistrationState != BaseRegistrationController.RegistrationState.Available)
            {
                return base.RedirectToAction(base.CurrentRegistrationState.ToString());
            }
            if (!string.IsNullOrEmpty(restartRegistrationButton))
            {
                return base.RedirectToAction("Page01");
            }
            if (!string.IsNullOrEmpty(submitButton))
            {
                base.Repository.UpdateVolunteerEmail(id, collection["NewEmailTextBox"]);
                return this.Page03(id);
            }
            BaseViewData viewData = new BaseViewData();
            base.SetBaseViewData(viewData);
            return new RedirectResult(viewData.Config["HomePage"]);
        }
    }
}
