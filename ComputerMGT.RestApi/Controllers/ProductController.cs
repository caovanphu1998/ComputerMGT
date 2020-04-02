using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.SearchModels;
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
    [Route("/api/products")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region SearchProduct        
        /// <summary>
        /// Searches the product.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResponseModel&lt;List&lt;SearchResultProductModel&gt;&gt;.</returns>
        [HttpGet]
        [Route("/api/products")]
        public async Task<ResponseModel<List<SearchResultProductModel>>> SearchProduct(
            [FromQuery] ProductSearchModel model)
        {
            var result = await _productService.SearchProduct(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new ResponseBuilder<List<SearchResultProductModel>>().Success()
                .Data(result)
                .Count(result.Count)
                .build();
        }
        #endregion

        #region User detail        
        /// <summary>
        /// Gets the product detail by identifier.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns>ResponseModel&lt;DetailProductModel&gt;.</returns>
        [HttpGet]
        [Route("/api/product")]
        public async Task<ResponseModel<DetailProductModel>> GetProductDetailById(
            [FromQuery] Guid ProductId)
        {
            var result = await _productService.GetProductDetail(ProductId);
            return new ResponseBuilder<DetailProductModel>().Success()
                .Data(result)
                .Count(1)
                .build();
        }
        #endregion
    }
}