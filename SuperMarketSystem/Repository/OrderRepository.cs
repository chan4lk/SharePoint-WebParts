﻿#region Imports
using Microsoft.SharePoint;
using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;
#endregion

namespace SuperMarketSystem.Repository
{
    /// <summary>
    /// The order repository.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Repository.IRepository{SuperMarketSystem.Models.Order}" />
    public class OrderRepository : IRepository<Order>
    {
        #region Constants
        /// <summary>
        /// The list name.
        /// </summary>
        private const string ListName = "Order";

        /// <summary>
        /// The field order identifier.
        /// </summary>
        private const string FieldOrderId = "ID";

        /// <summary>
        /// The field invoice identifier.
        /// </summary>
        private const string FieldInvoiceId = "InvoiceId";

        /// <summary>
        /// The field product identifier.
        /// </summary>
        private const string FieldProductId = "ProductId";

        /// <summary>
        /// The field quantity.
        /// </summary>
        private const string FieldQuantity = "Quantity";

        /// <summary>
        /// The field total.
        /// </summary>
        private const string FieldTotal = "Total";

        #endregion

        #region Properties - Public Members

        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        public string SiteUrl { get; set; } 

        #endregion

        #region Methods -Public Members

        #region Methods - Public Members - Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        public OrderRepository()
            : this(SPContext.Current.Web.Url)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public OrderRepository(string url)
        {
            this.SiteUrl = url;
        }
        #endregion

        #region Methods - Public Members(IRepository)
        /// <summary>
        /// Creates the specified order.
        /// </summary>
        /// <param name="item">The order.</param>
        /// <returns>
        /// 0 if success.
        /// </returns>
        public int Create(Order item)
        {
            int result = 0;

            if (item == null)
            {
                throw new ArgumentNullException(
                    typeof(Order).Name, 
                    SupermarketResources.ArgumentNullError);
            }

            using (SPSite site = new SPSite(this.SiteUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[OrderRepository.ListName];
                    SPListItem listItem = list.Items.Add();

                    listItem[FieldProductId] = item.ProductId;
                    listItem[FieldInvoiceId] = item.InvoiceId;
                    listItem[FieldQuantity] = item.Quantity;
                    listItem[FieldTotal] = item.Total;

                    listItem.Update();

                    result = listItem.ID;
                }
            }

            return result;
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The identifier.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public int Update(Order item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <returns>
        /// The identifier if success.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <returns>
        /// The entity.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// All the entities.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion 
        #endregion
    }
}
