using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsDsii.Repositories.IUnitOfWork
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
    }
}