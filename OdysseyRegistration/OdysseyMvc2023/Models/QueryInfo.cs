// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.QueryInfo
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Collections.Generic;

namespace OdysseyMvc2023.Models
{
    public class QueryInfo
    {
        public QueryInfo() => this.CsFieldMap = new Dictionary<string, string>();

        public string OriginalSql { get; set; }

        public string TableName { get; set; }

        public Dictionary<string, string> CsFieldMap { get; set; }
    }
}
