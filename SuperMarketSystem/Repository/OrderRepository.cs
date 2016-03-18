using Microsoft.SharePoint;
using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketSystem.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private const string LIST_NAME = "Order";
        private const string FIELD_ORDER_ID = "OrderId";
        private const string FIELD_INVOICE_ID = "InvoiceId";
        private const string FIELD_PRODUCT_ID = "ProductId";
        private const string FIELD_QUANTITY = "Quantity";
        private const string FIELD_Total = "Total";

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

            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists[OrderRepository.LIST_NAME];
            SPListItem item = list.Items.Add();

            item[FIELD_PRODUCT_ID] = order.ProductId;
            item[FIELD_INVOICE_ID] = order.InvoiceId;
            item[FIELD_QUANTITY] = order.Quantity;
            item[FIELD_Total] = order.Total;

            item.Update();

            result = item.ID;

            return result;
        }

        public int Update(Order item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
