using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSS.TCCS.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
    }
}
