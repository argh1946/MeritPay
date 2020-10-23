using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    /// <summary>
    /// عوامل موثر در کارانه
    /// </summary>
    [Table("MeritPayFactor", Schema = "MeritPay")]
    public class MeritPayFactor: EntityBase
    {
        public MeritPayFactor()
        {

        }

        //[Key]
        //public int MeritPayFactorId { get; set; }
        public int PeriodId { get; set; }
        public int MeritPayTypeId { get; set; }

        [Display(Name = "نام")]
        public string Title { get; set; }

        [Display(Name = "ضریب اهمیت")]
        public int Ratio { get; set; }



        #region Relations

        public virtual MeritPayType MeritPayType { get; set; }
        public virtual Period Period { get; set; }
        public virtual List<ScoreIndex> ScoreIndex { get; set; }
        public virtual List<Report> Report { get; set; }


        #endregion
    }
}
