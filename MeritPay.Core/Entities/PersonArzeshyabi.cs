using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("PersonArzeshyabi", Schema = "MeritPay")]
    public class PersonArzeshyabi : EntityBase
    {
        public PersonArzeshyabi()
        {

        }

        //[Key]
        //public int PersonScoreId { get; set; }
        public int PeriodId { get; set; }
        public int PersonInBranchId { get; set; }

        [Display(Name = "مقطع ارزیابی")]
        public string ArzyabiDate { get; set; }

        [Display(Name = "ارزیاب 1")]
        public int Arzyab1 { get; set; }

        [Display(Name = "ارزیاب 2")]
        public int Arzyab2 { get; set; }


        #region Relations

        public virtual Period Period { get; set; }
        public virtual PersonInBranch PersonInBranch { get; set; }

        #endregion

    }
}
