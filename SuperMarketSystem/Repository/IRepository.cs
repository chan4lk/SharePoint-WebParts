using System.Collections.Generic;

namespace SuperMarketSystem.Repository
{
    /// <summary>
    /// The repository interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        string SiteUrl { get; set; }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The identifier.
        /// </returns>
        int Create(TEntity item);

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The identifier.
        /// </returns>
        int Update(TEntity item);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <returns>
        /// The identifier if success.
        /// </returns>
        int Delete(int id);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <returns>
        /// The entity.
        /// </returns>
        TEntity GetById(int id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// All the entities.
        /// </returns>
        IEnumerable<TEntity> GetAll();
    }
}
