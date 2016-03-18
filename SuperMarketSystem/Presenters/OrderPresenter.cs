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
    public class OrderPresenter : IOrderPresenter<IOrderView>
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
            this.View.Model = new OrderViewModel();
            this.View.Model.Items = new List<ProductItem>();
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(ProductItem item)
        {
            this.View.Model.Items.Add(item);
            Console.WriteLine("Item added");
        }

        /// <summary>
        /// Submits this instance.
        /// </summary>
        public void Submit()
        {
            int invoiceID = 0;
            var total = this.View.Model.Total;

            Command command = new CreateInvoiceCommand
            {
                Invoice = new Invoice
                {
                    Total = total,
                    Date = DateTime.Now
                }
            };

            command.Execute();

            invoiceID = ((CreateInvoiceCommand)command).InvoiceId;            

            foreach (var item in this.View.Model.Items)
            {
                var order = new Order() 
                { 
                    ProductId = item.ProductId,
                    InvoiceId = invoiceID,
                    Quantity= item.Quantity, 
                    Total = item.Total 
                };
                Command orderCommand = new CreateOrderCommand() { Order = order};
                orderCommand.Execute();
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
            if (productId == 0 || quantity == 0)
            {
                Console.WriteLine("Cannot add item with zero count or id");
                return;
            }

            GetProductPriceCommand command = new GetProductPriceCommand()
            { 
                ProductId = productId
            };

            command.Execute();

            Product product = command.Product;

            var item = new ProductItem
            { 
                ProductId = productId, 
                Quantity = quantity, 
                Total = quantity * product.Price 
            };

            this.Add(item);
        }
    }
}
