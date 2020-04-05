using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepository<TblOrderDetail> _orderDetailRepository;
        private readonly IRepository<TblProduct> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region Contructor        
        public OrderDetailService(IUnitOfWork unitOfWork
            , IRepository<TblOrderDetail> orderDetailRepository
            , IRepository<TblProduct> productRepository)
        {
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
        }
        #endregion

        #region Create orderDetail        
        /// <summary>
        /// Creates the order detail.
        /// </summary>
        /// <param name="OrderId">The order identifier.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public async Task<bool> CreateOrderDetail(Guid OrderId, int quantity, Guid ProductId)
        {
            _orderDetailRepository.Insert(new TblOrderDetail
            {
                DetailId = Guid.NewGuid(),
                OrderId = OrderId,
                ProductId = ProductId,
                Quantity = quantity,
            });
            await _unitOfWork.CommitAsync();
            return true;
        }
        #endregion

        #region Get List OrderDetail        
        /// <summary>
        /// Gets the list order detail by.
        /// </summary>
        /// <param name="OrderId">The order identifier.</param>
        /// <returns>Task&lt;List&lt;OrderDetailReturnModel&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<OrderDetailReturnModel>> GetListOrderDetail(Guid OrderId)
        {
            return _orderDetailRepository.GetManyAsNoTracking(x => x.OrderId == OrderId)
                .Join(_productRepository.GetAllAsNoTracking()
                , x => x.ProductId, y => y.ProductId, (x, y) => new OrderDetailReturnModel
                {
                    OrderId = OrderId,
                    DetailId = x.DetailId,
                    productName = y.Name,
                    price = y.Price,
                    Quantity = x.Quantity
                }).ToList();
        }
        #endregion
    }
}
