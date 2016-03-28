
//-----------------------------------------------------------------------
// <copyright company="SomeCompany" file="EventReciver.cs">
// Copyright � 2016
// </copyright>
// <auto-generated />
//-----------------------------------------------------------------------
using Microsoft.Practices.Unity;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SuperMarketSystem.Common;
using SuperMarketSystem.Diagnostics;
using SuperMarketSystem.Jobs;
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
        /// <summary>
        /// The logger.
        /// </summary>
        private ILogger logger;

        #region Methods - Public Memebrs - SPFeatureReciver Memebers

        /// <summary>
        /// Occurs after a Feature is activated.
        /// </summary>
        /// <param name="properties">An <see cref="T:Microsoft.SharePoint.SPFeatureReceiverProperties" /> object that represents the properties of the event.</param>
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;
            this.logger = ConfigurationManager.Container.Resolve<ILogger>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    //// Create Sales group and add current user to the group.
                    this.SetPermissions(site);

                    //// Create timer job to be run with the current site collection.
                    this.CreateJob(site);
                });
            }
            catch (Exception exception)
            {
                this.logger.Error("could not activate features");
                this.logger.Error(
                    "Message: {0}, Stack trace: {1}",
                    exception.Message,
                    exception.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Occurs when a Feature is deactivated.
        /// </summary>
        /// <param name="properties">An <see cref="T:Microsoft.SharePoint.SPFeatureReceiverProperties" /> object that represents the properties of the event.</param>
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            this.logger = ConfigurationManager.Container.Resolve<ILogger>();
            try
            {
                var site = properties.Feature.Parent as SPSite;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    //// Unset permission for web part.
                    this.UnsetPermissions(site);

                    //// Delete the time job.
                    this.DeleteJob(site);
                });
            }
            catch (Exception exception)
            {
                this.logger.Error("could not deactivate features");
                this.logger.Error(
                    "Message: {0}, Stack trace: {1}",
                    exception.Message,
                    exception.StackTrace);
                throw;
            }
        }

        #endregion

        #region Methods - Private Members - Helpers

        /// <summary>
        /// Creates the job.
        /// </summary>
        /// <param name="site">The site.</param>
        private void CreateJob(SPSite site)
        {
            this.DeleteJob(site);

            GenerateReportJob reportJob = new GenerateReportJob(
                GenerateReportJob.JobName,
                site.WebApplication);

            //// Schedule to run daily at 11 P.M.
            SPDailySchedule schedule = new SPDailySchedule();
            schedule.BeginHour = 23;
            schedule.EndHour = 23;
            schedule.EndMinute = 59;
            reportJob.Schedule = schedule;

            //// Add Site URL to run the job;
            reportJob.Properties.Add(
                GenerateReportJob.SiteURLKey, 
                SPContext.Current.Web.Url);

            reportJob.Update();
        }

        /// <summary>
        /// Deletes the job.
        /// </summary>
        /// <param name="site">The site.</param>
        private void DeleteJob(SPSite site)
        {
            foreach (SPJobDefinition job in site.WebApplication.JobDefinitions)
            {
                if (job.Name == GenerateReportJob.JobName)
                {
                    job.Delete();
                }
            }
        }

        /// <summary>
        /// Sets the permissions.
        /// </summary>
        /// <param name="site">
        /// The site.
        /// </param>
        private void SetPermissions(SPSite site)
        {
            using (SPWeb web = site.OpenWeb())
            {
                web.AllowUnsafeUpdates = true;
                bool groupExists = false;

                foreach (SPGroup group in web.SiteGroups)
                {
                    if (group.Name == ConfigurationManager.SalesGroup)
                    {
                        groupExists = true;
                        break;
                    }
                }

                if (groupExists)
                {
                    SPGroup sales = web.SiteGroups.GetByName(ConfigurationManager.SalesGroup);
                    SPUserCollection users = sales.Users;
                    bool isIntheGroup = false;
                    foreach (SPUser user in users)
                    {
                        if (user.LoginName == web.CurrentUser.LoginName)
                        {
                            isIntheGroup = true;
                            break;
                        }
                    }
                    if (!isIntheGroup)
                    {
                        sales.Users.Add(web.CurrentUser.LoginName, web.CurrentUser.Email, web.CurrentUser.Name, string.Empty);
                    }
                }
                else
                {
                    web.SiteGroups.Add(ConfigurationManager.SalesGroup, web.CurrentUser, web.CurrentUser, string.Empty);
                }

                web.Update();
                web.AllowUnsafeUpdates = false;
            }
        }

        /// <summary>
        /// Unsets the permissions.
        /// </summary>
        /// <param name="site">The site.</param>
        private void UnsetPermissions(SPSite site)
        {
            using (SPWeb web = site.OpenWeb())
            {
                web.AllowUnsafeUpdates = true;
                web.SiteGroups.Remove(ConfigurationManager.SalesGroup);
                web.Update();
                web.AllowUnsafeUpdates = false;
            }
        }

        #endregion
    }
}
