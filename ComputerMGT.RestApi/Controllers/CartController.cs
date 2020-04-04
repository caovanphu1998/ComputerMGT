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
    [Route("/api/cart")]
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="cartService">The cart service.</param>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        #endregion

        #region Add Product To Cart        
        /// <summary>
        /// Adds the product to cart.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPut]
        [Route("/api/cart/addproduct")]
        public async Task AddProductToCart([FromBody] AddProductToCartModel model)
        {
            var result = await _cartService.AddProductToCart(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion
    }
}