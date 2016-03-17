using Microsoft.Practices.Unity;
using SuperMarketSystem.Commands;
using SuperMarketSystem.Common;
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
    /// Order presenter.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Presenters.IOrderPresenter" />
    public class OrderPresenter : IOrderPresenter
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public IOrderView View
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderPresenter"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public OrderPresenter()
        {
        }

        /// <summary>
        /// Initailizes the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        public void Initailize(IOrderView view)
        {
            this.View = view;            
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(ProductItem item)
        {
            ///// Workarround for showing the empty grid.
            //if (this.View.Model.Items.Count == 1)
            //{
            //    this.View.Model.Items.Clear();
            //}

            this.View.Model.Items.Add(item);
            Console.WriteLine("Item added");
        }

        /// <summary>
        /// Submits this instance.
        /// </summary>
        public void Submit()
        {
            foreach (var item in this.View.Model.Items)
            {
                var order = new Order() { ProductId = item.ProductId, Total = item.Total };
                Command command = new PlaceOrderCommand() { Order = order};
                command.Execute();
            }
            this.View.Clear();
        }


        /// <summary>
        /// Adds the specified product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="quantity">The quantity.</param>
        public void Add(int productId, int quantity)
        {
            var item = new ProductItem
            { 
                ProductId = productId, 
                Quantity = quantity, 
                Total = productId* quantity 
                ///TODO: calculate total
            };

            this.Add(item);
        }
    }
}
