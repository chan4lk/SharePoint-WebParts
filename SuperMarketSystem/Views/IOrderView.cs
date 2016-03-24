using SuperMarketSystem.Models;

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

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="isError">if set to <c>true</c> [is error].</param>
        void ShowMessage(string message, bool isError);

        /// <summary>
        /// Freezes this view.
        /// </summary>
        void Freeze();
    }
}
