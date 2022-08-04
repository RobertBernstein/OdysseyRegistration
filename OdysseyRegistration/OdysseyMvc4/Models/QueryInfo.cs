// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryInfo.cs" company="Tardis Technologies">
//   Copyright 2013 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The query info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The query info.
    /// </summary>
    public class QueryInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryInfo"/> class.
        /// </summary>
        public QueryInfo()
        {
            this.CsFieldMap = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the original SQL.
        /// </summary>
        public string OriginalSql { get; set; }

        ////public String RestrictingSQL;

        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the cs field map.
        /// </summary>
        public Dictionary<string, string> CsFieldMap { get; set; }
    }
}
