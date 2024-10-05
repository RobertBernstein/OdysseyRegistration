// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base registration controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.BaseRegistrationController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Net;
using System.Net.Mail;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData;
using Microsoft.AspNetCore.Mvc;
using ElmahCore;
using System;

namespace OdysseyMvc2024.Controllers
{
    /// <summary>
    /// The base registration controller.
    /// </summary>
    public class BaseRegistrationController : Controller
    {
        /// <summary>
        /// The object that provides access to the database.
        /// </summary>
        protected readonly OdysseyRepository Repository;

        public BaseRegistrationController(IOdysseyEntities context)
        {
            Repository = new OdysseyRepository(context);                              
        }

        [HttpGet]
        public ActionResult BadEmail()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View();

            // TODO: Test that this is the correct path to the BadEmail page, Rob - 01/18/2015.
            // return this.View("~/Views/Shared/BadEmail.cshtml");
        }

        /// <summary>
        /// Construct all the components of an e-mail message
        /// </summary>
        /// <param name="from">
        /// The sender of the e-mail message.
        /// </param>
        /// <param name="subject">
        /// The subject of the e-mail message.
        /// </param>
        /// <param name="body">
        /// The contents of the e-mail message.
        /// </param>
        /// <param name="to">
        /// The recipient(s) of the e-mail message; separate multiple recipients with commas (,).
        /// </param>
        /// <param name="bcc">
        /// The blind carbon copy recipient(s) of the e-mail message; separate multiple recipients with commas (,).
        /// </param>
        /// <param name="cc">
        /// The carbon copy recipient(s) of the e-mail message; separate multiple recipients with commas (,).
        /// </param>
        /// <returns>
        /// The constructed e-mail message.
        /// </returns>
        protected MailMessage? BuildMessage(
          string from,
          string subject,
          string body,
          string to,
          string bcc,
          string cc)
        {
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                Priority = MailPriority.Normal
            };

            // Set the recipient address of the mail message.
            string str = to;
            char[] chArray = [','];
            foreach (string recipient in str.Split(chArray))
            {
                try
                {
                    mailMessage.To.Add(new MailAddress(recipient));
                }
                catch (FormatException ex)
                {
                    return null;
                }
            }

            // Check if the bcc value is null or an empty string
            if (!string.IsNullOrWhiteSpace(bcc))
            {
                // Set the Bcc address of the mail message
                mailMessage.Bcc.Add(new MailAddress(bcc));
            }
            
            // Check if the cc value is null or an empty value
            if (!string.IsNullOrWhiteSpace(cc))
            {
                // Set the CC address of the mail message
                mailMessage.CC.Add(new MailAddress(cc));
            }

