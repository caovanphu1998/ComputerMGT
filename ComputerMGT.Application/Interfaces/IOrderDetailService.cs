using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface IOrderDetailService
    {
        Task<bool> CreateOrderDetail(Guid OrderId, int quantity, Guid ProductId);
    }
}
