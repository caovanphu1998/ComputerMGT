using ComputerMGT.Application.Interfaces;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<TblOrder> _orderRepository;
        private readonly IRepository<TblCart> _cartRepository;
        private readonly CartService _cartService;
        private readonly OrderDetailService _orderDetailService;
        private readonly IUnitOfWork _unitOfWork;

        #region Contructor                        
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="orderRepository">The order repository.</param>
        /// <param name="cartRepository">The cart repository.</param>
        /// <param name="cartService">The cart service.</param>
        public OrderService(IUnitOfWork unitOfWork
            , IRepository<TblOrder> orderRepository
            , IRepository<TblCart> cartRepository
            , CartService cartService
            , OrderDetailService orderDetailService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _cartService = cartService;
            _orderDetailService = orderDetailService;
        }
        #endregion

        #region Create Order        
        /// <summary>
        /// Creates the oder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public async Task<bool> CreateOder(Guid UserId)
        {
            var order = _orderRepository.Insert(new TblOrder
            {
                OrderId = Guid.NewGuid(),
                DateCreate = DateTime.Now.ToFileTime(),
                Total = await _cartService.TotalMoney(UserId),
                UserId = UserId,
            });
            var query = _cartRepository.GetManyAsNoTracking(x => x.UserId == UserId);
            foreach (TblCart element in query.ToList())
            {
                await _orderDetailService.CreateOrderDetail(order.Entity.OrderId, element.Quantity, element.ProductId);
            }
            await _cartService.DeleteCart(UserId);
            await _unitOfWork.CommitAsync();
            return true;
        }
        #endregion
    }
}
