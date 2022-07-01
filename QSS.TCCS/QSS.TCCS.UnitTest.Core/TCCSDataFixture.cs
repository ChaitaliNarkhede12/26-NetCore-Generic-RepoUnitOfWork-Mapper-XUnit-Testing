using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using QSS.TCCS.DataAccess.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSS.TCCS.UnitTest.Core
{
    public class TCCSDataFixture : IDisposable
    {
        public TCCSContext tccsContext { get; private set; }
        public DbContextOptions<TCCSContext> tccsContextOptions { get; private set; }
        private const string Database = "TCCSInMemoryDatabase";

        public TCCSDataFixture()
        {

            tccsContextOptions = new DbContextOptionsBuilder<TCCSContext>()
                .UseInMemoryDatabase(Database + DateTime.Now.ToFileTimeUtc())

             .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(true)
                .Options;


            tccsContext = new TCCSContext(tccsContextOptions);
            tccsContext.Database.EnsureDeleted();
            tccsContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            tccsContext.Database.EnsureDeleted();
            tccsContext.Dispose();
        }
    }
}
