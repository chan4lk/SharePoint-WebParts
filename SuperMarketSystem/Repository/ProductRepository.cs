using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace SuperMarketSystem.Repository
{
    /// <summary>
    /// The Product repository.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Repository.IRepository{SuperMarketSystem.Models.Product}" />
    public class ProductRepository : IRepository<Product>, IDisposable
    {
        private const string LIST_NAME = "Product";
        private const string ID_FIELD = "ProductId";
        private const string CATEGORY_FIELD = "CategoryName";
        private const string PRICE_FIELD = "Price";

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int Create(Product item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int Update(Product item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Product GetById(int id)
        {
            Product item = null;

            SPSite site = SPContext.Current.Site;
            SPWeb web = site.RootWeb;
            SPList list = web.Lists[ProductRepository.LIST_NAME];
            SPQuery query = new SPQuery()
            {
                Query = @"<Where><Eq>
                                        <FieldRef Name='ProductId' />
                                        <Value Type='Number'>
                                        " +
                                  id
                                  + @"</Value>
                                    </Eq></Where>
                                  ",
                ViewFields = string.Concat("<FieldRef Name='CategoryName' />", "<FieldRef Name='ProductId' />", "<FieldRef Name='Price' />")
            };

            SPListItemCollection products = list.GetItems(query);
            item = new Product
            {
                ProductId = int.Parse(products[0][ID_FIELD].ToString()),
                Price = decimal.Parse(products[0][PRICE_FIELD].ToString()),
                Category = products[0][CATEGORY_FIELD].ToString()
            };

            return item;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// All of the products.
        /// </returns>
        public List<Product> GetAll()
        {
            List<Product> items = null;

            using (SPSite site = SPContext.Current.Site)
            {
                using (SPWeb web = site.RootWeb)
                {
                    SPList list = web.Lists[ProductRepository.LIST_NAME];
                    SPQuery query = new SPQuery()
                    {
                        Query = @"<Query>                                   
                                  </Query>",
                        ViewFields = string.Concat(
                            "<FieldRef Name='Category' />",
                            "<FieldRef Name='ProductId' />",
                            "<FieldRef Name='Price' />")
                    };

                    SPListItemCollection products = list.GetItems(query);

                    items = new List<Product>();

                    foreach (SPListItem product in products)
                    {
                        var item = new Product
                        {
                            ProductId = int.Parse(product[ID_FIELD].ToString()),
                            Price = decimal.Parse(product[PRICE_FIELD].ToString()),
                            Category = product[CATEGORY_FIELD].ToString()
                        };

                        items.Add(item);
                    }

                }

            }

            return items;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("Disposing");
        }
    }
}
