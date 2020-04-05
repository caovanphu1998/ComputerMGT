using ComputerMGT.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddProductToCart(AddProductToCartModel model);

        Task<int> TotalMoney(Guid UserId);

        Task<bool> DeleteCart(Guid UserId);

        Task<bool> RemoveProduct(Guid CartId);

        Task<bool> changeQuantity(ChangeQuantityModel model);

        Task<List<CartModel>> getCart(Guid UserId);
    }
}
