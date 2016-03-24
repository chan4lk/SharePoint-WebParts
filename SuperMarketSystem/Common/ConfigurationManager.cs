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
    public class ConfigurationManager
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
                    instance.RegisterType<IOrderView, OrderPart.OrderPart>()
                             .RegisterType<IOrderPresenter<IOrderView>, OrderPresenter>();
                    instance.RegisterType<ILogger, UlsLogger>();
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
                            logger.Error("User is not on the sales group");
                            isAuthenticated = false;
                        }
                    }
                }

                return isAuthenticated;
            }
        }
    }
}
