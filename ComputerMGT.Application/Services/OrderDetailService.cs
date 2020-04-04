using ComputerMGT.Application.Interfaces;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepository<TblOrderDetail> _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region Contructor        
        public OrderDetailService(IUnitOfWork unitOfWork
            , IRepository<TblOrderDetail> orderDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
        }
        #endregion

        public async Task<bool> CreateOrderDetail(Guid OrderId, int quantity, Guid ProductId)
        {
            _orderDetailRepository.Insert(new TblOrderDetail
            {
                DetailId = Guid.NewGuid(),
                OrderId = OrderId,
                ProductId = ProductId,
                Quantity =quantity,
            });
            await _unitOfWork.CommitAsync();
            return true;
        }
        
    }
}
