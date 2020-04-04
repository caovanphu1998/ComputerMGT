using ComputerMGT.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddProductToCart(AddProductToCartModel model);

        Task<int> TotalMoney(Guid UserId);

        Task<bool> DeleteCart(Guid UserId);
    }
}
