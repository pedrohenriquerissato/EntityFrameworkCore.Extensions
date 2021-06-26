using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.Extensions
{
    public static class ContextExtensions
    {
        public static void BulkMerge<T>(this DbContext context, ICollection<T> collection)
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var primaryKeys = entityType.FindPrimaryKey();

            var query = string.Empty;
            for (int i = 0; i < collection.Count; i++)
            {
                query = query + string.Concat($"INSERT INTO `{entityType.DisplayName()}` ({})");
            }

            context.Database.ExecuteSqlRaw(query);
        }
    }
}
