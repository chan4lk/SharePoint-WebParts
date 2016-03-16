using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketSystem.Models
{
    /// <summary>
    /// The order view model for order grid view.
    /// </summary>
    public class OrderViewModel
    {
        public List<ProductItem> Items { get; set; }

        public decimal Total { get; set; }

        public string UserName { get; set; }
    }
}
