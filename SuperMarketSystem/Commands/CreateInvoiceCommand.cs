using SuperMarketSystem.Models;
using SuperMarketSystem.Repository;
using System;
using System.Threading.Tasks;

namespace SuperMarketSystem.Commands
{
    /// <summary>
    /// The command to create an invoice.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Commands.Command" />
    public class CreateInvoiceCommand : Command
    {
        /// <summary>
        /// Gets or sets the invoice.
        /// </summary>
        /// <value>
        /// The invoice.
        /// </value>
        public Invoice Invoice { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>
        /// The invoice identifier.
        /// </value>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            IRepository<Invoice> reposioty = new InvoiceRepository();
            this.InvoiceId = reposioty.Create(this.Invoice);
        }

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not implemented yet.
        /// </exception>
        public override Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
