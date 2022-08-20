using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Tests.Systems.Controllers
{
    public abstract class BaseTest : IDisposable
    {
        protected readonly Context _context;

        protected BaseTest()
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            var optionsBuilder = new DbContextOptionsBuilder<Context>()
                .UseMySql("server=localhost;database=test;user=root;password=root", serverVersion);

            var context = new Context(optionsBuilder.Options);
            context.Database.EnsureCreated();

            _context = context;
        }

        public void Dispose()
        {
            var tableNames = _context.Model.GetEntityTypes()
                .Select(t => t.GetTableName())
                .Distinct()
                .ToList();

            foreach (var tableName in tableNames)
            {
                _context.Database.ExecuteSqlRaw($"SET FOREIGN_KEY_CHECKS = 0; TRUNCATE TABLE `{tableName}`;");
            }
        }
    }
}