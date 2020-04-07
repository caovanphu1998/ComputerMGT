using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.SearchModels;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.RestApi.ApiResponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IHostingEnvironment _host;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public ProductController(IProductService productService, IHostingEnvironment webHostEnvironment)
        {
            _productService = productService;
            _host = webHostEnvironment;
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

        #region Add Product        
        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        [Route("/api/product")]
        public async Task AddProduct([FromBody] UpLoadProductModel model)
        {
            
            await _productService.AddProduct(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion

        #region Edit Product        
        /// <summary>
        /// Edit the product.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPut]
        [Route("/api/product")]
        public async Task EditProduct([FromBody] UpLoadProductModel model)
        {
            await _productService.UpdateProduct(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion

        #region Delete Product                
        /// <summary>
        /// Delete the product.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        [HttpDelete]
        [Route("/api/product")]
        public async Task DeleteProduct(Guid ProductId)
        {
            var result= await _productService.DeleteProduct(ProductId);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion

        #region upload file        
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>System.String.</returns>
        [HttpPost]
        [Route("/api/productfile")]
        public async Task<string> UploadFile(IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(_host.WebRootPath))
            {
                _host.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            if (file.Length > 0)
            {
                if (!Directory.Exists(_host.WebRootPath + "\\image\\"))
                {
                    Directory.CreateDirectory(_host.WebRootPath + "\\image\\");
                }
                using (FileStream fileStream = System.IO.File.Create(
                    _host.WebRootPath + "\\image\\" + file.FileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            var link = _host.WebRootPath + "\\image\\" + file.FileName;
            Response.StatusCode = (int)HttpStatusCode.OK;
            return link;
        }
        #endregion
    }
}