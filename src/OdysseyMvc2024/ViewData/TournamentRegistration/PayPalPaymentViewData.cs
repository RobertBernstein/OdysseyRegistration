// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PayPalPaymentViewData.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the view model for the standalone PayPal payment page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc2024.ViewData.TournamentRegistration
{
    /// <summary>
    /// Backing data for the standalone PayPal payment page that allows users to pay later.
    /// </summary>
    public class PayPalPaymentViewData : BaseViewData
    {
        public bool AcceptingPayPal
        {
            get
            {
                bool.TryParse(Config[nameof(AcceptingPayPal)], out bool flag);
                return flag;
            }
        }

        public string CoachLastName { get; set; } = string.Empty;

        public string TeamTournamentId { get; set; } = string.Empty;
    }
}
