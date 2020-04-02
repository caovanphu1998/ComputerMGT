using ComputerMGT.Application.ViewModels;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateUserModel model);

        Task<bool> UpdateUser(UpdateUserModel model);

        Task<UserDetailModel> Login(LoginModel model);
    }
}
