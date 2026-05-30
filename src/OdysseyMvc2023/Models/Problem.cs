// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.Problem
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

namespace OdysseyMvc2023.Models
{
    public class Problem
    {
        public int ProblemID { get; set; }

        public string ProblemCategory { get; set; }

        public string ProblemName { get; set; }

        public string ProblemDescription { get; set; }

        public string Divisions { get; set; }

        public string CostLimit { get; set; }

        public string ProblemCaptainID { get; set; }

        public string PCFirstName { get; set; }

        public string PCLastName { get; set; }

        public string PCAddress { get; set; }

        public string PCCity { get; set; }

        public string PCStateOrProvince { get; set; }

        public string PCPostalCode { get; set; }

        public string PCWorkPhone { get; set; }

        public string PCHomePhone { get; set; }

        public string PCMobilePhone { get; set; }

        public string PCFaxNumber { get; set; }

        public string PCEmail1 { get; set; }

        public string PCEmail2 { get; set; }

        public string Notes { get; set; }
    }
}
