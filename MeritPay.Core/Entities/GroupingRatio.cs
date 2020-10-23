using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("GroupingRatio", Schema = "MeritPay")]
    public class GroupingRatio: EntityBase
    {
        public GroupingRatio()
        {

        }


        //[Key]
        //public int GroupingRatioId { get; set; }

        [Display(Name = "دسته بندی")]
        public string Title { get; set; }

        [Display(Name = "ضریب")]
        public float Ratio { get; set; }


        #region Relations

        public virtual List<BranchGrouping> BranchGrouping { get; set; }

        #endregion

    }
}
