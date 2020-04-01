using System;
using System.Collections.Generic;

namespace ComputerMGT.Domain.Models
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblCart = new HashSet<TblCart>();
            TblOrderDetail = new HashSet<TblOrderDetail>();
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageLink { get; set; }
        public Guid CategoryId { get; set; }

        public virtual TblCategory Category { get; set; }
        public virtual ICollection<TblCart> TblCart { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetail { get; set; }
    }
}
