﻿using Microsoft.SharePoint;
using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SuperMarketSystem.Repository
{
    /// <summary>
    /// The invoice repository.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Repository.IRepository{SuperMarketSystem.Models.Invoice}" />
    public class InvoiceRepository : IRepository<Invoice>
    {
        #region Constants
        /// <summary>
        /// The list name.
        /// </summary>
        private const string ListName = "Invoice";

        /// <summary>
        /// The field invoice identifier.
        /// </summary>
        private const string FieldInvoiceId = "ID";

        /// <summary>
        /// The field date.
        /// </summary>
        private const string FieldDate = "InvoiceDate";

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

        #region Methods - Public Members
        #region Methods - Public Members - Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
        /// </summary>
        public InvoiceRepository()
            : this(SPContext.Current.Web.Url)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public InvoiceRepository(string url)
        {
            this.SiteUrl = url;
        }
        #endregion

        #region Methods - Public Members - IRepository Members
        /// <summary>
        /// Creates the specified invoice.
        /// </summary>
        /// <param name="item">
        /// The invoice.
        /// </param>
        /// <returns>
        /// 0 if success.
        /// </returns>
        public int Create(Invoice item)
        {
            int result = 0;

            if (item == null)
            {
                throw new ArgumentNullException(
                    typeof(Invoice).Name,
                    SupermarketResources.ArgumentNullError);
            }

            using (SPSite site = new SPSite(this.SiteUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[InvoiceRepository.ListName];
                    SPListItem listItem = list.Items.Add();

                    listItem[FieldTotal] = item.Total;
                    listItem[FieldDate] = item.Date;
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
        /// Id if success.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public int Update(Invoice item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
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
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The invoice.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// All the records.
        /// </returns>
        public IEnumerable<Invoice> GetAll()
        {
            List<Invoice> items = null;

            using (SPSite site = new SPSite(this.SiteUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[InvoiceRepository.ListName];
                    SPQuery query = new SPQuery()
                    {
                        Query = @"<Query>                                   
                                  </Query>",
                        ViewFields = string.Concat(
                            "<FieldRef Name='InvoiceDate' />",
                            "<FieldRef Name='ID' />",
                            "<FieldRef Name='Total' />")
                    };

                    SPListItemCollection products = list.GetItems(query);

                    items = new List<Invoice>();

                    foreach (SPListItem product in products)
                    {
                        int id;
                        decimal total;
                        if (int.TryParse(product[FieldInvoiceId].ToString(), out id) &&
                            decimal.TryParse(product[FieldTotal].ToString(), out total))
                        {
                            var item = new Invoice
                            {
                                Id = id,
                                Total = total,
                                Date = (DateTime)product[FieldDate]
                            };

                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// All the records.
        /// </returns>
        public IEnumerable<Invoice> GetByDate(DateTime date)
        {
            List<Invoice> items = null;

            using (SPSite site = new SPSite(this.SiteUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[InvoiceRepository.ListName];
                    SPQuery query = new SPQuery()
                    {
                        Query = string.Format(
                            CultureInfo.InvariantCulture,
                            SupermarketResources.InvoiceByDateQuery,
                            date,
                            date.AddDays(1)),
                        ViewFields = SupermarketResources.InvoiceViewFields
                    };

                    SPListItemCollection products = list.GetItems(query);

                    items = new List<Invoice>();

                    foreach (SPListItem product in products)
                    {
                        int id;
                        decimal total;
                        if (int.TryParse(product[FieldInvoiceId].ToString(), out id) &&
                            decimal.TryParse(product[FieldTotal].ToString(), out total))
                        {
                            var item = new Invoice
                            {
                                Id = id,
                                Total = total,
                                Date = (DateTime)product[FieldDate]
                            };

                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }
        #endregion
        #endregion
    }
}
