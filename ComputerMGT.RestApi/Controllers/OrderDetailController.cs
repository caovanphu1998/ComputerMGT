using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.RestApi.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ComputerMGT.RestApi.Controllers
{
    [ApiController]
    [Route("/api/orderdetail")]
    [Produces("application/json")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        #region Constructor                
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailController"/> class.
        /// </summary>
        /// <param name="orderDetailService">The order detail service.</param>
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        #endregion

        #region get order Detail                  
        /// <summary>
        /// Gets all order.
        /// </summary>
        /// <param name="OrderId">The order identifier.</param>
        /// <returns>ResponseModel&lt;List&lt;OrderDetailReturnModel&gt;&gt;.</returns>
        [HttpGet]
        [Route("/api/orderdetails")]
        public async Task<ResponseModel<List<OrderDetailReturnModel>>> GetAllOrder(Guid OrderId)
        {
            var result = await _orderDetailService.GetListOrderDetail(OrderId);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new ResponseBuilder<List<OrderDetailReturnModel>>().Success()
                .Data(result)
                .Count(result.Count)
                .build();
        }
        #endregion

    }
}