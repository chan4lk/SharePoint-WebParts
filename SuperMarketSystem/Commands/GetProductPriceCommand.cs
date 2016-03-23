using SuperMarketSystem.Models;
using SuperMarketSystem.Repository;
using System;
using System.Threading.Tasks;

namespace SuperMarketSystem.Commands
{
    /// <summary>
    /// Get the 
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Commands.Command" />
    public class GetProductPriceCommand : Command
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product { get; set; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            try
            {
                IRepository<Product> reposiory = new ProductRepository();
                this.Product = reposiory.GetById(this.ProductId);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                throw;
            }
        }

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented.
        /// </exception>
        public override Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
