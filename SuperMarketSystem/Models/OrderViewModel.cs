﻿using System.Collections.Generic;
using System.Linq;

namespace SuperMarketSystem.Models
{
    /// <summary>
    /// The order view model for order grid view.
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<ProductItem> Items { get; set; }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal Total
        {
            get
            {
                return this.Items.Sum(i => i.Total);
            }
        }
    }
}
