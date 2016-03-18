using Microsoft.SharePoint;
using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketSystem.Repository
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private const string LIST_NAME = "Invoice";
        private const string FIELD_INVOICE_ID = "InvoiceId";
        private const string FIELD_DATE = "Date";
        private const string FIELD_TOTAL = "Total";

        /// <summary>
        /// Creates the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>
        /// 0 if success.
        /// </returns>
        public int Create(Invoice invoice)
        {
            int result = 0;

            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists[InvoiceRepository.LIST_NAME];
            SPListItem item = list.Items.Add();

            item[FIELD_TOTAL] = invoice.Total;
            item[FIELD_DATE] = invoice.Date;

            item.Update();

            result = item.ID;

            return result;
        }

        public int Update(Invoice item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
