using Microsoft.Practices.Unity;
using Microsoft.SharePoint;
using SuperMarketSystem.Presenters;
using SuperMarketSystem.Views;
using System;

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
                }

                return instance;
            }
        }

        /// <summary>
        /// Determines whether this instance is authenticated.
        /// </summary>
        /// <returns>
        /// <c>true</c> if authenticated.
        /// </returns>
        public static bool IsAuthenticated()
        {
            bool isAuthenticated = false;

            SPWeb web = SPContext.Current.Web;
            SPUser user = web.CurrentUser;

            try
            {
                user.Groups.GetByName(ConfigurationManager.SalesGroup);
                isAuthenticated = true;
            }
            catch (Exception)
            {
                isAuthenticated = false;
            } 

            return isAuthenticated;
        }
    }
}
