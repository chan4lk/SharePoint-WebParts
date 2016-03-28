namespace SuperMarketSystem.Models
{
    /// <summary>
    /// The order domain item.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Models.IDomainEntity" />
    public class Order : IDomainEntity
    {
        /// <summary>
        /// The order identifier.
        /// </summary>
        private int orderId;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get
            {
                return this.orderId;
            }

            set
            {
                this.orderId = value;
            }
        }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>
        /// The invoice identifier.
        /// </value>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal Total { get; set; }
    }
}
