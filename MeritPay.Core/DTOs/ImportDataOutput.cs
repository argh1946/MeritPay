using SampleProject.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeritPay.Core.DTOs
{
    public class ImportDataOutput : ResponseMessage
    {
        public ImportDataVM Data { get; private set; }
        public ImportDataOutput(bool success, ImportDataVM data, string message = null) : base(success, message)
        {
            Data = data;
        }
    }
   

    public class ImportDataVM
    {
        public int PersonCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDate { get; set; }

        public string EmployeeDate { get; set; }

        public string Grade { get; set; }

        public string StudyBranch { get; set; }
        public string StudyJob { get; set; }
    }
}
