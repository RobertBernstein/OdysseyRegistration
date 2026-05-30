// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRegistrationController.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base registration controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.Controllers
{
    using System;
    using System.Net.Mail;
    using System.Threading;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;

    // TODO: Determine whether to re-add Elmah.
    //using Elmah;

    using OdysseyCoreMvc.Models;
    using OdysseyCoreMvc.Pages;

    /// <summary>
    /// The base registration controller.
    /// </summary>
    public class BaseRegistrationController : Controller
    {
        /// <summary>
        /// The object that provides access to the database.
        /// </summary>
        protected readonly OdysseyRepository Repository = new();

        [HttpGet]
        public ActionResult BadEmail()
        {
            // TODO: What do we do with this?
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);

            // TODO: Test that this is the correct path to the BadEmail page, Rob - 01/18/2015.
            return View("~/Views/Shared/BadEmail.cshtml");
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
        protected MailMessage? BuildMessage(string from, string subject, string body, string to, string bcc, string cc)
        {
            MailMessage mailMessage = new()
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
            // TODO: What do we do with this?
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);
            //return View(viewData);
            return View();
        }

        /// <summary>
        /// Handle HTTP POST requests for the Closed page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the home page.
        /// </returns>
        /// <remarks>
        /// The ActionName attribute specifies the name of the function for which the MVC routing will search.
        /// </remarks>
        [ActionName("Closed")]
        [HttpPost]
        public ActionResult ClosedPost()
        {
            // TODO: What do we do with this?
            //var viewData = new BasePageModel();
            //SetBaseViewData(viewData);
            //return Redirect(viewData.Config["HomePage"]);
            return View();
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
            // TODO: What do we do with this?
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);
            //return View(viewData);
            return View();
        }

        /// <summary>
        /// Handle HTTP POST requests for the Down page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the home page.
        /// </returns>
        /// <remarks>
        /// The ActionName attribute specifies the name of the function for which the MVC routing will search.
        /// </remarks>
        [ActionName("Down")]
        [HttpPost]
        public ActionResult DownPost()
        {
            // TODO: What do we do with this?
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);
            //return Redirect(viewData.Config["HomePage"]);
            return View();
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
            // TODO: What do we do with this?
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);
            //return View(viewData);
            return View();
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
            // TODO: What do we do with this?
            //DateTime registrationCloseDateTime;
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);

            //try
            //{
            //    registrationCloseDateTime = DateTime.Parse(viewData.Config[registrationType + "RegistrationCloseDateTime"]);
            //}
            //catch (Exception)
            //{
            //    // If we cannot even read the closing time from the database, close down registration!
            //    return true;
            //}

            //// TODO: Add app setting to decide what timezone to consider local.
            //// TODO: Adjust for Eastern Time if not in Eastern Time, e.g. Pacific Time
            //DateTime currentEasternTime = DateTime.Now; ////.AddHours(3);
            //return DateTime.Compare(registrationCloseDateTime, currentEasternTime) < 0;
            return false;
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
            // TODO: What do we do with this?
            //DateTime registrationOpenDate;
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);

            //try
            //{
            //    registrationOpenDate = DateTime.Parse(viewData.Config[registrationType + "RegistrationOpenDateTime"]);
            //}
            //catch (Exception)
            //{
            //    // If we cannot even read the opening time from the database, assume we've passed the opening date.
            //    return false;
            //}

            //// TODO: Add app setting to decide what timezone to consider local.
            //// TODO: Adjust for Eastern Time if not in Eastern Time, e.g. Pacific Time
            //DateTime currentEasternTime = DateTime.Now; ////.AddHours(3);
            //return DateTime.Compare(currentEasternTime, registrationOpenDate) < 0;
            return false;
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
            // TODO: What do we do with this?
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);

            //bool registrationIsDown;
            //bool.TryParse(viewData.Config["Is" + registrationType + "RegistrationDown"], out registrationIsDown);
            //return registrationIsDown;
            return false;
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
        public string? SendMessage(BasePageModel viewData, MailMessage mailMessage)
        {
            SmtpClient smtpClient = new()
            {
                Host = viewData.Config["EmailServer"],
                Credentials = new System.Net.NetworkCredential(
                    viewData.Config["WebmasterEmail"],
                    viewData.Config["WebmasterEmailPassword"])
            };

            // Send the mail message
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException exception)
            {
                // TODO: Re-add Elmah.
                //ErrorSignal.FromCurrentContext().Raise(exception);

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
                // TODO: Re-add Elmah.
                //ErrorSignal.FromCurrentContext().Raise(smtpException);

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
        protected void SetBaseViewData(BasePageModel viewData)
        {
            // TODO: Do we need the next line (or any of these)?
            //viewData.Config = Repository.Config;
            //viewData.RegionName = Repository.RegionName;
            //viewData.RegionNumber = Repository.RegionNumber;
            //viewData.TournamentInfo = Repository.TournamentInfo;
            //viewData.FriendlyRegistrationName = FriendlyRegistrationName;
            //viewData.SiteName = DetermineSiteName();
            //viewData.PathToSiteCssFile = DetermineSiteCssFile();
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
            // TODO: What do we do with this?
            //BasePageModel viewData = new();
            //SetBaseViewData(viewData);
            //return View(viewData);
            return View();
        }

        /// <summary>
        /// Handle HTTP POST requests for the Soon page.
        /// </summary>
        /// <returns>
        /// An ActionResult that sends the caller back to the home page.
        /// </returns>
        /// <remarks>
        /// The ActionName attribute specifies the name of the function for which the MVC routing will search.
        /// </remarks>
        [ActionName("Soon")]
        [HttpPost]
        public ActionResult SoonPost()
        {
            // TODO: What do we do with this?
            //var viewData = new BasePageModel();
            //SetBaseViewData(viewData);
            //return Redirect(viewData.Config["HomePage"]);
            return View();
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

                // TODO: What do we do with this?
                //BasePageModel viewData = new();
                //SetBaseViewData(viewData);

                //// Is it too early to register?
                //if (IsRegistrationComingSoon(CurrentRegistrationType))
                //{
                //    return RegistrationState.Soon;
                //}

                //// Is registration temporarily disabled?
                //if (IsRegistrationDown(CurrentRegistrationType))
                //{
                //    return RegistrationState.Down;
                //}

                //// Is it too late to register?
                //return IsRegistrationClosed(CurrentRegistrationType)
                //           ? RegistrationState.Closed
                //           : RegistrationState.Available;
                return RegistrationState.Available;
            }
        }

        /// <summary>
        /// Gets or sets the current registration type.
        /// </summary>
        public RegistrationType CurrentRegistrationType { get; set; }

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
