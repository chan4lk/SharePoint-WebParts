using SuperMarketSystem.Commands;
using SuperMarketSystem.Common;
using SuperMarketSystem.Models;
using SuperMarketSystem.Views;
using System;
using System.Collections.Generic;

namespace SuperMarketSystem.Presenters
{
    /// <summary>
    /// Order presenter.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Presenters.IOrderPresenter" />
    public class OrderPresenter : IOrderPresenter<IOrderView>
    {
        #region Properties - Public Memebers
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
        #endregion

        #region Methods - Public Members
        #region Methods - Public Members - Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderPresenter" /> class.
        /// </summary>
        public OrderPresenter()
        {
        }
        #endregion

        #region Methods - Public - IOrderPresenter Members
        /// <summary>
        /// Initializes the specified view.
        /// </summary>
        public void Initailize()
        {
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
            this.View.ShowMessage("Item added to cart");
        }

        /// <summary>
        /// Submits this order.
        /// </summary>
        public void Submit()
        {
            if (this.View.Model.Items.IsEmpty())
            {
                this.View.ShowMessage("There are no orders.", true);
                return;
            }

            try
            {
                int invoiceID = this.CreateInvoice();

                foreach (var item in this.View.Model.Items)
                {
                    var order = new Order()
                    {
                        ProductId = item.ProductId,
                        InvoiceId = invoiceID,
                        Quantity = item.Quantity,
                        Total = item.Total
                    };

                    Command orderCommand = new CreateOrderCommand() { Order = order };
                    try
                    {
                        orderCommand.Execute();
                    }
                    catch (Exception)
                    {
                        this.View.ShowMessage("Could not create Order", true);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                this.View.ShowMessage("Could not create Invoice", true);
            }

            this.View.Clear();
            this.View.ShowMessage("Invoice created");
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
            try
            {
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
            catch (Exception)
            {
                this.View.ShowMessage("No product item with given ID", true);
            }
        }
        #endregion
        #endregion

        #region Method - Private Members (Helpers)
        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <returns>
        /// The invoice Identifier.
        /// </returns>
        private int CreateInvoice()
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

            return invoiceID;
        } 
        #endregion
        
    }
}
