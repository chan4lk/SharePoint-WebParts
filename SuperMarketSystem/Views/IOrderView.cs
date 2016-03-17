using SuperMarketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketSystem.Views
{
    /// <summary>
    /// The order view contract.
    /// </summary>
    public interface IOrderView
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        OrderViewModel Model { get; set; }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        void Add();

        /// <summary>
        /// Submits this instance.
        /// </summary>
        void Submit();

        /// <summary>
        /// Draws the grid.
        /// </summary>
        void Draw();

        /// <summary>
        /// Clears the grid.
        /// </summary>
        void Clear();
    }
}
