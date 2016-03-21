using SuperMarketSystem.Common;
using SuperMarketSystem.Models;
using SuperMarketSystem.Presenters;
using SuperMarketSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;

namespace SuperMarketSystem.OrderPart
{
    /// <summary>
    /// The Order view code behind.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Common.BasePart{SuperMarketSystem.Presenters.OrderPresenter,SuperMarketSystem.Views.IOrderView}" />
    /// <seealso cref="System.Web.UI.WebControls.WebParts.WebPart" />
    /// <seealso cref="SuperMarketSystem.Views.IOrderView" />
    [ToolboxItemAttribute(false)]
    public partial class OrderPart : BasePart<OrderPresenter, IOrderView>, IOrderView
    {
        #region Properties - Public Members
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public OrderViewModel Model
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the product ids.
        /// </summary>
        /// <value>
        /// The product ids.
        /// </value>
        public List<int> ProductIds { get; set; }

        #endregion

        #region Constants - Privae Memebers

        /// <summary>
        /// The quantity label identifier.
        /// </summary>
        private const string QuantityLabelID = "QuantityLabel";

        /// <summary>
        /// The identifier label identifier.
        /// </summary>
        private const string IdLabelID = "IdLabel";

        /// <summary>
        /// The quantity text identifier.
        /// </summary>
        private const string QuantityTextID = "QuantityText";

        /// <summary>
        /// The identifier text identifier.
        /// </summary>
        private const string IdTextID = "ProductIdText";

        /// <summary>
        /// The total label.
        /// </summary>
        private const string TotalLabel = "TotalLabel";

        /// <summary>
        /// The error class.
        /// </summary>
        private const string ErrorClass = "err";

        /// <summary>
        /// The information class.
        /// </summary>
        private const string InfoClass = "info";
        #endregion

        #region Methods - Constructors
        /// Uncomment the following SecurityPermission attribute only when doing Performance Profiling using
        /// the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        /// for production. Because the SecurityPermission attribute bypasses the security check for callers of
        /// your constructor, it's not recommended for production purposes.
        /// [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderPart"/> class.
        /// </summary>
        public OrderPart()
            : base()
        {
            this.Presenter.Initailize(this);
        }
        #endregion

        #region Methods - Event Handlers
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.InitializeControl();
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Draw();
            }
        }

        /// <summary>
        /// Adds the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClearButtonClick(object sender, EventArgs e)
        {
            this.Clear();
        }

        /// <summary>
        /// Submits the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SubmitButtonClick(object sender, EventArgs e)
        {
            this.ReserveItems();
            this.Submit();
        }

        /// <summary>
        /// Called when [row created].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnRowCreated(object sender, EventArgs e)
        {
            Console.WriteLine("Row Created");
        }

        /// <summary>
        /// Called when [row command].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                this.Add();
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the orderView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label totalLabel = e.Row.FindControl(OrderPart.TotalLabel) as Label;
                totalLabel.Text = this.Model.Items.Sum(i => i.Total).ToString("C");
            }
        }

        #endregion

        #region Methods - IOrderView Members
        /// <summary>
        /// Add product item.
        /// </summary>
        public void Add()
        {
            this.ReserveItems();
            this.InsertNewItem();
            this.Draw();
        }

        /// <summary>
        /// Submits order.
        /// </summary>
        public void Submit()
        {
            this.Presenter.Submit();
        }

        /// <summary>
        /// Draws the grid.
        /// </summary>
        public void Draw()
        {
            // Workaround for showing the empty grid.
            if (this.Model.Items.Count == 0)
            {
                this.Clear();
            }
            else
            {
                this.orderView.DataSource = this.Model.Items;
                this.orderView.DataBind();
            }
        }

        /// <summary>
        /// Clears the grid.
        /// </summary>
        public void Clear()
        {
            this.Model.Items = new List<ProductItem>();
            this.Model.Items.Add(new ProductItem());      // Workaround for showing the empty grid.      
            this.orderView.DataSource = this.Model.Items;
            this.orderView.DataBind();
            this.orderView.Rows[0].Visible = false;     // Workaround for showing the empty grid.
            this.ShowMessage("Please add items to the cart");
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessage(string message)
        {
            this.MessageLabel.Text = message;
            this.MessageLabel.CssClass = InfoClass;
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="isError">if set to <c>true</c> [is error].</param>
        public void ShowMessage(string message, bool isError)
        {
            this.MessageLabel.Text = message;
            this.MessageLabel.CssClass = ErrorClass;
        }

        #endregion

        #region Methods - Helpers
        /// <summary>
        /// Inserts the new item.
        /// </summary>
        private void InsertNewItem()
        {
            TextBox quantityText = this.orderView.FooterRow.FindControl(QuantityTextID) as TextBox;
            TextBox productIdText = this.orderView.FooterRow.FindControl(IdTextID) as TextBox;

            // Escape invalid items.
            if (string.IsNullOrEmpty(productIdText.Text) || string.IsNullOrEmpty(quantityText.Text))
            {
                return;
            }

            int productId = int.Parse(productIdText.Text);
            int quantity = int.Parse(quantityText.Text);
            this.Presenter.Add(productId, quantity);
        }

        /// <summary>
        /// Reserves the items.
        /// </summary>
        private void ReserveItems()
        {
            for (int i = 0; i < this.orderView.Rows.Count; i++)
            {
                Label quantityLabel = this.orderView.Rows[i].FindControl(QuantityLabelID) as Label;
                Label idLabel = this.orderView.Rows[i].FindControl(IdLabelID) as Label;

                // Escape invalid items.
                if (string.IsNullOrEmpty(quantityLabel.Text) || string.IsNullOrEmpty(idLabel.Text))
                {
                    continue;
                }

                int productId = int.Parse(idLabel.Text);
                int quantity = int.Parse(quantityLabel.Text);
                this.Presenter.Add(productId, quantity);
            }
        }
        #endregion
    }
}
