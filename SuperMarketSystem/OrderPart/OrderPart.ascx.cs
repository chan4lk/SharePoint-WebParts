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

        #region Methods - Constructors
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling using
        // the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public OrderPart()
        {
            this.Presenter = ConfigurationManager.Container.Resolve<IOrderPresenter>();
            this.Model = new OrderViewModel();
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
                this.ProductIds = new List<int> { 1, 2, 3, 4 };
                this.Draw();
        }

        /// <summary>
        /// Adds the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClearButtonClick(object sender, EventArgs e)
        {
            this.Add();
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

        protected void AddButton_Click(object sender, EventArgs e)
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
            TextBox quantityText = this.orderView.FooterRow.FindControl("QuantityText") as TextBox;
            TextBox productIdText = this.orderView.FooterRow.FindControl("ProductIdText") as TextBox;
            this.Presenter.Add(int.Parse(productIdText.Text), int.Parse(quantityText.Text));
        }

        /// <summary>
        /// Submits order.
        /// </summary>
        public void Submit()
        {
            this.Presenter.Submit();
        }
        #endregion


        public void Draw()
        {
            this.orderView.DataSource = this.Model.Items;
            this.orderView.DataBind();
        }

        public void Clear()
        {
            this.Model.Items = new List<ProductItem>();
            this.orderView.DataSource = this.Model.Items;
            this.orderView.DataBind();
        }
    }
}
