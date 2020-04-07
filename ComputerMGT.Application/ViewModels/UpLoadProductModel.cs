using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerMGT.Application.ViewModels
{
    public class UpLoadProductModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string file { get; set; }
        public Guid CategoryId { get; set; }
    }
}
