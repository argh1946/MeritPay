using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    /// <summary>
    /// دسته بندی
    /// </summary>
    [Table("BranchGrouping", Schema = "MeritPay")]
    public class BranchGrouping: EntityBase
    {
        public BranchGrouping()
        {
        }


        //[Key]
        //public int BranchGroupingId { get; set; }
        public int GroupingRatioId { get; set; }
        public int AdjustmentFactorId { get; set; }
        public int BranchId { get; set; }
        
               


        #region Relations

        public virtual Period Period { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual GroupingRatio GroupingRatio { get; set; }
        public virtual AdjustmentFactor AdjustmentFactor { get; set; }
        public virtual List<BranchGroupingInPeriod> BranchGroupingInPeriod { get; set; }

        #endregion
    }
}
