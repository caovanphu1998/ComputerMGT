using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerMGT.Application.ViewModels
{
    public class AddProductToCartModel
    {
        public Guid UserId { get; set; }

        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
