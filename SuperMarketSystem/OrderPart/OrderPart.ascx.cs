using Microsoft.Practices.Unity;
using SuperMarketSystem.Common;
using SuperMarketSystem.Models;
using SuperMarketSystem.Presenters;
using SuperMarketSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace SuperMarketSystem.OrderPart
{
    [ToolboxItemAttribute(false)]
    public partial class OrderPart : WebPart, IOrderView
    {
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
        /// Gets or sets the presenter.
        /// </summary>
        /// <value>
        /// The presenter.
        /// </value>
        public IOrderPresenter Presenter { get; set; }

        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling using
        // the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public OrderPart()
        {
            this.Presenter = ConfigurationManager.Container.Resolve<IOrderPresenter>();
        }

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
            this.Presenter.Initailize(this);
            this.orderView.DataSource = this.Model.Items;
            this.orderView.DataBind();
        }

        /// <summary>
        /// Adds the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AddButtonClick(object sender, EventArgs e)
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
        /// Add product item.
        /// </summary>
        public void Add()
        {
            this.Presenter.Add(new ProductItem { Quantity = 3, ProductId = 3, Total = 12.5M });
        }

        /// <summary>
        /// Submits order.
        /// </summary>
        public void Submit()
        {
            this.Presenter.Submit();
        }
    }
}
