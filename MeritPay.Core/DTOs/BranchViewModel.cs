using System;

namespace MeritPay.Core.DTOs
{
    public partial class BranchViewModel
    {
        public long Id { get; set; }
        public string EnName { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
        public long Branch { get; set; }
        public string Name { get; set; }
        public long Area { get; set; }
        public long Zone { get; set; }
        public int? GhesmatkhedmatID { get; set; }
        public string FullName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
