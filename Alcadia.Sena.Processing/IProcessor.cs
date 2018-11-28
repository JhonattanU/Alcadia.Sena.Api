using Alcadia.Sena.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alcadia.Sena.Processing
{
    public interface IProcessor<TEntity> where TEntity : class
    {
        /// <summary>
        /// get element by id string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get<T>(string id) where T : class;
        /// <summary>
        /// Get element by id string async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string id) where T : class;
        /// <summary>
        /// Get element by id int async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(int id) where T : class;
        /// <summary>
        /// Gets an element by its primary key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get<T>(Guid id) where T : class;
        /// <summary>
        /// Gets an element by its primary key asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(Guid id) where T : class;
        /// <summary>
        /// Gets all elements from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>() where T : class;
        /// <summary>
        /// Gets all elements from the database asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        /// <summary>
        /// Inserts a single elment to the database
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        TOutput Insert<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class;
        /// <summary>
        /// Inserts a single elment to the database asynchronously
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<TOutput> InsertAsync<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class;
        /// <summary>
        /// Inserts a list of elements to the database
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        IEnumerable<TOutput> Insert<TInput, TOutput>(IEnumerable<TInput> list) where TInput : class where TOutput : class;
        /// <summary>
        /// Inserts a list of elements to the database asynchronously
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<IEnumerable<TOutput>> InsertAsync<TInput, TOutput>(IEnumerable<TInput> list) where TInput : class where TOutput : class;
        /// <summary>
        /// Updates an element on the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Update<T>(Guid id, T obj) where T : class;
        /// <summary>
        /// Updates an element on the database asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(Guid id, T obj) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(int id, T obj) where T : class;
        /// <summary>
        /// Updates an element on the database asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(string id, T obj) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        bool Update<T>(IEnumerable<T> list) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync<T>(IEnumerable<T> list) where T : class;
        /// <summary>
        /// Deletes an element from the database by its primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Delete<TInput>(TInput obj) where TInput : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<TInput>(TInput obj) where TInput : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Delete<TInput>(IEnumerable<TInput> obj) where TInput : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<TInput>(IEnumerable<TInput> obj) where TInput : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool DeleteByFilter<TInput>(TInput filter) where TInput : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> DeleteByFilterAsync<TInput>(TInput filter) where TInput : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool DeleteByFilter(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteByFilter(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> DeleteByFilterAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByFilterAsync(Guid id);

        /// <summary>
        /// This method will query the database given an object.
        /// Will dynamicly create the SQL string and its parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IEnumerable<TOutput> GetDynamic<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class;

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<IEnumerable<TOutput>> GetDynamicAsync<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class;

        /// <summary>
        /// This method will query the database synchronously given an object.
        /// Will dynamicly create the SQL string and its parameters,
        /// and return a list of elements given pagination parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        (Pagination pagination, IEnumerable<TOutput> elements) GetDynamicPaged<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class;

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters,
        /// and return a list of elements given pagination parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<(Pagination pagination, IEnumerable<TOutput> elements)> GetDynamicPagedAsync<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class;
    }
}
