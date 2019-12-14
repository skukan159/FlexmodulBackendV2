using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Domain;

namespace FlexmodulBackendV2.Data
{
    public static class DbSeeder<T> where T : EntityBase
    {
        public static void SeedDatabaseWithData(ApplicationDbContext context, IEnumerable<T> data)
        {
            if (context.Set<T>().Any()) return;

            foreach (var entity in data)
            {
                context.Set<T>().Add(entity);
            }
            context.SaveChanges();
        }
    }
}
