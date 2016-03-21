﻿using SuperMarketSystem.Presenters;
using SuperMarketSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Practices.Unity; 

namespace SuperMarketSystem.Common
{
    /// <summary>
    /// The base web part class.
    /// </summary>
    /// <typeparam name="TPresenter">The type of the presenter.</typeparam>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <seealso cref="System.Web.UI.WebControls.WebParts.WebPart" />
    public abstract class BasePart<TPresenter, TView> : WebPart
        where TPresenter : IOrderPresenter<TView> 
        where TView : IOrderView
    {
        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        /// <value>
        /// The presenter.
        /// </value>
        protected IOrderPresenter<TView> Presenter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePart{TPresenter, TView}"/> class.
        /// </summary>
        public BasePart()
        {
            if (!(this is TView))
            {
                throw new InvalidOperationException("Must implement the generic type TView");
            }

            this.Presenter = ConfigurationManager.Container.Resolve<TPresenter>();
            this.Presenter.View = (TView)(object)this;
        }
    }
}
