using System;
using System.Collections.Generic;

namespace ComputerMGT.Domain.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblProduct = new HashSet<TblProduct>();
        }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TblProduct> TblProduct { get; set; }
    }
}
