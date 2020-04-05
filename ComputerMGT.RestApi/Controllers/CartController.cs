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
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        #endregion

        #region getall                
        /// <summary>
        /// Gets the cart.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>ResponseModel&lt;List&lt;CartModel&gt;&gt;.</returns>
        [HttpGet]
        [Route("/api/carts")]
        public async Task<ResponseModel<List<CartModel>>> GetCart(Guid UserId)
        {
            var result = await _cartService.getCart(UserId);
            return new ResponseBuilder<List<CartModel>>().Success()
                .Data(result)
                .Count(result.Count)
                .build();
        }
        #endregion

        #region Adds the product
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

        #region Changes the quantity.
        /// <summary>
        /// Changes the quantity.
        /// </summary>
        /// <param name="CartId">The cart identifier.</param>
        /// <param name="quantity">The quantity.</param>
        [HttpPut]
        [Route("/api/cart/changeQuantity")]
        public async Task ChangeQuantity([FromBody] ChangeQuantityModel model)
        {
            await _cartService.changeQuantity(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion

        #region Removes the product
        /// <summary>
        /// Removes the product to cart.
        /// </summary>
        /// <param name="CartId">The cart identifier.</param>
        [HttpDelete]
        [Route("/api/cart/removeproduct")]
        public async Task RemoveProductToCart([FromBody] Guid CartId)
        {
            await _cartService.RemoveProduct(CartId);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion
    }
}