using ComputerMGT.Application.ViewModels;
using ComputerMGT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateUserModel model);
    }
}
