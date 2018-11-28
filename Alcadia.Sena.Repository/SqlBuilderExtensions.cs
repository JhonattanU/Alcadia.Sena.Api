using Alcadia.Sena.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alcadia.Sena.Repository
{
    public static class SqlBuilderExtensions
    {
        /// <summary>
        /// Get Sql String For Table Dinamic
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameterNames"></param>
        /// <returns></returns>
        public static StringBuilder GetSqlString(this Type type, IEnumerable<string> parameterNames)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"select * from {type.GetTableName()} ");
            builder.AppendLine("where 1 = 1 ");
            foreach (var name in parameterNames)
            {
                builder.AppendLine($"and [{name}] = @{name} ");
            }
            return builder;

        }

        /// <summary>
        /// Gets the count sql string for a given table
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameterNames"></param>
        /// <returns></returns>
        public static string GetSqlCountString(this Type type, IEnumerable<string> parameterNames)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"select count(*) from {type.GetTableName()} ");
            builder.AppendLine("where 1 = 1 ");
            foreach (var name in parameterNames)
            {
                builder.AppendLine($"and [{name}] = @{name} ");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Get Table Name
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns
        private static string GetTableName(this Type type)
        {
            var tableAttr = type.GetTypeInfo().GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;

            if (tableAttr != null) return tableAttr.Name;

            return type.Name + "s";
        }

        /// <summary>
        /// Get Parameters for object Dinamic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DynamicParameters GetParameters<T>(this T obj) where T : class
        {
            var properties = obj.GetType().GetRuntimeProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                var name = property.Name;
                var propertyType = property.GetType();

                if (value is null ||
                    value == null ||
                    (propertyType.IsInstanceOfType(typeof(string)) && string.IsNullOrWhiteSpace(value.ToString())) ||
                    /** Cambiar esta logica, validar por tipo IFilter*/
                    name.Equals(nameof(IFilter.OrderBy)) ||
                    name.Equals(nameof(IFilter.PageNumber)) ||
                    name.Equals(nameof(IFilter.PageSize)) ||
                    value.IsDefaultValue()) continue;

                parameters.Add(name, value);
            }
            return parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsDefaultValue(this object value)
        {
            switch (value)
            {
                case int i:
                    return default(int) == i;
                case DateTime i:
                    return default(DateTime) == i;
                case decimal i:
                    return default(decimal) == i;
                case double i:
                    return default(double) == i;
                case float i:
                    return default(float) == i;
                case long i:
                    return default(long) == i;
                case short i:
                    return default(short) == i;
                case uint i:
                    return default(uint) == i;
                case ulong i:
                    return default(ulong) == i;
                case ushort i:
                    return default(ushort) == i;
                case sbyte i:
                    return default(sbyte) == i;
                case Guid i:
                    return default(Guid) == i;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Add the order by parameters to and existing sql query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="sql"></param>
        public static StringBuilder AddOrderByToSqlString<T>(this T obj, StringBuilder sql) where T : class
        {
            //Return if no orderby present
            if (!obj.ValidateOrderBy()) return sql;

            //Get the order by list
            var order = obj as IFilter;
            var notNullElements = order.OrderBy.Where(c => !string.IsNullOrWhiteSpace(c.ColumnName.Trim())).Select(d => d.ToString());

            //Append new line with oder
            sql.AppendLine($"order by {string.Join(", ", notNullElements)}");

            return sql;
        }

        private static bool ValidateOrderBy<T>(this T obj) where T : class
        {
            //if not assignable from base class, return
            if (!typeof(IFilter).IsAssignableFrom(typeof(T))) return false;

            //if null, return
            var order = obj as IFilter;
            if (order == null || order.OrderBy == null) return false;

            //if does not contain any order by, return
            if (!order.OrderBy.Any()) return false;

            //check for null elements
            return order.OrderBy.Any(c => !string.IsNullOrWhiteSpace(c.ColumnName.Trim()));
        }

        public static StringBuilder AddPagingToSqlString<T>(this T obj, StringBuilder sql) where T : class
        {
            //orderby is mandatory for paging
            if (!obj.ValidateOrderBy()) return sql;

            var order = obj as IFilter;

            //return because paging is not possible if page size is less than or equals to 0
            if (order.PageSize <= 0) return sql;
            //return because at this stage the page number cannot be 0
            if (order.PageNumber <= 0) return sql;

            //Calculate offset
            int offset = GetOffset(order.PageSize, order.PageNumber);

            //return because and offset less than 0 is not possible
            if (offset < 0) return sql;

            sql.AppendLine($"offset {offset} rows");
            sql.AppendLine($"fetch next {order.PageSize} rows only");

            return sql;
        }

        private static int GetOffset(int pageSize, int pageNumber)
        {
            return (pageNumber - 1) * pageSize;
        }

        public static Pagination GetPagination<T>(this T filter, int totalElements) where T : class
        {
            var iFilter = filter as IFilter;
            if (iFilter == null) return new Pagination();

            return new Pagination
            {
                TotalElements = totalElements,
                PageNumber = iFilter.PageNumber,
                PageSize = iFilter.PageSize,
                TotalPages = GetTotalPages(totalElements, iFilter.PageSize)
            };
        }

        private static int GetTotalPages(int totalElements, int pageSize)
        {
            if (pageSize == 0) return 0;
            double totalPages = totalElements / (double)pageSize;
            return (int)Math.Ceiling(totalPages);
        }
    }
}
