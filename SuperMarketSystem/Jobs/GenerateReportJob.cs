#region Imports
using Microsoft.Practices.Unity;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SuperMarketSystem.Common;
using SuperMarketSystem.Diagnostics;
using SuperMarketSystem.Models;
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
        /// <summary>
        /// The list name.
        /// </summary>
        public const string ListName = "Invoice";

        /// <summary>
        /// The job name.
        /// </summary>
        public const string JobName = "Generate Invoice Report Job";

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
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private ILogger Logger { get; set; }

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

        /// <summary>
        /// Executes the job definition.
        /// </summary>
        /// <param name="targetInstanceId">For target types of <see cref="T:Microsoft.SharePoint.Administration.SPContentDatabase" /> this is the database ID of the content database being processed by the running job. This value is GUID.Empty for all other target types.</param>
        public override void Execute(Guid targetInstanceId)
        {
            this.Logger = new UlsLogger();
            this.Logger.Info("Starting to execute");
            SPWebApplication webApp = this.Parent as SPWebApplication;
            SPSite site = webApp.Sites[0];
            try
            {
                IEnumerable<Invoice> invoices = this.GetInvoicesByDate(site, DateTime.Today);
                this.Logger.Info("Got invoices");
                if (invoices.Count() > 0)
                {
                    this.Logger.Info("Writing to CSV");
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
                    File.WriteAllText(@"C:\data\report.csv", csv.ToString());
                    this.Logger.Info("CSV written successfully.");
                }
            }
            catch (Exception exception)
            {
                this.Logger.Error("Could not generate report exception occurred.");
                this.Logger.Error(exception.Message);
                this.Logger.Error(exception.StackTrace);
                throw;
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

        /// <summary>
        /// Gets the invoices by date.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="date">The date.</param>
        /// <returns>
        /// All the invoices happened today.
        /// </returns>
        private IEnumerable<Invoice> GetInvoicesByDate(SPSite site, DateTime date)
        {
            List<Invoice> items = null;
            this.Logger.Error("trying to retrieve invoices");

            using (SPSite devSite = new SPSite("http://intranet.cloudapp.net/sites/dev"))
            {
                using (SPWeb web = devSite.OpenWeb())
                {
                    this.Logger.Info("Web is open" + web.Name);

                    SPList list = web.Lists[GenerateReportJob.ListName];

                    this.Logger.Info("list fetched" + list.Title);

                    SPQuery query = new SPQuery()
                    {
                        Query = @"<Query>
                                      <Where>
                                        <And>
                                          <Geq>
                                            <FieldRef Name='InvoiceDate' />
                                              <Value IncludeTimeValue='TRUE' Type='DateTime'>" + date + @"</Value>
                                          </Geq>
                                          <Leq>
                                            <FieldRef Name='InvoiceDate' />
                                            <Value IncludeTimeValue='TRUE' Type='DateTime'>" + date.AddDays(1) + @"</Value>
                                          </Leq>
                                        </And>
                                      </Where>
                                    </Query>",
                        ViewFields = string.Concat(
                            "<FieldRef Name='InvoiceDate' />",
                            "<FieldRef Name='ID' />",
                            "<FieldRef Name='Total' />")
                    };

                    SPListItemCollection invoices = list.GetItems(query);

                    this.Logger.Info("Invoices collected");

                    items = new List<Invoice>();

                    foreach (SPListItem invoice in invoices)
                    {
                        var item = new Invoice
                        {
                            Id = int.Parse(invoice[FieldInvoiceId].ToString()),
                            Total = decimal.Parse(invoice[FieldTotal].ToString()),
                            Date = (DateTime)invoice[FieldDate]
                        };
                        this.Logger.Info("Invoice ID: {0} Total: {1} Date: {2}", item.Id, item.Total, item.Date);
                        items.Add(item);
                    }
                }
            }

            return items;
        }
    }
}
