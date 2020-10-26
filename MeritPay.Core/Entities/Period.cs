using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    /// <summary>
    /// دوره
    /// </summary>
    [Table("Period", Schema = "MeritPay")]
    public class Period : EntityBase
    {
        public Period()
        {
        }

        //[Key]
        //public int PeriodId { get; set; }

        [Display(Name = "دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PeriodTitle { get; set; }

        [Display(Name = "شروع دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "پایان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime DateTo { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }


        #region Relations

        public virtual List<BranchGrouping> BranchGrouping { get; set; }
        public virtual List<MeritPayLimit> MeritPayLimit { get; set; }
        public virtual List<MeritPayFactor> MeritPayFactor { get; set; }
        public virtual List<BranchGroupingInPeriod> BranchGroupingInPeriod { get; set; }
        public virtual List<PersonArzeshyabi> PersonArzeshyabi { get; set; }

        #endregion
    }
}
