using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    [Table("PersonInBranch", Schema = "MeritPay")]
    public class PersonInBranch : EntityBase
    {
        public PersonInBranch()
        {

        }

        //[Key]
        //public int PersonInBranchId { get; set; }

        public int PersonId { get; set; }

        public int BranchId { get; set; }

        [Display(Name = "تاریخ جابجایی")]
        public int MoveDate { get; set; }



        #region Relations

        public virtual Branch Branch { get; set; }
        public virtual Person Person { get; set; }
        public virtual List<Report> Report { get; set; }
        public virtual List<PersonScore> PersonScore { get; set; }

        #endregion
    }
}
