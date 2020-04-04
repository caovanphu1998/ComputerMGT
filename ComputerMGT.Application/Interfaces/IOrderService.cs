using System;
using System.Threading.Tasks;

namespace ComputerMGT.Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOder(Guid UserId);
    }
}
