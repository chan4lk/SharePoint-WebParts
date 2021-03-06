﻿#region Imports
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using SuperMarketSystem.Common;
using SuperMarketSystem.Models;
using SuperMarketSystem.Presenters;
using SuperMarketSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.WebControls;
#endregion

namespace SuperMarketSystem.OrderPart
{
    /// <summary>
    /// The Order view code behind.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Common.BasePart{SuperMarketSystem.Presenters.OrderPresenter,SuperMarketSystem.Views.IOrderView}" />
    /// <seealso cref="System.Web.UI.WebControls.WebParts.WebPart" />
    /// <seealso cref="SuperMarketSystem.Views.IOrderView" />
    [ToolboxItemAttribute(false)]
    public partial class OrderWebPart : BasePart<OrderPresenter, IOrderView>, IOrderView
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
        /// Gets or sets a value indicating whether this instance is submitted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is submitted; otherwise, <c>false</c>.
        /// </value>
        private bool IsSubmitted { get; set; }

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
        /// The add button identifier.
        /// </summary>
        private const string AddButtonID = "AddButton";

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

        /// <summary>
        /// The add to cart command.
        /// </summary>
        private const string AddToCartCommand = "AddToCart";
        #endregion

        #region Methods - Constructors
        /// Uncomment the following SecurityPermission attribute only when doing Performance Profiling using
        /// the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        /// for production. Because the SecurityPermission attribute bypasses the security check for callers of
        /// your constructor, it's not recommended for production purposes.
        /// [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderWebPart"/> class.
        /// </summary>
        public OrderWebPart()
            : base()
        {
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
            //// Check if the current user has access to Sales Group.
            if (!ConfigurationManager.IsAuthenticated)
            {
                SPUtility.SendAccessDeniedHeader(
                    new SPException(SupermarketResources.AuthenticationError));
            }

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
        private void OnClearButtonClick(object sender, EventArgs e)
        {
            this.Clear();
        }

        /// <summary>
        /// Submits the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnSubmitButtonClick(object sender, EventArgs e)
        {
            this.ReserveItems();
            this.Submit();
        }

        /// <summary>
        /// Called when [row created].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox quantityText = e.Row.FindControl(QuantityTextID) as TextBox;
                quantityText.TextChanged -= this.OnAdd;
                quantityText.TextChanged += this.OnAdd;
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the orderView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(
                    typeof(GridViewRowEventArgs).Name,
                    SupermarketResources.ArgumentNullError);
            }
            else if (e.Row == null)
            {
                throw new ArgumentNullException(
                    typeof(GridViewRow).Name,
                    SupermarketResources.ArgumentNullError);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox productIdText = e.Row.FindControl(IdTextID) as TextBox;

                //// Disable post back on press Enter key
                productIdText.Attributes.Add("onkeydown", "return (event.keyCode!=13)");

                //// Disable controls if submit button was hit.
                if (this.IsSubmitted)
                {
                    Label totalLabel = e.Row.FindControl(OrderWebPart.TotalLabel) as Label;
                    totalLabel.Text = this.Model.Total.ToString("C", CultureInfo.InvariantCulture);

                    TextBox quantityText = e.Row.FindControl(QuantityTextID) as TextBox;

                    quantityText.Enabled = false;
                    productIdText.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the quantityText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAdd(object sender, EventArgs e)
        {
            this.Add();
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
            this.ShowMessage(SupermarketResources.MessageAddItem);
        }

        /// <summary>
        /// Freezes this view.
        /// </summary>
        public void Freeze()
        {
            this.IsSubmitted = true;
            this.Draw();
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

            int productId;
            int quantity;

            if (int.TryParse(productIdText.Text, out productId) &&
                int.TryParse(quantityText.Text, out quantity))
            {
                this.Presenter.Add(productId, quantity);
            }
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
                if (string.IsNullOrEmpty(quantityLabel.Text) ||
                    string.IsNullOrEmpty(idLabel.Text))
                {
                    continue;
                }

                int productId;
                int quantity;

                if (int.TryParse(idLabel.Text, out productId) &&
                    int.TryParse(quantityLabel.Text, out quantity))
                {
                    this.Presenter.Add(productId, quantity);
                }
            }
        }
        #endregion
    }
}
