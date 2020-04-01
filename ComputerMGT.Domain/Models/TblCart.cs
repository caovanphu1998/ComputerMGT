using System;
using System.Collections.Generic;

namespace ComputerMGT.Domain.Models
{
    public partial class TblCart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual TblProduct Product { get; set; }
        public virtual TblUser User { get; set; }
    }
}
