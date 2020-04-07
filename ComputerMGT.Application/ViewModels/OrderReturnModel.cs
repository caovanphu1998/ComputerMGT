using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerMGT.Application.ViewModels
{
    public class OrderReturnModel
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public long Date { get; set; }
        public int Total { get; set; }
    }
}
