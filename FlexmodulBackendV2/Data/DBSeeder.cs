using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Data
{
    public static class DbSeeder<T> where T : EntityBase
    {
        public static async Task SeedDatabaseWithData(ApplicationDbContext context, IEnumerable<T> data)
        {
            if (context.Set<T>().Any()) return;

            foreach (var entity in data)
            {
                context.Set<T>().Add(entity);
            }
            await context.SaveChangesAsync();
        }
    }
}
