using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("MeritPayLimit", Schema = "MeritPay")]
    public class MeritPayLimit: EntityBase
    {

        public MeritPayLimit()
        {

        }

        //[Key]
        //public int MeritPayLimitId { get; set; }

        public int PeriodId { get; set; }

        [Display(Name = "حد بالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int MaxDay { get; set; }

        [Display(Name = "حد پایین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int MinDay { get; set; }


        #region Relations

        public virtual Period Period { get; set; }



        #endregion
    }
}
