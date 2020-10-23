using SampleProject.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeritPay.Core.DTOs
{
    public class ReportDataOutput : ResponseMessage
    {
        public ReportDataVM Data { get; private set; }
        public ReportDataOutput(bool success, ReportDataVM data, string message = null) : base(success, message)
        {
            Data = data;
        }
    }
   

    public class ReportDataVM
    {
        public int PersonCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string EmployeeDate { get; set; }
        public string Grade { get; set; }
        public string StudyBranch { get; set; }
        public string StudyJob { get; set; }

        public int TaScore { get; set; }
        public int TaRankInBranch { get; set; }
        public int TaRankInZone { get; set; }
        public int TaRankInBank { get; set; }

        public int GroupScore { get; set; }
        public int GroupRankInBranch { get; set; }
        public int GroupRankInZone { get; set; }
        public int GroupRankInBank { get; set; }

        public int ArzeshScore { get; set; }
        public int ArzeshRankInBranch { get; set; }
        public int ArzeshRankInZone { get; set; }
        public int ArzeshRankInBank { get; set; }

        public int TotalScore { get; set; }
        public int TotalRankInBranch { get; set; }
        public int TotalRankInZone { get; set; }
        public int TotalRankInBank { get; set; }
    }
}
