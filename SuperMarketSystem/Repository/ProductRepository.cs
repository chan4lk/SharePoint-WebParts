using Microsoft.SharePoint;
using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SuperMarketSystem.Repository
{
    /// <summary>
    /// The Product repository.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Repository.IRepository{SuperMarketSystem.Models.Product}" />
    public class ProductRepository : IRepository<Product>
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
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        public ProductRepository()
            : this(SPContext.Current.Web.Url)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public ProductRepository(string url)
        {
            this.SiteUrl = url;
        }

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

            using (SPSite site = new SPSite(this.SiteUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[ProductRepository.ListName];
                    SPQuery query = new SPQuery()
                    {
                        Query = string.Format(
                            CultureInfo.InvariantCulture,
                            SupermarketResources.ProductByIDQuery,
                            id),
                        ViewFields = SupermarketResources.ProductViewFileds
                    };

                    SPListItemCollection products = list.GetItems(query);

                    if (products.Count > 1)
                    {
                        throw new Exception(SupermarketResources.ProductNotUniqeError);
                    }

                    item = new Product
                    {
                        ProductId = int.Parse(products[0][FieldId].ToString()),
                        Price = decimal.Parse(products[0][FieldPrice].ToString()),
                        Category = products[0][FieldCategory].ToString().Remove(0, 3)
                    };
                }
            }

            return item;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// All of the products.
        /// </returns>
        public IEnumerable<Product> GetAll()
        {
            List<Product> items = null;

            using (SPSite site = new SPSite(this.SiteUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[ProductRepository.ListName];
                    SPQuery query = new SPQuery()
                    {
                        Query = SupermarketResources.ProductAllQuery,
                        ViewFields = SupermarketResources.ProductViewFileds
                    };

                    SPListItemCollection products = list.GetItems(query);

                    items = new List<Product>();

                    foreach (SPListItem product in products)
                    {
                        int productId;
                        decimal price;
                        
                        if (int.TryParse(product[FieldId].ToString(), out productId)
                            && decimal.TryParse(product[FieldPrice].ToString(), out price))
                        {
                            var item = new Product
                            {
                                ProductId = productId,
                                Price = price,
                                Category = product[FieldCategory].ToString()
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
