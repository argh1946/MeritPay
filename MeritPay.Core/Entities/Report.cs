using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    /// <summary>
    /// کارنامه
    /// </summary>
    [Table("Report", Schema = "MeritPay")]
    public class Report : EntityBase
    {
        public Report()
        {

        }

        //[Key]
        //public int ReportId { get; set; }
        public int MeritPayFactorId { get; set; }
        public int PersonInBranchId { get; set; }

       
        [Display(Name = "امتیاز")]
        public int Score { get; set; }
        [Display(Name = "رتبه در شعبه")]
        public int RankInBranch { get; set; }
        [Display(Name = "رتبه در منطقه")]
        public int RankInZone { get; set; }
        [Display(Name = "رتبه در بانک")]
        public int RankInBank { get; set; }



        #region Relations

        public virtual MeritPayFactor MeritPayFactor { get; set; }
        public virtual PersonInBranch PersonInBranch { get; set; }


        #endregion
    }
}
