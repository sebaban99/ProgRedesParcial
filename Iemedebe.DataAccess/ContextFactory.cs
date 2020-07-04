using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Iemedebe.DataAccess
{
    public enum ContextType
    {
        MEMORY, SQL
    }

    public class ContextFactory : IDesignTimeDbContextFactory<IemedebeContext>
    {
        public IemedebeContext CreateDbContext(string[] args)
        {
            return GetNewContext();
        }

        public static IemedebeContext GetNewContext(ContextType type = ContextType.SQL)
        {
            var builder = new DbContextOptionsBuilder<IemedebeContext>();
            DbContextOptions options = null;
            if (type == ContextType.MEMORY)
            {
                options = GetMemoryConfig(builder);
            }
            else
            {
                options = GetSqlConfig(builder);
            }
            return new IemedebeContext(options);
        }

        private static DbContextOptions GetMemoryConfig(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase("Iemedebe");
            return builder.Options;
        }

        private static DbContextOptions GetSqlConfig(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.\SQLEXPRESS01;Database=Iemedebe;Trusted_Connection=True;");
            return builder.Options;
        }
    }
}