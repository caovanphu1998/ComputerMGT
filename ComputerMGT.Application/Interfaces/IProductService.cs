using ComputerMGT.Application.SearchModels;
using ComputerMGT.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<SearchResultProductModel>> SearchProduct(ProductSearchModel model);

        Task<DetailProductModel> GetProductDetail(Guid Id);

        Task<bool> AddProduct(DetailProductModel model);

        Task<bool> UpdateProduct(DetailProductModel model);

        Task<bool> DeleteProduct(Guid ProductId);


    }
}
