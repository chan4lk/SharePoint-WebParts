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
        #region Fields - Private members
        /// <summary>
        /// The product repository.
        /// </summary>
        private IRepository<Product> productRepository;
        #endregion

        #region Properties - Public members
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

        #endregion

        #region Methods - Public Members - Contructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductPriceCommand"/> class.
        /// </summary>
        public GetProductPriceCommand()
            : this(new ProductRepository())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductPriceCommand"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public GetProductPriceCommand(IRepository<Product> productRepository)
            : base()
        {
            this.productRepository = productRepository;
        }
        #endregion

        #region Methods - Public Members - Command Members
        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            try
            {
                this.Product = this.productRepository.GetById(this.ProductId);
            }
            catch (Exception exp)
            {
                this.Logger.Error(exp.Message);
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
        #endregion
    }
}
