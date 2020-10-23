using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("BranchGroupingInPeriod", Schema = "MeritPay")]
    public class BranchGroupingInPeriod: EntityBase
    {
        public BranchGroupingInPeriod()
        {

        }
        //[Key]
        //public int BranchGroupingInPeriodId { get; set; }
        public int BranchGroupingId { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "منابع مردمی")]
        public decimal PublicSource { get; set; }
       

        [Display(Name = "تعداد اسناد")]
        public int DocumentCount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "تسهیلات از ابتدا افتتاح")]
        public decimal Facilities { get; set; }


        #region Relations
       
        public virtual Period Period { get; set; }
        public virtual BranchGrouping BranchGrouping { get; set; }

        #endregion
    }
}
