using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    /// <summary>
    /// شاخص
    /// </summary>
    [Table("ScoreIndex", Schema = "MeritPay")]
    public class ScoreIndex : EntityBase
    {
        public ScoreIndex()
        {

        }
        //[Key]
        //public int ScoreIndexId { get; set; }
        public int MeritPayFactorId { get; set; }

        [Display(Name = "نام شاخص")]
        public string Title { get; set; }

        [Display(Name = "ضریب شاخص")]
        public int Ratio { get; set; }


        #region Relations

        public virtual MeritPayFactor MeritPayFactor { get; set; }
        public virtual List<ScoreSubIndex> ScoreSubIndex { get; set; }


        #endregion
    }
}
