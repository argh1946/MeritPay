using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("ScoreSubIndex", Schema = "MeritPay")]
    public class ScoreSubIndex : EntityBase
    {
        public ScoreSubIndex()
        {

        }

        //[Key]
        //public int ScoreSubIndexId { get; set; }

        public int ScoreIndexId { get; set; }

        [Display(Name = "نام زیر شاخص")]
        public string Title { get; set; }

        [Display(Name = "ضریب زیر شاخص")]
        public int Ratio { get; set; }




        #region Relations

        public virtual ScoreIndex ScoreIndex { get; set; }
        public virtual List<PersonScore> PersonScore { get; set; }

        #endregion
    }
}
