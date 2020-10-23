using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    /// <summary>
    /// ضریب تعدیل شعب
    /// </summary>
    [Table("AdjustmentFactor", Schema = "MeritPay")]
    public class AdjustmentFactor: EntityBase
    {
        public AdjustmentFactor()
        {

        }


        //[Key]
        //public int AdjustmentFactorId { get; set; }

        [Display(Name = "دسته بندی")]
        public string Title { get; set; }

        [Display(Name = "ضریب تعدیل")]
        public int Ratio { get; set; }


        #region Relations

        public virtual List<BranchGrouping> BranchGrouping { get; set; }

        #endregion
    }
}
