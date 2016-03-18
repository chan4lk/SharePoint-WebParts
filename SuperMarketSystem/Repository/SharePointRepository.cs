using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace SuperMarketSystem.Repository
{
    public class SharePointRepository : IRepository<Product>
    {
        public int Create(Product item)
        {
            throw new NotImplementedException();
        }

        public int Update(Product item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            Product item = null;

            using (SPSite site = SPContext.Current.Site)
            {
                using (SPWeb web = site.RootWeb)
                {
                    SPList Products = web.Lists[""];
                }

            }

            return item;
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
