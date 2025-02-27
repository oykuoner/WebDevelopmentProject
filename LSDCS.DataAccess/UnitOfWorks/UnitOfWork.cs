using LSDCS.DataAccess.Context;
using LSDCS.DataAccess.Repositories.Abstractions;
using LSDCS.DataAccess.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LSDCSDbContext _dbContext;
        public UnitOfWork(LSDCSDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

      

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await  _dbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new Repository<T>(_dbContext);
        }
    }
}
