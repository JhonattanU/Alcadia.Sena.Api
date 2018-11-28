using Alcadia.Sena.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcadia.Sena.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IConnectionFactory _connectionFactory;

        public IDbConnection Connection
        {
            get
            {
                return _connectionFactory.CreateConnection();
            }
        }

        public Repository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Gets an element by id string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(string id)
        {
            using (var connection = Connection)
            {
                return connection.Get<T>(id);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            using (var connection = Connection)
            {
                return connection.Get<T>(id);
            }
        }
        /// <summary>
        /// Gets an element by id string async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(string id)
        {
            using (var connection = Connection)
            {
                return await connection.GetAsync<T>(id);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(int id)
        {
            using (var connection = Connection)
            {
                return await connection.GetAsync<T>(id);
            }
        }
        /// <summary>
        /// Gets an element by its primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Element if found</returns>
        public virtual T Get(Guid id)
        {
            using (var connection = Connection)
            {
                return connection.Get<T>(id);
            }
        }
        /// <summary>
        /// Gets and element by its primary key asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(Guid id)
        {
            using (var connection = Connection)
            {
                return await connection.GetAsync<T>(id);
            }
        }
        /// <summary>
        /// Gets all elements from the database
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            using (var connection = Connection)
            {
                return connection.GetAll<T>();
            }
        }
        /// <summary>
        /// Gets all elements from the database asynchronously
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = Connection)
            {
                return await connection.GetAllAsync<T>();
            }
        }
        /// <summary>
        /// Inserts a single elment to the database
        /// </summary>
        /// <param name="entity">Element to be inserted</param>
        /// <returns></returns>
        public virtual long Insert(T entity)
        {
            using (var connection = Connection)
            {
                return connection.Insert(entity);
            }
        }
        /// <summary>
        /// Inserts a single elment to the database asynchronously
        /// </summary>
        /// <param name="entity">Element to be inserted</param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(T entity)
        {
            using (var connection = Connection)
            {
                return await connection.InsertAsync(entity);
            }
        }
        /// <summary>
        /// Inserts a list of elements to the database
        /// </summary>
        /// <param name="list">Elements to be inserted</param>
        /// <returns>Number of elements inserted</returns>
        public virtual long Insert(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                long result = 0;
                foreach (var entity in list)
                {
                    result += connection.Insert(entity);
                }
                return result;
            }
        }
        /// <summary>
        /// Inserts a list of elements to the database asynchronously
        /// </summary>
        /// <param name="list">Elements to be inserted</param>
        /// <returns>Number of elements inserted</returns>
        public virtual async Task<int> InsertAsync(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                int result = 0;
                foreach (var entity in list)
                {
                    result += await connection.InsertAsync(entity);
                }
                return result;
            }
        }
        /// <summary>
        /// Updates an element on the database
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>True if update was successful</returns>
        public virtual bool Update(T entity)
        {
            using (var connection = Connection)
            {
                if (typeof(Audit).IsAssignableFrom(typeof(T)))
                {
                    (entity as Audit).SetDateModified();
                }
                return connection.Update(entity);
            }
        }
        /// <summary>
        /// Updates an element on the database asynchronously
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>True if update was successful</returns>
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            using (var connection = Connection)
            {
                if (typeof(Audit).IsAssignableFrom(typeof(T)))
                {
                    (entity as Audit).SetDateModified();
                }
                return await connection.UpdateAsync(entity);
            }
        }
        /// <summary>
        /// Updates a given list
        /// </summary>
        /// <param name="entity">List of entity to be updated</param>
        /// <returns>True if update was successful</returns>
        public virtual bool Update(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                if (typeof(Audit).IsAssignableFrom(typeof(T)))
                {
                    (list as IEnumerable<Audit>).SetDateModified();
                }

                foreach (var entity in list)
                {
                    connection.Update(entity);
                }
                return list.Any();
            }
        }
        /// <summary>
        /// Updates a given list asynchronously
        /// </summary>
        /// <param name="entity">List of entity to be updated</param>
        /// <returns>True if update was successful</returns>
        public virtual async Task<bool> UpdateAsync(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                if (typeof(Audit).IsAssignableFrom(typeof(T)))
                {
                    (list as IEnumerable<Audit>).SetDateModified();
                }

                foreach (var entity in list)
                {
                    await connection.UpdateAsync(entity);
                }
                return list.Any();
            }
        }
        /// <summary>
        /// Deletes a given element from the database
        /// </summary>
        /// <param name="entity">Element to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        private bool Delete(T entity)
        {
            using (var connection = Connection)
            {
                return connection.Delete(entity);
            }
        }

        /// <summary>
        /// Deletes a given element from the database
        /// </summary>
        /// <param name="entity">Element to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        public virtual bool DeleteOrUpdate(T entity)
        {
            if (entity is Audit)
            {
                (entity as Audit).SetIsDelete();
                return Update(entity);
            }            
            else
            {
                return Delete(entity);
            }
        }

        /// <summary>
        /// Deletes a given element from the database asynchronously
        /// </summary>
        /// <param name="entity">Element to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        private async Task<bool> DeleteAsync(T entity)
        {
            using (var connection = Connection)
            {
                return await connection.DeleteAsync(entity);
            }
        }

        /// <summary>
        /// Deletes a given element from the database asynchronously
        /// </summary>
        /// <param name="entity">Element to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        public virtual async Task<bool> DeleteOrUpdateAsync(T entity)
        {
            if (entity is Audit)
            {
                (entity as Audit).SetIsDelete();
                return await UpdateAsync(entity);
            }            
            else
            {
                return await DeleteAsync(entity);
            }
        }

        /// <summary>
        /// Deletes a list of elements from the database
        /// </summary>
        /// <param name="entity">List of elements to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        private bool Delete(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                return connection.Delete(list);
            }
        }

        /// <summary>
        /// Deletes a list of elements from the database
        /// </summary>
        /// <param name="entity">List of elements to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        public virtual bool DeleteOrUpdate(IEnumerable<T> list)
        {
            if (typeof(Audit).IsAssignableFrom(typeof(T)))
            {
                (list as IEnumerable<Audit>).SetIsDelete();
                return Update(list);
            }           
            else
            {
                return Delete(list);
            }
        }

        /// <summary>
        /// Deletes a list of elements from the database asynchronously
        /// </summary>
        /// <param name="entity">List of elements to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        private async Task<bool> DeleteAsync(IEnumerable<T> list)
        {
            using (var connection = Connection)
            {
                return await connection.DeleteAsync(list);
            }
        }

        // <summary>
        /// Deletes a list of elements from the database asynchronously
        /// </summary>
        /// <param name="entity">List of elements to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        public virtual async Task<bool> DeleteOrUpdateAsync(IEnumerable<T> list)
        {
            if (typeof(Audit).IsAssignableFrom(typeof(T)))
            {
                (list as IEnumerable<Audit>).SetIsDelete();
                return await UpdateAsync(list);
            }            
            else
            {
                return await DeleteAsync(list);
            }
        }

        /// <summary>
        /// This method will execute SQL and return a dynamic list
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query(string sql, object param = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql: sql, param: param);
            }
        }

        /// <summary>
        /// This method will execute SQL and return a dynamic list asynchronously
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> QueryAsync(string sql, object param = null)
        {
            using (var connection = Connection)
            {
                return await connection.QueryAsync<T>(sql: sql, param: param);
            }
        }

        /// <summary>
        /// This method will query the database given an object.
        /// Will dynamicly create the SQL string and its parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> QueryDynamic<TFilter>(TFilter filter) where TFilter : class
        {
            using (var connection = Connection)
            {
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlString(parameters.ParameterNames);
                sql = filter.AddOrderByToSqlString(sql);
                sql = filter.AddPagingToSqlString(sql);
                return connection.Query<T>(sql: sql.ToString(), param: parameters);
            }
        }

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> QueryDynamicAsync<TFilter>(TFilter filter) where TFilter : class
        {
            using (var connection = Connection)
            {
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlString(parameters.ParameterNames);
                sql = filter.AddOrderByToSqlString(sql);
                sql = filter.AddPagingToSqlString(sql);
                return await connection.QueryAsync<T>(sql: sql.ToString(), param: parameters);
            }
        }

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters,
        /// and return a list of elements given pagination parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual (Pagination pagination, IEnumerable<T> elements) QueryDynamicPaged<TFilter>(TFilter filter) where TFilter : class
        {
            var totalElements = QueryDynamicCount(filter);
            var pagination = filter.GetPagination(totalElements);
            var elements = QueryDynamic(filter);
            return (pagination, elements);
        }

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters,
        /// and return a list of elements given pagination parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<(Pagination pagination, IEnumerable<T> elements)> QueryDynamicPagedAsync<TFilter>(TFilter filter) where TFilter : class
        {
            var totalElements = await QueryDynamicCountAsync(filter);
            var pagination = filter.GetPagination(totalElements);
            var elements = await QueryDynamicAsync(filter);
            return (pagination, elements);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual int QueryDynamicCount<TFilter>(TFilter filter) where TFilter : class
        {
            using (var connection = Connection)
            {
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlCountString(parameters.ParameterNames);
                return connection.ExecuteScalar<int>(sql: sql.ToString(), param: parameters);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<int> QueryDynamicCountAsync<TFilter>(TFilter filter) where TFilter : class
        {
            using (var connection = Connection)
            {
                var parameters = filter.GetParameters();
                var sql = typeof(T).GetSqlCountString(parameters.ParameterNames);
                return await connection.ExecuteScalarAsync<int>(sql: sql.ToString(), param: parameters);
            }
        }
    }
}
