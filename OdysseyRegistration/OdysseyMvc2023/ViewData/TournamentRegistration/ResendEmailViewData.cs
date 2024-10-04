// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResendEmailViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Backing data for the email that gets resent to the requester.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.ResendEmailViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    /// <summary>
    /// Backing data for the email that gets resent to the requester.
    /// </summary>
    public class ResendEmailViewData : BaseViewData
    {
        public string AltCoachCheckbox { get; set; }

        public string CoachCheckbox { get; set; }

        public string ErrorMessage { get; set; }

        public bool Success { get; set; }

        public int TeamNumber { get; set; }
    }
}
