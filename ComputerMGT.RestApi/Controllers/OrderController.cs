using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.RestApi.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace ComputerMGT.RestApi.Controllers
{
    [ApiController]
    [Route("/api/order")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        #region Constructor                
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderService">The order service.</param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region Create Order             
        /// <summary>
        /// Creates the order.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        [HttpPut]
        [Route("/api/order")]
        public async Task CreateOrder([FromBody] CreateOrderModel model)
        {
            await _orderService.CreateOder(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion

        #region get order           
        /// <summary>
        /// Gets all order.
        /// </summary>
        /// <returns>ResponseModel&lt;List&lt;OrderReturnModel&gt;&gt;.</returns>
        [HttpGet]
        [Route("/api/orders")]
        public async Task<ResponseModel<List<OrderReturnModel>>> GetAllOrder()
        {
            var result = await _orderService.getallOrder();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new ResponseBuilder<List<OrderReturnModel>>().Success()
                .Data(result)
                .Count(result.Count)
                .build();
        }
        #endregion
    }
}