using System;
using System.Collections.Generic;

namespace ComputerMGT.Domain.Models
{
    public partial class TblOrderDetail
    {
        public Guid DetailId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? UnitCost { get; set; }

        public virtual TblOrder Order { get; set; }
        public virtual TblProduct Product { get; set; }
    }
}
