using SuperMarketSystem.Models;
using SuperMarketSystem.Views;

namespace SuperMarketSystem.Presenters
{
    /// <summary>
    /// Order presenter contract.
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <seealso cref="SuperMarketSystem.Presenters.IPresenter" />
    public interface IOrderPresenter<TView> : IPresenter where TView : IOrderView
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        TView View { get; set; }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Add(ProductItem item);

        /// <summary>
        /// Adds the specified product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="quantity">The quantity.</param>
        void Add(int productId, int quantity);

        /// <summary>
        /// Submits this order.
        /// </summary>
        void Submit();
    }
}
