using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace buddy.EntityFrameworkCore
{
    public static class buddyDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<buddyDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<buddyDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
