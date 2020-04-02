﻿using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.SearchModels;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Domain.Models;
using ComputerMGT.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<TblProduct> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region Contructor
        public ProductService(IUnitOfWork unitOfWork
            , IRepository<TblProduct> productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        #endregion

        #region Search Product
        public async Task<List<SearchResultProductModel>> SearchProduct(ProductSearchModel model)
        {
            const string defaultSort = "ProductName ASC";
            var sortType = model.IsSortDesc ? "DESC" : "ASC";
            var sortField = ValidateUtils.IsNullOrEmpty(model.SortField)
                ? defaultSort
                : $"{model.SortField} {sortType}";
            var query = _productRepository.GetManyAsNoTracking(x =>
             ValidateUtils.IsNullOrEmpty(model.ProductName) || x.Name.ToUpper().Contains(model.ProductName))
                .Select(x => new SearchResultProductModel
                {
                    ProductId = x.ProductId,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    ImageLink = x.ImageLink,
                    Name = x.Name,
                    Price = x.Price
                });
            var result = query.Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize);
            return result.ToList();
        }
        #endregion

        #region Detail
        /// <summary>
        /// Get Product Detail
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DetailProductModel> GetProductDetail(Guid Id)
        {
            var query = _productRepository.GetById(Id);
            if (query == null) throw new Exception("The product not found");
            return new DetailProductModel
            {
                ProductId = query.ProductId,
                CategoryId = query.CategoryId,
                Description = query.Description,
                ImageLink = query.ImageLink,
                Name = query.Name,
                Price = query.Price
            };
        }
        #endregion
    }
}
