using Microsoft.Practices.Unity;
using SuperMarketSystem.Presenters;
using SuperMarketSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarketSystem.Common
{
    /// <summary>
    /// The configuration manager.
    /// </summary>
    public class ConfigurationManager
    {
        /// <summary>
        /// The container instance.
        /// </summary>
        private static IUnityContainer _instance;

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static IUnityContainer Container
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnityContainer();
                    _instance.RegisterType<IOrderView, OrderPart.OrderPart>()
                             .RegisterType<IOrderPresenter, OrderPresenter>();
                }

                return _instance;
            }
        }

    }
}
