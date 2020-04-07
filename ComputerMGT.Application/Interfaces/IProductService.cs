using ComputerMGT.Application.SearchModels;
using ComputerMGT.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<SearchResultProductModel>> SearchProduct(ProductSearchModel model);

        Task<DetailProductModel> GetProductDetail(Guid Id);

        Task<bool> AddProduct(UpLoadProductModel model);

        Task<bool> UpdateProduct(UpLoadProductModel model);

        Task<bool> DeleteProduct(Guid ProductId);
    }
}
