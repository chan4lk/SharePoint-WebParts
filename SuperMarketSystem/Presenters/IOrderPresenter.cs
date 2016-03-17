using SuperMarketSystem.Models;
using SuperMarketSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketSystem.Presenters
{
    /// <summary>
    /// Order presenter contract.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Presenters.IPresenter" />
    public interface IOrderPresenter : IPresenter
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        IOrderView View { get; set; }

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

        /// <summary>
        /// Initailizes the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        void Initailize(IOrderView view);
    }
}
