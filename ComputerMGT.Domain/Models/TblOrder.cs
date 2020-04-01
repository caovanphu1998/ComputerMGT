using System;
using System.Collections.Generic;

namespace ComputerMGT.Domain.Models
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderDetail = new HashSet<TblOrderDetail>();
        }

        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public long DateCreate { get; set; }
        public int Total { get; set; }

        public virtual TblUser User { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetail { get; set; }
    }
}
