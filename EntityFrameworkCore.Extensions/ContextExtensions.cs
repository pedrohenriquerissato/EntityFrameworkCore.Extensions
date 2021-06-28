using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.Extensions
{
    public static class ContextExtensions
    {
        /// <summary>
        /// Bulk Upsert extension for EF Core with Pomelo.MySQL connector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="collection"></param>
        public static void BulkUpsert<T>(this DbContext context, ICollection<T> collection)
        {
            if (!collection.Any())
                throw new ArgumentException("Collection can not be empty");

            var type = typeof(T);
            var entityType = context.Model.FindEntityType(type);
            var properties = entityType.GetProperties();

            var query = string.Empty;
            foreach (var item in collection)
            {
                query += string.Concat($"INSERT INTO `{context.Database.GetDbConnection().Database}`.`{entityType.GetTableName()}` ({string.Join(", ", properties.Select(x => x.GetColumnName()))}) VALUES ({string.Join(", ",  properties.Select(x => "'" + type.GetProperty(x.Name).GetValue(item, null) + "'"))}); ");
            }
                

            context.Database.ExecuteSqlRaw(query);
        }
    }
}
