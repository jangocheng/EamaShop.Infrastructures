using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// Represent a readonly local respository that store entity.
    /// It will get data from disk or database or cache server if local dosen't exist;
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TPrimaryKey">The type of identifier for <typeparamref name="TEntity"/> 
    /// </typeparam>
    public interface IReadOnlyRespository<TEntity, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IStatusRecord
    {
        /// <summary>
        /// Asynchronous returns the only entity of this respsitory ,or default value if the respsitory is emtpy;
        /// </summary>
        /// <param name="primaryKey">primary key</param>
        /// <returns>A searching task,if no entity matching,task return a null oncomplte,otherwise,return matched entity</returns>
        Task<TEntity> GetAsync(TPrimaryKey primaryKey);
        /// <summary>
        /// Asynchronous returns the only entity of this respository, or a default value if the respository is emtpy; 
        /// This method throws an exception if there is more than one entity in the respository.
        /// </summary>
        /// <param name="predicate">The predicate for matching entity in respository</param>
        /// <returns>A searching task,if no entity matching,task return a null oncomplte,otherwise,return matched entity</returns>
        /// <exception cref="ArgumentNullException">predicate</exception>
        /// <exception cref="InvalidOperationException">The respository contains more than one  entities was matched</exception>
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity,bool>> predicate);
        /// <summary>
        /// Asynchronous returns the first entity of this respository, or a default value if the respository is emtpy; 
        /// </summary>
        /// <param name="predicate">The predicate for matching entity in respsitory</param>
        /// <returns>A searching task,if no entity matching,task return a null oncomplte,otherwise,return matched entity</returns>
        /// <exception cref="ArgumentNullException">predicate is null</exception>
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Asynchronous returns the last entity of this respository, or a default value if the respository is emtpy; 
        /// </summary>
        /// <param name="predicate">The predicate for matching entity in respsitory</param>
        /// <returns>A searching task,if no entity matching,task return a null oncomplte,otherwise,return matched entity</returns>
        /// <exception cref="ArgumentNullException">predicate is null</exception>
        Task<TEntity> GetLastAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Asynchronous returns the multiple entities of this respository, or a empty value if the respository is emtpy; 
        /// </summary>
        /// <param name="predicate">The predicate for matching entity in respsitory</param>
        /// <returns>A searching task,if no entity matching,task return a null oncomplte,otherwise,return matched entity</returns>
        /// <exception cref="ArgumentNullException">predicate is null</exception>
        Task<IEnumerable<TEntity>> GetMutipleAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
