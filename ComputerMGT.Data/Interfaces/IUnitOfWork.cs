using System;
using System.Threading.Tasks;

namespace ComputerMGT.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ComputerMGTContext Context { get; }
        void Commit();
        Task CommitAsync();
    }
}