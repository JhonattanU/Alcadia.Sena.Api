using Alcadia.Sena.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alcadia.Sena.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get Elements by id string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);
        /// <summary>
        /// Gets an element by its primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Element if found</returns>
        T Get(Guid id);
        /// <summary>
        /// Gets and element by its primary key asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(Guid id);
        /// <summary>
        /// Gets all elements from the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Gets all elements from the database asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Inserts a single elment to the database
        /// </summary>
        /// <param name="entity">Element to be inserted</param>
        /// <returns></returns>
        long Insert(T entity);
        /// <summary>
        /// Inserts a single elment to the database asynchronously
        /// </summary>
        /// <param name="entity">Element to be inserted</param>
        /// <returns></returns>
        Task<int> InsertAsync(T entity);
        /// <summary>
        /// Inserts a list of elements to the database
        /// </summary>
        /// <param name="list">Elements to be inserted</param>
        /// <returns>Number of elements inserted</returns>
        long Insert(IEnumerable<T> list);
        /// <summary>
        /// Inserts a list of elements to the database asynchronously
        /// </summary>
        /// <param name="list">Elements to be inserted</param>
        /// <returns>Number of elements inserted</returns>
        Task<int> InsertAsync(IEnumerable<T> list);
        /// <summary>
        /// Updates an element on the database
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>True if update was successful</returns>
        bool Update(T entity);
        /// <summary>
        /// Updates an element on the database asynchronously
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>True if update was successful</returns>
        Task<bool> UpdateAsync(T entity);
        /// <summary>
        /// Updates a given list
        /// </summary>
        /// <param name="entity">List of entity to be updated</param>
        /// <returns>True if update was successful</returns>
        bool Update(IEnumerable<T> list);
        /// <summary>
        /// Updates a given list asynchronously
        /// </summary>
        /// <param name="entity">List of entity to be updated</param>
        /// <returns>True if update was successful</returns>
        Task<bool> UpdateAsync(IEnumerable<T> list);
        /// <summary>
        /// Deletes a given element from the database
        /// </summary>
        /// <param name="entity">Element to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        bool DeleteOrUpdate(T entity);
        /// <summary>
        /// Deletes a given element from the database asynchronously
        /// </summary>
        /// <param name="entity">Element to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        Task<bool> DeleteOrUpdateAsync(T entity);
        /// <summary>
        /// Deletes a list of elements from the database
        /// </summary>
        /// <param name="entity">List of elements to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        bool DeleteOrUpdate(IEnumerable<T> list);
        /// <summary>
        /// Deletes a list of elements from the database asynchronously
        /// </summary>
        /// <param name="entity">List of elements to be deleted</param>
        /// <returns>True if the delete was successful</returns>
        Task<bool> DeleteOrUpdateAsync(IEnumerable<T> list);

        /// <summary>
        /// This method will execute SQL and return a dynamic list
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, object param = null);

        /// <summary>
        /// This method will execute SQL and return a dynamic list asynchronously
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync(string sql, object param = null);

        /// <summary>
        /// This method will query the database given an object.
        /// Will dynamicly create the SQL string and its parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IEnumerable<T> QueryDynamic<TFilter>(TFilter filter) where TFilter : class;

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryDynamicAsync<TFilter>(TFilter filter) where TFilter : class;

        /// <summary>
        /// This method will query the database synchronously given an object.
        /// Will dynamicly create the SQL string and its parameters,
        /// and return a list of elements given pagination parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        (Pagination pagination, IEnumerable<T> elements) QueryDynamicPaged<TFilter>(TFilter filter) where TFilter : class;

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters,
        /// and return a list of elements given pagination parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<(Pagination pagination, IEnumerable<T> elements)> QueryDynamicPagedAsync<TFilter>(TFilter filter) where TFilter : class;
    }
}
