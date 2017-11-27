using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// Represent a write and read respository.All changes will persist when call <see cref="SaveChangesAsync"/>
    /// </summary>
    public interface IRespository<TEntity, TPrimaryKey>
        : IReadOnlyRespository<TEntity, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IStatusRecord
    {
        /// <summary>
        /// Asynchronous update matched entity ,and only given fields will update value.
        /// </summary>
        /// <param name="predicate">The predicate for matching entity</param>
        /// <param name="fieldSelectors">The selectors for selecting field</param>
        /// <returns>A Task</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null</exception>
        /// <exception cref="ArgumentNullException"><paramref name="fieldSelectors"/> is null</exception>
        Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] fieldSelectors);
        /// <summary>
        /// Asynchronous update matched entity ,and only given fields will update value.
        /// </summary>
        /// <param name="predicate">The predicate for matching entity</param>
        /// <returns>A Task</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null</exception>
        Task UpdateAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Asynchronous remove matched entity.
        /// </summary>
        /// <param name="predicate">The predicate for matching entity</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null</exception>
        Task RemoveAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Asynchronous remove given entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveAsync(TEntity entity);
        /// <summary>
        /// Asynchronous add a entity.
        /// </summary>
        /// <param name="entity">entity need add</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null</exception>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// Apply all modifying to disk or database or cache server.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
