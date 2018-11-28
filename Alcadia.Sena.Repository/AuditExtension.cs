using Alcadia.Sena.Models;
using System;
using System.Collections.Generic;

namespace Alcadia.Sena.Repository
{
    public static class AuditExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static T SetIsDelete<T>(this T entity) where T : Audit
        {
            entity.IsDeleted = true;
            return entity;
        }
        /// <summary>
        /// Set IsDeleted Audit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static IEnumerable<T> SetIsDelete<T>(this IEnumerable<T> list) where T : Audit
        {
            foreach (var entity in list) { entity.SetIsDelete(); }
            return list;
        }

        /// <summary>
        /// Set DateModified Audit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static T SetDateModified<T>(this T entity) where T : Audit
        {
            entity.DateModified = DateTime.UtcNow;
            return entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static IEnumerable<T> SetDateModified<T>(this IEnumerable<T> list) where T : Audit
        {
            foreach (var entity in list) { entity.SetDateModified(); }
            return list;
        }
    }
}
