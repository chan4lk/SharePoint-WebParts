#region Imports
using Microsoft.Practices.Unity;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SuperMarketSystem.Common;
using SuperMarketSystem.Diagnostics;
using SuperMarketSystem.Models;
using SuperMarketSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#endregion

namespace SuperMarketSystem.Jobs
{
    /// <summary>
    /// Generates a report of all the invoices.
    /// </summary>
    /// <seealso cref="Microsoft.SharePoint.Administration.SPJobDefinition" />
    public class GenerateReportJob : SPJobDefinition
    {
        #region Constants

        #region Constants - Public Members
        /// <summary>
        /// The list name.
        /// </summary>
        public const string ListName = "Invoice";

        /// <summary>
        /// The site URL key.
        /// </summary>
        public const string SiteUrlKey = "SPSiteURL";

        /// <summary>
        /// The job name.
        /// </summary>
        public const string JobName = "Generate Invoice Report Job"; 
        #endregion

        #region Constants - Private Members
        /// <summary>
        /// The field invoice identifier.
        /// </summary>
        private const string FieldInvoiceId = "ID";

        /// <summary>
        /// The field date.
        /// </summary>
        private const string FieldDate = "InvoiceDate";

        /// <summary>
        /// The field total.
        /// </summary>
        private const string FieldTotal = "Total";

        /// <summary>
        /// The file path.
        /// </summary>
        private const string FilePath = @"C:\data\report.csv"; 

        #endregion

        #endregion

        #region Feilds - Private Members

        /// <summary>
        /// The site URL.
        /// </summary>
        private string siteURL;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private ILogger logger;

        #endregion

        #region Methods - Public Member - Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateReportJob"/> class.
        /// </summary>
        public GenerateReportJob()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateReportJob"/> class.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="service">The service.</param>
        public GenerateReportJob(string jobName, SPService service)
            : base(jobName, service, null, SPJobLockType.None)
        {
            this.Title = GenerateReportJob.JobName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateReportJob"/> class.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="web">The web.</param>
        public GenerateReportJob(string jobName, SPWebApplication web)
            : base(jobName, web, null, SPJobLockType.ContentDatabase)
        {
            this.Title = GenerateReportJob.JobName;
        }

        #endregion

        #region Methods - JobDefinition Members
        /// <summary>
        /// Executes the job definition.
        /// </summary>
        /// <param name="targetInstanceId">For target types of <see cref="T:Microsoft.SharePoint.Administration.SPContentDatabase" /> this is the database ID of the content database being processed by the running job. This value is GUID.Empty for all other target types.</param>
        public override void Execute(Guid targetInstanceId)
        {
            this.logger = new UlsLogger();
            this.logger.Info("Starting to execute");

            if (this.Properties.ContainsKey(GenerateReportJob.SiteUrlKey))
            {
                this.siteURL = this.Properties[SiteUrlKey].ToString();

                if (!string.IsNullOrEmpty(this.siteURL))
                {
                    this.WriteToFile();
                }
            }
        }

        /// <summary>
        /// Determines whether [has additional update access].
        /// </summary>
        /// <returns>
        /// <c>true</c> to prevent exception.
        /// </returns>
        protected override bool HasAdditionalUpdateAccess()
        {
            return true;
        } 
        #endregion

        #region Methods - Helpers

        /// <summary>
        /// Writes to file.
        /// </summary>
        private void WriteToFile()
        {
            try
            {
                IEnumerable<Invoice> invoices = this.GetInvoicesByDate(DateTime.Today);
                this.logger.Info("Got invoices");
                if (invoices.Count() > 0)
                {
                    this.logger.Info("Writing to CSV");
                    var csv = new StringBuilder();

                    //// Add data rows
                    foreach (var item in invoices)
                    {
                        csv.AppendFormat("{0}, {1}", item.Id, item.Total);
                        csv.AppendLine();
                    }

                    //// Add Total to the footer
                    csv.AppendFormat("Total , {0}", invoices.Sum(i => i.Total));
                    csv.AppendLine();

                    //// Write to disk.
                    File.WriteAllText(GenerateReportJob.FilePath, csv.ToString());
                    this.logger.Info("CSV written successfully.");
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("Could not generate report exception occurred.");
                this.logger.Error(exception.Message);
                this.logger.Error(exception.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Gets the invoices by date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// All the invoices happened today.
        /// </returns>
        private IEnumerable<Invoice> GetInvoicesByDate(DateTime date)
        {
            IEnumerable<Invoice> items = null;

            InvoiceRepository repository = new InvoiceRepository(this.siteURL);

            items = repository.GetByDate(date);

            return items;
        } 
        #endregion
    }
}
