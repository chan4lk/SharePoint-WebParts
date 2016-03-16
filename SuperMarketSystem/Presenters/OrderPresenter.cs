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

        public void Initailize(IOrderView view)
        {
            this.View = view;
            this.View.Model.Items = new List<ProductItem>
            {
                new ProductItem{Quantity = 1, ProductId = 3, Total = 25.5M},
                new ProductItem{Quantity = 2, ProductId = 3, Total = 55.5M},
                new ProductItem{Quantity = 3, ProductId = 3, Total = 12.5M}
            };
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(Models.ProductItem item)
        {
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
        }
    }
}
