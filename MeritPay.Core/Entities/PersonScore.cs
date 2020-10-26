using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("PersonScore", Schema = "MeritPay")]
    public class PersonScore : EntityBase
    {
        public PersonScore()
        {

        }

        //[Key]
        //public int PersonScoreId { get; set; }
        public int ScoreSubIndexId { get; set; }
        public int PersonInBranchId { get; set; }

        [Display(Name = "ارزش")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        [Display(Name = "امتیاز")]
        public int Score { get; set; }
        [Display(Name = "رتبه در شعبه")]
        public int RankInBranch { get; set; }
        [Display(Name = "رتبه در منطقه")]
        public int RankInZone { get; set; }
        [Display(Name = "رتبه در بانک")]
        public int RankInBank { get; set; }




        #region Relations

        public virtual ScoreSubIndex ScoreSubIndex { get; set; }
        public virtual PersonInBranch PersonInBranch { get; set; }

        #endregion

    }
}
