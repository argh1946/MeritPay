using MeritPay.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeritPay.Core.Entities
{
    /// <summary>
    /// ماهیت وظایف
    /// </summary>
    [Table("MeritPayType", Schema = "MeritPay")]
    public class MeritPayType: EntityBase
    {
        public MeritPayType()
        {

        }
        //[Key]
        //public int MeritPayTypeId { get; set; }

        public string Title { get; set; }


        #region Relations

        public virtual List<MeritPayFactor> MeritPayFactor { get; set; }


        #endregion

    }
}
