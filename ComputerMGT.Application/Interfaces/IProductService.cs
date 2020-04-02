using ComputerMGT.Application.SearchModels;
using ComputerMGT.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Search Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<SearchResultProductModel>> SearchProduct(ProductSearchModel model);

        Task<DetailProductModel> GetProductDetail(Guid Id);
    }
}
