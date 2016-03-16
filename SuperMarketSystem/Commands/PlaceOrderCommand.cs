using SuperMarketSystem.Models;
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
    public class PlaceOrderCommand : Command
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
        public override void ExecuteAsync()
        {
            Console.WriteLine("Execute async {0} ", Order.ProductId);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            ExecuteAsync();
        }
    }
}
