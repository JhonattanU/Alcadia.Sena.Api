using Alcadia.Sena.Models;
using Alcadia.Sena.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcadia.Sena.Processing
{
    public class Processor<TEntity> : IProcessor<TEntity> where TEntity : class
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;

        public Processor(IMapper mapper, IRepository<TEntity> repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Get by string Id 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(string id) where T : class
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            var result = _repository.Get(id);
            return _mapper.Map<T>(result);
        }
        /// <summary>
        /// Get async by string Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string id) where T : class
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            var result = await _repository.GetAsync(id);
            return _mapper.Map<T>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(int id) where T : class
        {
            if (id < 0) throw new ArgumentNullException(nameof(id));

            var result = await _repository.GetAsync(id);
            return _mapper.Map<T>(result);
        }

        /// <summary>
        /// Get by Guid Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(Guid id) where T : class
        {
            if (id == null || id == default(Guid)) throw new ArgumentNullException(nameof(id));

            var result = _repository.Get(id);
            return _mapper.Map<T>(result);
        }

        /// <summary>
        /// Gets an element by its primary key asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(Guid id) where T : class
        {
            if (id == null || id == default(Guid)) throw new ArgumentNullException(nameof(id));

            var result = await _repository.GetAsync(id);
            return _mapper.Map<T>(result);
        }
        /// <summary>
        /// Gets all elements from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>() where T : class
        {
            var result = _repository.GetAll();
            return _mapper.Map<IEnumerable<T>>(result);
        }
        /// <summary>
        /// Gets all elements from the database asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<T>>(result);
        }
        /// <summary>
        /// Inserts a single elment to the database
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TOutput Insert<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var entity = _mapper.Map<TEntity>(obj);
            _repository.Insert(entity);
            return _mapper.Map<TOutput>(entity);
        }
        /// <summary>
        /// Inserts a single elment to the database asynchronously
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<TOutput> InsertAsync<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var entity = _mapper.Map<TEntity>(obj);
            await _repository.InsertAsync(entity);
            return _mapper.Map<TOutput>(entity);
        }
        /// <summary>
        /// Inserts a list of elements to the database
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public IEnumerable<TOutput> Insert<TInput, TOutput>(IEnumerable<TInput> list) where TInput : class where TOutput : class
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            var entities = _mapper.Map<IEnumerable<TEntity>>(list);
            _repository.Insert(entities);
            return _mapper.Map<IEnumerable<TOutput>>(entities);
        }

        /// <summary>
        /// Inserts a list of elements to the database asynchronously
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TOutput>> InsertAsync<TInput, TOutput>(IEnumerable<TInput> list) where TInput : class where TOutput : class
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            var entity = _mapper.Map<IEnumerable<TEntity>>(list);
            await _repository.InsertAsync(entity);
            return _mapper.Map<IEnumerable<TOutput>>(entity);
        }
        /// <summary>
        /// Updates an element on the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update<T>(Guid id, T obj) where T : class
        {
            if (id == null || id == default(Guid)) throw new ArgumentNullException(nameof(id));

            var entity = _repository.Get(id);
            if (entity == null) return false;
            _mapper.Map(obj, entity);
            return _repository.Update(entity);
        }

        /// <summary>
        /// Updates an element on the database asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<T>(Guid id, T obj) where T : class
        {
            if (id == null || id == default(Guid)) throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetAsync(id);
            if (entity == null) return false;
            _mapper.Map(obj, entity);
            return await _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<T>(int id, T obj) where T : class
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetAsync(id);
            if (entity == null) return false;
            _mapper.Map(obj, entity);
            return await _repository.UpdateAsync(entity);
        }
        /// <summary>
        /// Updates an element on the database asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<T>(string id, T obj) where T : class
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetAsync(id);
            if (entity == null) return false;
            _mapper.Map(obj, entity);
            return await _repository.UpdateAsync(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Update<T>(IEnumerable<T> list) where T : class
        {
            if (list == null || list.Count() == 0) throw new ArgumentNullException(nameof(list));
            var entities = _mapper.Map<IEnumerable<TEntity>>(list);
            return _repository.Update(entities);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync<T>(IEnumerable<T> list) where T : class
        {
            if (list == null || list.Count() == 0) throw new ArgumentNullException(nameof(list));
            var entities = _mapper.Map<IEnumerable<TEntity>>(list);
            return await _repository.UpdateAsync(entities);
        }
        /// <summary>
        /// Deletes an element from the database by its primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            if (id == null || id == default(Guid)) throw new ArgumentNullException(nameof(id));

            var entity = _repository.Get(id);
            if (entity == null) return false;
            return _repository.DeleteOrUpdate(entity);
        }
        /// <summary>
        /// Deletes an element from the database by its primary key asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == null || id == default(Guid)) throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetAsync(id);
            if (entity == null) return false;
            return await _repository.DeleteOrUpdateAsync(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete<TInput>(TInput obj) where TInput : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var result = _mapper.Map<TEntity>(obj);
            return _repository.DeleteOrUpdate(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<TInput>(TInput obj) where TInput : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var result = _mapper.Map<TEntity>(obj);
            return await _repository.DeleteOrUpdateAsync(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Delete<T>(IEnumerable<T> list) where T : class
        {
            if (list == null || list.Count() == 0) throw new ArgumentNullException(nameof(list));
            var result = _mapper.Map<IEnumerable<TEntity>>(list);
            return _repository.DeleteOrUpdate(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<T>(IEnumerable<T> list) where T : class
        {
            if (list == null || list.Count() == 0) throw new ArgumentNullException(nameof(list));
            var result = _mapper.Map<IEnumerable<TEntity>>(list);
            return await _repository.DeleteOrUpdateAsync(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            var entity = _repository.Get(id);
            if (entity == null) return false;
            return _repository.DeleteOrUpdate(entity);
        }
        /// <summary>
        /// delete data by filters
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteByFilter<TInput>(TInput filter) where TInput : class
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            var entity = _repository.QueryDynamic(filter);
            if (entity == null) return false;
            return _repository.DeleteOrUpdate(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByFilterAsync<TInput>(TInput filter) where TInput : class
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            var entity = await _repository.QueryDynamicAsync(filter);
            if (entity == null) return false;
            return await _repository.DeleteOrUpdateAsync(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteByFilter(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            var result = _mapper.Map<TEntity>(id);
            var entity = _repository.QueryDynamic(result);
            if (entity == null) return false;
            return _repository.DeleteOrUpdate(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteByFilter(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var result = _mapper.Map<TEntity>(id);
            var entity = _repository.QueryDynamic(result);
            if (entity == null) return false;
            return _repository.DeleteOrUpdate(entity);
        }
        public async Task<bool> DeleteByFilterAsync(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var result = _mapper.Map<TEntity>(id);
            var entity = await _repository.QueryDynamicAsync(result);
            if (entity == null) return false;
            return await _repository.DeleteOrUpdateAsync(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByFilterAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            var result = _mapper.Map<TEntity>(id);
            var entity = await _repository.QueryDynamicAsync(result);
            if (entity == null) return false;
            return await _repository.DeleteOrUpdateAsync(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            var entity = await _repository.GetAsync(id);
            if (entity == null) return false;
            return await _repository.DeleteOrUpdateAsync(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            var entity = _repository.Get(id);
            if (entity == null) return false;
            return _repository.DeleteOrUpdate(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            var entity = await _repository.GetAsync(id);
            if (entity == null) return false;
            return await _repository.DeleteOrUpdateAsync(entity);
        }
        /// <summary>
        /// This method will query the database given an object.
        /// Will dynamicly create the SQL string and its parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IEnumerable<TOutput> GetDynamic<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var result = _repository.QueryDynamic(obj);
            return _mapper.Map<IEnumerable<TOutput>>(result);
        }

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TOutput>> GetDynamicAsync<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var result = await _repository.QueryDynamicAsync(obj);
            return _mapper.Map<IEnumerable<TOutput>>(result);
        }

        /// <summary>
        /// This method will query the database synchronously given an object.
        /// Will dynamicly create the SQL string and its parameters,
        /// and return a list of elements given pagination parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public (Pagination pagination, IEnumerable<TOutput> elements) GetDynamicPaged<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var result = _repository.QueryDynamicPaged(obj);
            var elements = _mapper.Map<IEnumerable<TOutput>>(result.elements);
            return (result.pagination, elements);
        }

        /// <summary>
        /// This method will query the database asynchronously given an object.
        /// Will dynamicly create the SQL string and its parameters,
        /// and return a list of elements given pagination parameters
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<(Pagination pagination, IEnumerable<TOutput> elements)> GetDynamicPagedAsync<TInput, TOutput>(TInput obj) where TInput : class where TOutput : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var result = await _repository.QueryDynamicPagedAsync(obj);
            var elements = _mapper.Map<IEnumerable<TOutput>>(result.elements);
            return (result.pagination, elements);
        }
    }
}
