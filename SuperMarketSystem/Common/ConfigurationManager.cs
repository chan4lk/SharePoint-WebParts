#region Imports
using Microsoft.Practices.Unity;
using Microsoft.SharePoint;
using SuperMarketSystem.Diagnostics;
using SuperMarketSystem.Presenters;
using SuperMarketSystem.Views;
using System;
#endregion

namespace SuperMarketSystem.Common
{
    /// <summary>
    /// The configuration manager.
    /// </summary>
    public sealed class ConfigurationManager
    {
        /// <summary>
        /// The sales group.
        /// </summary>
        public const string SalesGroup = "Sales";

        /// <summary>
        /// The container instance.
        /// </summary>
        private static IUnityContainer instance;

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
                if (instance == null)
                {
                    instance = new UnityContainer();
                    instance.RegisterType<IOrderView, OrderPart.OrderWebPart>()
                            .RegisterType<IOrderPresenter<IOrderView>, OrderPresenter>()
                            .RegisterType<ILogger, UnifiedSystemLogger>();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public static bool IsAuthenticated
        {
            get
            {
                bool isAuthenticated = false;

                using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPUser user = web.CurrentUser;

                        try
                        {
                            user.Groups.GetByName(ConfigurationManager.SalesGroup);
                            isAuthenticated = true;
                        }
                        catch (Exception)
                        {
                            ILogger logger = ConfigurationManager.Container.Resolve<ILogger>();
                            logger.Err(SupermarketResources.AuthenticationError);
                            isAuthenticated = false;
                        }
                    }
                }

                return isAuthenticated;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ConfigurationManager"/> class from being created.
        /// </summary>
        private ConfigurationManager()
        {
        }
    }
}
