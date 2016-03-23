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
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public Order Order { get; set; }

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
                IRepository<Order> repository = new OrderRepository();
                repository.Create(this.Order);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not create order");
                throw;
            }
        }
    }
}
