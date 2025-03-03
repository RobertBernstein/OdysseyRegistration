// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page10ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page10ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page10ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using OdysseyMvc2024.Models;
using System;

namespace OdysseyMvc2024.ViewData.TournamentRegistration
{
    /// <summary>
    /// Backing data for Page 10 of the Tournament Registration wizard.
    /// </summary>
    public class Page10ViewData(IOdysseyRepository repository) : BaseViewData(repository)
    {
        public bool AcceptingPayPal
        {
            get
            {
                bool flag;
                bool.TryParse(this.Config[nameof(AcceptingPayPal)], out flag);
                return flag;
            }
        }

        public string? Division { get; set; }

        public string? JudgeErrorMessage { get; set; }

        public string? JudgeFirstName { get; set; }

        public string? JudgeLastName { get; set; }

        public string? MailBody { get; set; }

        public string? MailErrorMessage { get; set; }

        public string? PaymentDueDate
        {
            get
            {
                DateTime? paymentDueDate = this.TournamentInfo.PaymentDueDate;
                return paymentDueDate.HasValue ? paymentDueDate.Value.ToLongDateString() : "TBA";
            }
        }

        public string? ProblemName { get; set; }

        public string? SchoolName { get; set; }

        public required Models.TournamentRegistration? TournamentRegistration { get; set; }

        public string? VolunteerErrorMessage { get; set; }

        public string? VolunteerFirstName { get; set; }

        public string? VolunteerLastName { get; set; }
    }
}
