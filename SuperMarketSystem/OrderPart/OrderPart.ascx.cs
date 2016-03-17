using Microsoft.Practices.Unity;
using SuperMarketSystem.Common;
using SuperMarketSystem.Models;
using SuperMarketSystem.Presenters;
using SuperMarketSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SuperMarketSystem.OrderPart
{
    /// <summary>
    /// The Order view code behind.
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.WebParts.WebPart" />
    /// <seealso cref="SuperMarketSystem.Views.IOrderView" />
    [ToolboxItemAttribute(false)]
    public partial class OrderPart : WebPart, IOrderView
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

        public List<int> ProductIds { get; set; }

        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        /// <value>
        /// The presenter.
        /// </value>
        [Dependency]
        public IOrderPresenter Presenter
        {
            get;
            set;
        }
        #endregion

        #region Constants - Privae Memebers
        private const string quantityLabelID = "QuantityLabel";
        private const string IdLabelID = "IdLabel";
        private const string quantityTextID = "QuantityText";
        private const string IdTextID = "ProductIdText"; 
        #endregion

        #region Methods - Constructors
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling using
        // the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public OrderPart()
        {
            this.Presenter = ConfigurationManager.Container.Resolve<IOrderPresenter>();
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
            InitializeControl();
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Model = new OrderViewModel();
            this.Model.Items = new List<ProductItem>();

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
            this.Submit();
        }

        /// <summary>
        /// Called when [row created].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnRowCreated(object sender, EventArgs e)
        {
            /// Handle.
            //this.Add();
            Console.WriteLine("Row Created");
        }

        protected void OnRowCommand(object sender,  GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                // int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                // GridViewRow row = orderView.Rows[index];

                this.Add();
            }

        }

        #endregion

        #region Methods - IOrderView Members
        /// <summary>
        /// Add product item.
        /// </summary>
        public void Add()
        {
            ReserveItems();
            InsertNewItem();
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
            if (this.Model.Items.Count == 0) ///Workaaround for showing the empty grid.
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
            this.Model.Items.Add(new ProductItem());      ///Workaaround for showing the empty grid.      
            this.orderView.DataSource = this.Model.Items;
            this.orderView.DataBind();
            this.orderView.Rows[0].Visible = false;     ///Workaaround for showing the empty grid.
        }
        #endregion

        #region Methods - Helpers
        /// <summary>
        /// Inserts the new item.
        /// </summary>
        private void InsertNewItem()
        {
            TextBox quantityText = this.orderView.FooterRow.FindControl(quantityTextID) as TextBox;
            TextBox productIdText = this.orderView.FooterRow.FindControl(IdTextID) as TextBox;

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
                Label quantityLabel = this.orderView.Rows[i].FindControl(quantityLabelID) as Label;
                Label idLabel = this.orderView.Rows[i].FindControl(IdLabelID) as Label;
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
