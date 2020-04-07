using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.ViewModels;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<TblCart> _cartRepository;
        private readonly IRepository<TblProduct> _productRepository;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;

        #region Contructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="cartRepository">The cart repository.</param>
        public CartService(IUnitOfWork unitOfWork
            , IRepository<TblCart> cartRepository
            , IRepository<TblProduct> productRepository
            , IProductService productService
            )
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
            _productService = productService;
            _productRepository = productRepository;
        }
        #endregion

        #region add product to Cart        
        /// <summary>
        /// Adds the product to cart.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Threading.Tasks.Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> AddProductToCart(AddProductToCartModel model)
        {
            var cart = new TblCart
            {
                CartId = Guid.NewGuid(),
                UserId = model.UserId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
            };
            _cartRepository.Insert(cart);
            await _unitOfWork.CommitAsync();
            return true;
        }
        #endregion

        public async Task<int> TotalMoney(Guid UserId)
        {
            var query = _cartRepository.GetManyAsNoTracking(x => x.UserId == UserId);
            if (query.ToList().Count == 0) return 0;
            int count = 0;
            foreach (TblCart element in query.ToList())
            {
                var product = await _productService.GetProductDetail(element.ProductId);
                count += element.Quantity * product.Price;
            }
            return count;
        }

        public async Task<bool> DeleteCart(Guid UserId)
        {
            var list = _cartRepository.GetManyAsNoTracking(x => x.UserId == UserId).ToList();
            foreach(TblCart a in list)
            {
                _cartRepository.Delete(a.CartId);
            }
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<List<CartModel>> getCart(Guid UserId)
        {
            var query = _cartRepository.GetManyAsNoTracking(x => x.UserId == UserId)
                .Join(_productRepository.GetAllAsNoTracking(), x => x.ProductId, y => y.ProductId, (x, y) => new CartModel
                {
                    CartId = x.CartId,
                    Price = y.Price,
                    ProductId = x.ProductId,
                    ProductName = y.Name,
                    Quantity = x.Quantity
                }).ToList();
            return query;
        }

        public async Task<bool> RemoveProduct(Guid CartId)
            {
            var query = _cartRepository.GetById(CartId);
            _cartRepository.Delete(query.CartId);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> changeQuantity(ChangeQuantityModel model)
        {
            var query = _cartRepository.GetById(model.CartId);
            query.Quantity = model.quantity;
            _cartRepository.Update(query);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
