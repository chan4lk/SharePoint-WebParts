//-----------------------------------------------------------------------
// <copyright company="SomeCompany" file="EventReciver.cs">
// Copyright � 2016
// </copyright>
// <auto-generated />
//-----------------------------------------------------------------------
using Microsoft.SharePoint;
using System;
using System.Runtime.InteropServices;

namespace SuperMarketSystem.Features.SuperMarketFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>
    [Guid("7b50c7cb-da8c-431b-ab65-2a6ca50a584e")]
    public class SuperMarketFeatureEventReceiver : SPFeatureReceiver
    {
        /// Uncomment the method below to handle the event raised after a feature has been activated.
        ///public override void FeatureActivated(SPFeatureReceiverProperties properties)
        ///{
        ///    IUnityContainer container = ConfigurationManager.Container;
        ///    container.RegisterType<IOrderPresenter<IOrderView>, OrderPresenter>();
        ///}
          
        /// Uncomment the method below to handle the event raised before a feature is deactivated.
        ///public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        ///{
        ///}
          
        /// Uncomment the method below to handle the event raised after a feature has been installed.
        ///public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        ///{
        ///}
          
        /// Uncomment the method below to handle the event raised before a feature is uninstalled.
        ///public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        ///{
        ///}
          
        /// Uncomment the method below to handle the event raised when a feature is upgrading.
        ///public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        ///{
        ///}
    }
}
