using System;
using System.Collections.Generic;

namespace ComputerMGT.Domain.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblCart = new HashSet<TblCart>();
            TblOrder = new HashSet<TblOrder>();
        }

        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Role { get; set; }

        public virtual ICollection<TblCart> TblCart { get; set; }
        public virtual ICollection<TblOrder> TblOrder { get; set; }
    }
}
