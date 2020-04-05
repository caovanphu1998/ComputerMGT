using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerMGT.Application.ViewModels
{
    public class OrderDetailReturnModel
    {
        public Guid DetailId { get; set; }
        public Guid OrderId { get; set; }
        public string productName { get; set; }
        public int? Quantity { get; set; }
        public int price { get; set; }
    }
}
