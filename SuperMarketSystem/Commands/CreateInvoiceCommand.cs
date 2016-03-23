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
        /// The invoice repository.
        /// </summary>
        private IRepository<Invoice> invoiceRepository;

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
        /// Initializes a new instance of the <see cref="CreateInvoiceCommand"/> class.
        /// </summary>
        public CreateInvoiceCommand() : this(new InvoiceRepository())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvoiceCommand" /> class.
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository.</param>
        public CreateInvoiceCommand(IRepository<Invoice> invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            this.InvoiceId = this.invoiceRepository.Create(this.Invoice);
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
