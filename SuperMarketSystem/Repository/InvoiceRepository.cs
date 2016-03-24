using Microsoft.SharePoint;
using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;

namespace SuperMarketSystem.Repository
{
    /// <summary>
    /// The invoice repository.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Repository.IRepository{SuperMarketSystem.Models.Invoice}" />
    public class InvoiceRepository : IRepository<Invoice>
    {
        /// <summary>
        /// The list name.
        /// </summary>
        private const string ListName = "Invoice";

        /// <summary>
        /// The field invoice identifier.
        /// </summary>
        private const string FieldInvoiceId = "InvoiceId";

        /// <summary>
        /// The field date.
        /// </summary>
        private const string FieldDate = "Date";

        /// <summary>
        /// The field total.
        /// </summary>
        private const string FieldTotal = "Total";

        /// <summary>
        /// Creates the specified invoice.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// 0 if success.
        /// </returns>
        public int Create(Invoice invoice)
        {
            int result = 0;

            using (SPSite site = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[InvoiceRepository.ListName];
                    SPListItem item = list.Items.Add();

                    item[FieldTotal] = invoice.Total;
                    item[FieldDate] = invoice.Date;
                    item.Update();
                    result = item.ID;
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
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public List<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
