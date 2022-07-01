using Microsoft.EntityFrameworkCore;
using QSS.TCCS.DataAccess.DBContext;
using QSS.TCCS.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSS.TCCS.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(TCCSContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
