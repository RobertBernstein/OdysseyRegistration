// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base registration controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Controllers
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading;
    using System.Web.Mvc;

    using Elmah;

    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;

    /// <summary>
    /// The base registration controller.
    /// </summary>
    public class BaseRegistrationController : Controller
    {
        /// <summary>
        /// The object that provides access to the database.
        /// </summary>
        protected readonly OdysseyRepository Repository = new OdysseyRepository();

        [HttpGet]
        public ActionResult BadEmail()
        {
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            return this.View();
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
        protected MailMessage BuildMessage(string from, string subject, string body, string to, string bcc, string cc)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                Priority = MailPriority.Normal
            };

            // Set the recepient address of the mail message.
            string[] recipients = to.Split(new[] { ',' });
            foreach (string recipient in recipients)
            {
                try
                {
                    mailMessage.To.Add(new MailAddress(recipient));
                }
                catch (FormatException)
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
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        private string DetermineSiteCssFile()
        {
            if ((this.Request.Url != null) && this.Request.Url.AbsoluteUri.ToLowerInvariant().Contains("novasouth"))
            {
                return this.Url.Content("~/Content/NovaSouth.css");
            }

            return this.Url.Content("~/Content/NovaNorth.css");
        }

        private string DetermineSiteName()
        {
            string siteName = (this.Request.Url != null)
                ? this.Request.Url.Host.ToLowerInvariant()
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
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
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
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        public string GetFriendlyRegistrationName()
        {
            // Make sure that CurrentRegistrationType has been set before calling this method.
            // TODO: we should probably assert here if CurrentRegistrationType has not been set.
            if (this.CurrentRegistrationType == RegistrationType.None)
            {
                return string.Empty;
            }

            if (this.CurrentRegistrationType == RegistrationType.CoachesTraining)
            {
                return "Coaches Training Registration";
            }

            return this.CurrentRegistrationType + " Registration";
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
        public bool IsRegistrationClosed(RegistrationType registrationType)
        {
            DateTime registrationCloseDateTime;
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);

            try
            {
                registrationCloseDateTime = DateTime.Parse(viewData.Config[registrationType + "RegistrationCloseDateTime"]);
            }
            catch (Exception)
            {
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
        public bool IsRegistrationComingSoon(RegistrationType registrationType)
        {
            DateTime registrationOpenDate;
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);

            try
            {
                registrationOpenDate = DateTime.Parse(viewData.Config[registrationType + "RegistrationOpenDateTime"]);
            }
            catch (Exception)
            {
                // If we cannot even read the opening time from the database, assume we've passed the opening date.
                return false;
            }

            // TODO: Adjust for Eastern Time if not in Eastern Time, e.g. Pacific Time
            DateTime currentEasternTime = DateTime.Now; ////.AddHours(3);
            return DateTime.Compare(currentEasternTime, registrationOpenDate) < 0;
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
        public bool IsRegistrationDown(RegistrationType registrationType)
        {
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);

            bool registrationIsDown;
            bool.TryParse(viewData.Config["Is" + registrationType + "RegistrationDown"], out registrationIsDown);
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
        public string SendMessage(BaseViewData viewData, MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient
            {
                Host = viewData.Config["EmailServer"],
                Credentials =
                    new NetworkCredential(viewData.Config["WebmasterEmail"], viewData.Config["WebmasterEmailPassword"])
            };

            // Send the mail message
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException exception)
            {
                ErrorSignal.FromCurrentContext().Raise(exception);

                foreach (SmtpFailedRecipientException smtpFailedRecipientException in exception.InnerExceptions)
                {
                    SmtpStatusCode status = smtpFailedRecipientException.StatusCode;
                    switch (status)
                    {
                        case SmtpStatusCode.MailboxBusy:
                        case SmtpStatusCode.MailboxUnavailable:
                            Thread.Sleep(5000);
                            smtpClient.Send(mailMessage);
                            break;

                        default:
                            return string.Format("Failed to deliver message to {0}", smtpFailedRecipientException.FailedRecipient);
                    }
                }
            }
            catch (SmtpException smtpException)
            {
                ErrorSignal.FromCurrentContext().Raise(smtpException);

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
            viewData.SiteName = this.DetermineSiteName();
            viewData.PathToSiteCssFile = this.DetermineSiteCssFile();
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
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            return this.View(viewData);
        }

        /// <summary>
        /// Gets whether this registration system is coming soon, down, closed, or available.
        /// </summary>
        public RegistrationState CurrentRegistrationState
        {
            get
            {
                // Enable a user to bypass these checks
                ////if (!string.IsNullOrWhiteSpace(Request.QueryString["Test"]) &&
                ////    Request.QueryString["Test"] == DataLayer.OdysseyConfig["TestGuid"])
                ////{
                ////    return RegistrationState.Available;
                ////}

                BaseViewData viewData = new BaseViewData();
                this.SetBaseViewData(viewData);

                // Is it too early to register?
                if (this.IsRegistrationComingSoon(this.CurrentRegistrationType))
                {
                    return RegistrationState.Soon;
                }

                // Is registration temporarily disabled?
                if (this.IsRegistrationDown(this.CurrentRegistrationType))
                {
                    return RegistrationState.Down;
                }

                // Is it too late to register?
                return this.IsRegistrationClosed(this.CurrentRegistrationType)
                           ? RegistrationState.Closed
                           : RegistrationState.Available;
            }
        }

        /// <summary>
        /// Gets or sets the current registration type.
        /// </summary>
        public RegistrationType CurrentRegistrationType { get; set; }

        /// <summary>
        /// Gets or sets the friendly, i.e. displayable, registration name.
        /// </summary>
        public string FriendlyRegistrationName { get; set; }

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
