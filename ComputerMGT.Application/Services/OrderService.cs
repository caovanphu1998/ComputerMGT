using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<TblOrder> _orderRepository;
        private readonly IRepository<TblCart> _cartRepository;
        private readonly IRepository<TblUser> _userRepository;
        private readonly ICartService _cartService;
        private readonly IOrderDetailService _orderDetailService;
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
            , IRepository<TblUser> userRepository
            , ICartService cartService
            , IOrderDetailService orderDetailService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _cartService = cartService;
            _orderDetailService = orderDetailService;
            _userRepository = userRepository;
        }
        #endregion

        #region Create Order        
        /// <summary>
        /// Creates the oder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public async Task<bool> CreateOder(CreateOrderModel model)
        {
            var order = _orderRepository.Insert(new TblOrder
            {
                OrderId = Guid.NewGuid(),
                DateCreate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                Total = await _cartService.TotalMoney(model.UserId),
                UserId = model.UserId,
            });
            var query = _cartRepository.GetManyAsNoTracking(x => x.UserId == model.UserId);
            foreach (TblCart element in query.ToList())
            {
                await _orderDetailService.CreateOrderDetail(order.Entity.OrderId, element.Quantity, element.ProductId);
            }
            await _cartService.DeleteCart(model.UserId);
            await _unitOfWork.CommitAsync();
            return true;
        }
        #endregion

        #region get order        
        /// <summary>
        /// Getalls the order.
        /// </summary>
        /// <returns>List&lt;OrderReturnModel&gt;.</returns>
        public async Task<List<OrderReturnModel>> getallOrder()
        {
            return _orderRepository.GetAllAsNoTracking().Join(_userRepository.GetAllAsNoTracking()
                , x => x.UserId, y => y.UserId, (x, y) => new OrderReturnModel
                {
                    OrderId = x.OrderId,
                    Date = x.DateCreate,
                    Total = x.Total,
                    UserId = x.UserId,
                    UserName = y.Name
                }).ToList();
        }
        #endregion
    }
}
