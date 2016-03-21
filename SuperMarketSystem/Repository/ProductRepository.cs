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
        #region Fields - Contants
        /// <summary>
        /// The list name.
        /// </summary>
        private const string ListName = "Product";

        /// <summary>
        /// The field identifier.
        /// </summary>
        private const string FieldId = "ID";

        /// <summary>
        /// The field category.
        /// </summary>
        private const string FieldCategory = "CategoryName";

        /// <summary>
        /// The field price
        /// </summary>
        private const string FieldPrice = "Price"; 
        #endregion
        
        #region Methods - IRepository Members

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The identifier.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public int Create(Product item)
        {
            throw new NotImplementedException();
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
        public int Update(Product item)
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
        /// The product.
        /// </returns>
        public Product GetById(int id)
        {
            Product item = null;

            SPSite site = SPContext.Current.Site;
            SPWeb web = site.RootWeb;
            SPList list = web.Lists[ProductRepository.ListName];
            SPQuery query = new SPQuery()
            {
                Query = @"<Where><Eq>
                                        <FieldRef Name='ID' />
                                        <Value Type='Number'>
                                        " +
                                  id
                                  + @"</Value>
                                    </Eq></Where>
                                  ",
                ViewFields = string.Concat("<FieldRef Name='CategoryName' />", "<FieldRef Name='ID' />", "<FieldRef Name='Price' />")
            };

            SPListItemCollection products = list.GetItems(query);
            item = new Product
            {
                ProductId = int.Parse(products[0][FieldId].ToString()),
                Price = decimal.Parse(products[0][FieldPrice].ToString()),
                Category = products[0][FieldCategory].ToString().Remove(0, 3)
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
                    SPList list = web.Lists[ProductRepository.ListName];
                    SPQuery query = new SPQuery()
                    {
                        Query = @"<Query>                                   
                                  </Query>",
                        ViewFields = string.Concat(
                            "<FieldRef Name='Category' />",
                            "<FieldRef Name='ID' />",
                            "<FieldRef Name='Price' />")
                    };

                    SPListItemCollection products = list.GetItems(query);

                    items = new List<Product>();

                    foreach (SPListItem product in products)
                    {
                        var item = new Product
                        {
                            ProductId = int.Parse(product[FieldId].ToString()),
                            Price = decimal.Parse(product[FieldPrice].ToString()),
                            Category = product[FieldCategory].ToString()
                        };

                        items.Add(item);
                    }
                }
            }

            return items;
        }

        #endregion

        #region Methods - IDisposable Members
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("Disposing");
        } 
        #endregion
    }
}
