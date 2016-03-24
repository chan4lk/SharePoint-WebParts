using SuperMarketSystem.Models;
using SuperMarketSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketSystem.Commands
{
    /// <summary>
    /// This command place a new order.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Commands.Command" />
    public class CreateOrderCommand : Command
    {
        #region Fields - Private Members
        /// <summary>
        /// The order repository.
        /// </summary>
        private IRepository<Order> orderRepository;
        #endregion

        #region Properties - Public Members
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public Order Order { get; set; }

        #endregion

        #region Methods - Public Members - Contructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderCommand"/> class.
        /// </summary>
        public CreateOrderCommand()
            : this(new OrderRepository())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderCommand"/> class.
        /// </summary>
        /// <param name="orderRepository">The order repository.</param>
        public CreateOrderCommand(IRepository<Order> orderRepository)
            : base()
        {
            // TODO: Complete member initialization
            this.orderRepository = orderRepository;
        }
        #endregion

        #region Methods - Public Members - Command Members
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

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            try
            {
                this.Order.Id = this.orderRepository.Create(this.Order);
            }
            catch (Exception)
            {
                this.Logger.Error("Could not create order");
                throw;
            }
        }
        #endregion
    }
}
