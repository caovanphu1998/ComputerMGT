using ComputerMGT.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOder(CreateOrderModel model);

        Task<List<OrderReturnModel>> getallOrder();
    }
}
