using System.Threading.Tasks;
using ComputerMGT.Data.Interfaces;

namespace ComputerMGT.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ComputerMGTContext context)
        {
            Context = context;
        }

        public ComputerMGTContext Context { get; }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}