            return mailMessage;
        }

        /// <summary>
        /// Handle HTTP GET requests for the Closed page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the same page with the viewData
        /// populated.
        /// </returns>
        [HttpGet]
        public ActionResult Closed()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View((object)baseViewData);
        }

        private string DetermineSiteCssFile() =>
            !string.IsNullOrEmpty(Url.PageLink()) &&
            Url.PageLink().Contains("novasouth", StringComparison.InvariantCultureIgnoreCase)
                ? Url.Content("~/Content/NovaSouth.css")
                : Url.Content("~/Content/NovaNorth.css");

        private string DetermineSiteName()
        {
            string hostname = HttpContext.Request.Host.Host;

            string siteName = Url.PageLink() != null
                ? hostname.ToLowerInvariant()
                : "novanorth.org";

            if (siteName.StartsWith("www."))
            {
                siteName = siteName.Substring(4);
            }

            return siteName;
        }

        /// <summary>
        /// Handle HTTP GET requests for the Down page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the same page with the viewData
        /// populated.
        /// </returns>
        [HttpGet]
        public ActionResult Down()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View((object)baseViewData);
        }

        /// <summary>
        /// Handle HTTP GET requests for the Error page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the same page with the viewData
        /// populated.
        /// </returns>
        [HttpGet]
        public ActionResult Error()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View((object)baseViewData);
        }

        public string GetFriendlyRegistrationName()
        {
            // Make sure that CurrentRegistrationType has been set before calling this method.
            // TODO: we should probably assert here if CurrentRegistrationType has not been set.
            if (this.CurrentRegistrationType == BaseRegistrationController.RegistrationType.None)
            {
                return string.Empty;
            }

            return this.CurrentRegistrationType == BaseRegistrationController.RegistrationType.CoachesTraining
                ? "Coaches Training Registration"
                : this.CurrentRegistrationType.ToString() + " Registration";
        }

        /// <summary>
        /// Determine whether the specified registration is closed based on the Config
        /// table in the database.
        /// </summary>
        /// <param name="registrationType">
        /// The registration type.
        /// </param>
        /// <returns>
        /// true if registration is closed, false otherwise.
        /// </returns>
        public bool IsRegistrationClosed(
          BaseRegistrationController.RegistrationType registrationType)
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);

            DateTime registrationCloseDateTime;
            try
            {
                // TODO: Change this to TryParse (everywhere).
                // TODO: This will fail if you specify an invalid date, such as 2/29 on a non-leap year! Log such possibilities. Add a unit test for this.
                registrationCloseDateTime = DateTime.Parse(baseViewData.Config[registrationType.ToString() + "RegistrationCloseDateTime"]);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception.
                // If we cannot even read the closing time from the database, close down registration!
                return true;
            }

            // TODO: Adjust for Eastern Time if not in Eastern Time, e.g. Pacific Time
            DateTime currentEasternTime = DateTime.Now; ////.AddHours(3);
            return DateTime.Compare(registrationCloseDateTime, currentEasternTime) < 0;
        }

        /// <summary>
        /// Determine whether the specified registration is coming soon based on the
        /// Config table in the database.
        /// </summary>
        /// <param name="registrationType">
        /// The registration type.
        /// </param>
        /// <returns>
        /// true if registration is coming soon, false otherwise.
        /// </returns>
        public bool IsRegistrationComingSoon(
          BaseRegistrationController.RegistrationType registrationType)
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);

            DateTime registrationOpenDate;
            try
            {
                registrationOpenDate = DateTime.Parse(baseViewData.Config[registrationType.ToString() + "RegistrationOpenDateTime"]);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception.
                // If we cannot even read the opening time from the database, assume we've passed the opening date.
                return false;
            }

            // TODO: Adjust for Eastern Time if not in Eastern Time, e.g. Pacific Time
            // DateTime currentEasternTime = DateTime.Now; ////.AddHours(3);
            return DateTime.Compare(DateTime.Now, registrationOpenDate) < 0;
        }

        /// <summary>
        /// Is the current registration type set to be administratively down?
        /// </summary>
        /// <param name="registrationType">
        /// The registration type.
        /// </param>
        /// <returns>
        /// true if the registration is administratively down, false otherwise.
        /// </returns>
        public bool IsRegistrationDown(
          BaseRegistrationController.RegistrationType registrationType)
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);

            bool registrationIsDown;
            bool.TryParse(baseViewData.Config["Is" + registrationType + "RegistrationDown"], out registrationIsDown);
            return registrationIsDown;
        }

        /// <summary>
        /// Sends the provided e-mail message.
        /// </summary>
        /// <param name="viewData">
        /// Provides access to the Config information for the current registration.
        /// </param>
        /// <param name="mailMessage">
        /// The e-mail to send.
        /// </param>
        /// <returns>
        /// null on success, an error message otherwise.
        /// </returns>
        public string? SendMessage(BaseViewData viewData, MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = viewData.Config["EmailServer"],
                Credentials = new NetworkCredential(viewData.Config["WebmasterEmail"], viewData.Config["WebmasterEmailPassword"])
            };

            // Send the mail message
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException exception)
            {
                ElmahExtensions.RaiseError(exception);

                // TODO: Should we rename innerException to smtpFailedRecipientException?
                foreach (SmtpFailedRecipientException innerException in exception.InnerExceptions)
                {
                    switch (innerException.StatusCode)
                    {
                        case SmtpStatusCode.MailboxBusy:
                        case SmtpStatusCode.MailboxUnavailable:
                            Thread.Sleep(5000);
                            smtpClient.Send(mailMessage);
                            continue;
                        default:
                            return string.Format("Failed to deliver message to {0}", innerException.FailedRecipient);
                    }
                }
            }
            catch (SmtpException smtpException)
            {
                ElmahExtensions.RaiseError(smtpException);
                return smtpException.StatusCode.ToString();
            }

            return null;
        }

        /// <summary>
        /// Set the configuration information required by every ViewData class/object.
        /// </summary>
        /// <param name="viewData">
        /// The view data.
        /// </param>
        protected void SetBaseViewData(BaseViewData viewData)
        {
            viewData.Config = this.Repository.Config;
            viewData.RegionName = this.Repository.RegionName;
            viewData.RegionNumber = this.Repository.RegionNumber;
            viewData.TournamentInfo = this.Repository.TournamentInfo;
            viewData.FriendlyRegistrationName = this.FriendlyRegistrationName;
            viewData.SiteName = DetermineSiteName();
            viewData.PathToSiteCssFile = DetermineSiteCssFile();
        }

        /// <summary>
        /// Handle HTTP GET requests for the Soon page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the same page with the viewData
        /// populated.
        /// </returns>
        [HttpGet]
        public ActionResult Soon()
        {
            BaseViewData baseViewData = new(Repository)
            {
                Config = Repository.Config,
                TournamentInfo = Repository.TournamentInfo
            };

            SetBaseViewData(baseViewData);
            return View((object)baseViewData);
        }

        /// <summary>
        /// Gets whether this registration system is coming soon, down, closed, or available.
        /// </summary>
        public BaseRegistrationController.RegistrationState CurrentRegistrationState
        {
            get
            {
                // Enable a user to bypass these checks
                ////if (!string.IsNullOrWhiteSpace(Request.QueryString["Test"]) &&
                ////    Request.QueryString["Test"] == DataLayer.OdysseyConfig["TestGuid"])
                ////{
                ////    return RegistrationState.Available;
                ////}

                BaseViewData baseViewData = new(Repository)
                {
                    Config = Repository.Config,
                    TournamentInfo = Repository.TournamentInfo
                };

                SetBaseViewData(baseViewData);

                // Is it too early to register?
                if (IsRegistrationComingSoon(this.CurrentRegistrationType))
                {
                    return BaseRegistrationController.RegistrationState.Soon;
                }

                // Is registration temporarily disabled?
                if (IsRegistrationDown(this.CurrentRegistrationType))
                {
                    return BaseRegistrationController.RegistrationState.Down;
                }

                // Is it too late to register?
                return IsRegistrationClosed(this.CurrentRegistrationType)
                    ? BaseRegistrationController.RegistrationState.Closed
                    : BaseRegistrationController.RegistrationState.Available;
            }
        }

        /// <summary>
        /// Gets or sets the current registration type.
        /// </summary>
        public BaseRegistrationController.RegistrationType CurrentRegistrationType { get; set; }

        /// <summary>
        /// Gets or sets the friendly, i.e. displayable, registration name.
        /// </summary>
        public required string FriendlyRegistrationName { get; set; }

        /// <summary>
        /// Determines the availability of a registration system.
        /// </summary>
        public enum RegistrationState
        {
            /// <summary>
            /// The registration system is available.
            /// </summary>
            Available,

            /// <summary>
            /// The registration system is closed for the year.
            /// </summary>
            Closed,

            /// <summary>
            /// The registration system is temporarily down.
            /// </summary>
            Down,

            /// <summary>
            /// The registration system is not open for the season yet.
            /// </summary>
            Soon
        }

        /// <summary>
        /// The registration type.
        /// </summary>
        public enum RegistrationType
        {
            /// <summary>
            /// The default, i.e. no registration type.  This should never be used.
            /// </summary>
            None,
            
            /// <summary>
            /// Identifies Tournament Registration.
            /// </summary>
            Tournament,

            /// <summary>
            /// Identifies Judges Registration.
            /// </summary>
            Judges,

            /// <summary>
            /// Identifies Coaches Training Registration.
            /// </summary>
            CoachesTraining,

            /// <summary>
            /// Identifies Volunteer Registration.
            /// </summary>
            Volunteer
        }
    }
}
