using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("Branch", Schema = "MeritPay")]
    public class Branch : EntityBase
    {
        public Branch()
        {

        }

        //[Key]
        //public int BranchId { get; set; }

        [Display(Name = "کد شعبه")]
        public int BranchCode { get; set; }

        [Display(Name = "نام شعبه")]
        public string BranchName { get; set; }

        [Display(Name = "کد منطقه")]
        public int ZoneCode { get; set; }

        [Display(Name = "نام منطقه")]
        public string ZoneName { get; set; }



        #region Relations

        public virtual List<BranchGrouping> BranchGrouping { get; set; }
        public virtual List<PersonInBranch> PersonInBranch { get; set; }


        #endregion
    }
}
