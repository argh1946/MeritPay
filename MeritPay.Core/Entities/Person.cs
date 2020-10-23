using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("Person", Schema = "MeritPay")]
    public class Person : EntityBase
    {
        public Person()
        {

        }

        //[Key]
        //public int PersonId { get; set; }

        [Display(Name = "کد پرسنلی")]
        public int PersonCode { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "تاریخ تولد")]
        public string BirthDate { get; set; }

        [Display(Name = "تاریخ استخدام")]
        public string EmployeeDate { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        public string Grade { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string StudyBranch { get; set; }

        [Display(Name = "رشته شغلی")]
        public string StudyJob { get; set; }


        #region Relations

        public virtual List<PersonInBranch> PersonInBranch { get; set; }        

        #endregion
    }
}
