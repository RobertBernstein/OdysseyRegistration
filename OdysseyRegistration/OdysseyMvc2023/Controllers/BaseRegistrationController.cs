// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.BaseRegistrationController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web.Mvc;
using OdysseyMvc2023.Models;
using OdysseyMvc2023.ViewData;

namespace OdysseyMvc2023.Controllers
{
    public class BaseRegistrationController : Controller
    {
        protected readonly OdysseyRepository Repository = new OdysseyRepository();

        [HttpGet]
        public ActionResult BadEmail()
        {
            this.SetBaseViewData(new BaseViewData());
            return (ActionResult)this.View();
        }

        protected MailMessage BuildMessage(
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
            string str = to;
            char[] chArray = new char[1] { ',' };
            foreach (string address in str.Split(chArray))
            {
                try
                {
                    mailMessage.To.Add(new MailAddress(address));
                }
                catch (FormatException ex)
                {
                    return (MailMessage)null;
                }
            }
            if (!string.IsNullOrWhiteSpace(bcc))
                mailMessage.Bcc.Add(new MailAddress(bcc));
            if (!string.IsNullOrWhiteSpace(cc))
                mailMessage.CC.Add(new MailAddress(cc));
            return mailMessage;
        }

        [HttpGet]
        public ActionResult Closed()
        {
            BaseViewData baseViewData = new BaseViewData();
            this.SetBaseViewData(baseViewData);
            return (ActionResult)this.View((object)baseViewData);
        }

        private string DetermineSiteCssFile() => this.Request.Url != (Uri)null && this.Request.Url.AbsoluteUri.ToLowerInvariant().Contains("novasouth") ? this.Url.Content("~/Content/NovaSouth.css") : this.Url.Content("~/Content/NovaNorth.css");

        private string DetermineSiteName()
        {
            string siteName = this.Request.Url != (Uri)null ? this.Request.Url.Host.ToLowerInvariant() : "novanorth.org";
            if (siteName.StartsWith("www."))
                siteName = siteName.Substring(4);
            return siteName;
        }

        [HttpGet]
        public ActionResult Down()
        {
            BaseViewData baseViewData = new BaseViewData();
            this.SetBaseViewData(baseViewData);
            return (ActionResult)this.View((object)baseViewData);
        }

        [HttpGet]
        public ActionResult Error()
        {
            BaseViewData baseViewData = new BaseViewData();
            this.SetBaseViewData(baseViewData);
            return (ActionResult)this.View((object)baseViewData);
        }

        public string GetFriendlyRegistrationName()
        {
            if (this.CurrentRegistrationType == BaseRegistrationController.RegistrationType.None)
                return string.Empty;
            return this.CurrentRegistrationType == BaseRegistrationController.RegistrationType.CoachesTraining ? "Coaches Training Registration" : this.CurrentRegistrationType.ToString() + " Registration";
        }

        public bool IsRegistrationClosed(
          BaseRegistrationController.RegistrationType registrationType)
        {
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            DateTime t1;
            try
            {
                t1 = DateTime.Parse(viewData.Config[registrationType.ToString() + "RegistrationCloseDateTime"]);
            }
            catch (Exception ex)
            {
                return true;
            }
            DateTime now = DateTime.Now;
            return DateTime.Compare(t1, now) < 0;
        }

        public bool IsRegistrationComingSoon(
          BaseRegistrationController.RegistrationType registrationType)
        {
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            DateTime t2;
            try
            {
                t2 = DateTime.Parse(viewData.Config[registrationType.ToString() + "RegistrationOpenDateTime"]);
            }
            catch (Exception ex)
            {
                return false;
            }
            return DateTime.Compare(DateTime.Now, t2) < 0;
        }

        public bool IsRegistrationDown(
          BaseRegistrationController.RegistrationType registrationType)
        {
            BaseViewData viewData = new BaseViewData();
            this.SetBaseViewData(viewData);
            bool result;
            bool.TryParse(viewData.Config["Is" + (object)registrationType + "RegistrationDown"], out result);
            return result;
        }

        public string SendMessage(BaseViewData viewData, MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = viewData.Config["EmailServer"],
                Credentials = (ICredentialsByHost)new NetworkCredential(viewData.Config["WebmasterEmail"], viewData.Config["WebmasterEmailPassword"])
            };
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                // TODO: Install ELMAH.
                //ErrorSignal.FromCurrentContext().Raise((Exception)ex);
                foreach (SmtpFailedRecipientException innerException in ex.InnerExceptions)
                {
                    switch (innerException.StatusCode)
                    {
                        case SmtpStatusCode.MailboxBusy:
                        case SmtpStatusCode.MailboxUnavailable:
                            Thread.Sleep(5000);
                            smtpClient.Send(mailMessage);
                            continue;
                        default:
                            return string.Format("Failed to deliver message to {0}", (object)innerException.FailedRecipient);
                    }
                }
            }
            catch (SmtpException ex)
            {
                // TODO: Install ELMAH.
                //ErrorSignal.FromCurrentContext().Raise((Exception)ex);
                return ex.StatusCode.ToString();
            }
            return (string)null;
        }

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

        [HttpGet]
        public ActionResult Soon()
        {
            BaseViewData baseViewData = new BaseViewData();
            this.SetBaseViewData(baseViewData);
            return (ActionResult)this.View((object)baseViewData);
        }

        public BaseRegistrationController.RegistrationState CurrentRegistrationState
        {
            get
            {
                this.SetBaseViewData(new BaseViewData());
                if (this.IsRegistrationComingSoon(this.CurrentRegistrationType))
                    return BaseRegistrationController.RegistrationState.Soon;
                return this.IsRegistrationDown(this.CurrentRegistrationType) ? BaseRegistrationController.RegistrationState.Down : (this.IsRegistrationClosed(this.CurrentRegistrationType) ? BaseRegistrationController.RegistrationState.Closed : BaseRegistrationController.RegistrationState.Available);
            }
        }

        public BaseRegistrationController.RegistrationType CurrentRegistrationType { get; set; }

        public string FriendlyRegistrationName { get; set; }

        public enum RegistrationState
        {
            Available,
            Closed,
            Down,
            Soon,
        }

        public enum RegistrationType
        {
            None,
            Tournament,
            Judges,
            CoachesTraining,
            Volunteer,
        }
    }
}
