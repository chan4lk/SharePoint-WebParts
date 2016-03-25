#region Imports
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
        #region Fields - Private Members
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

        #region Methods - Public Members(IRepository)
        /// <summary>
        /// Creates the specified order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>
        /// 0 if success.
        /// </returns>
        public int Create(Order order)
        {
            int result = 0;

            using (SPSite site = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[OrderRepository.ListName];
                    SPListItem item = list.Items.Add();

                    item[FieldProductId] = order.ProductId;
                    item[FieldInvoiceId] = order.InvoiceId;
                    item[FieldQuantity] = order.Quantity;
                    item[FieldTotal] = order.Total;

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
    }
}
