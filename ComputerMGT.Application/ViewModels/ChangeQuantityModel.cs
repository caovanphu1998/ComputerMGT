using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerMGT.Application.ViewModels
{
    public class ChangeQuantityModel
    {
        public Guid CartId { get; set; }
        public int quantity { get; set; }
    }
}
