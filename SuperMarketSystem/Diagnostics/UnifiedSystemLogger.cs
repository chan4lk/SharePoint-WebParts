using Microsoft.SharePoint.Administration;
using System.Globalization;

namespace SuperMarketSystem.Diagnostics
{
    /// <summary>
    /// The logger which writes to ULS log.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Diagnostics.ILogger" />
    public class UnifiedSystemLogger : ILogger
    {
        /// <summary>
        /// The category.
        /// </summary>
        private static string category = SupermarketResources.Catelog;

        /// <summary>
        /// The diagnostic service.
        /// </summary>
        private SPDiagnosticsService diagnosticService = SPDiagnosticsService.Local;

        /// <summary>
        /// The identifier.
        /// </summary>
        private uint id = 0;

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        public void Log(string message, EventSeverity level)
        {
            switch (level)
            {
                case EventSeverity.ErrorCritical:
                    this.Err(message);
                    break;
                case EventSeverity.Error:
                    this.Err(message);
                    break;
                case EventSeverity.Warning:
                    this.Warn(message);
                    break;
                default:
                    this.Info(message);
                    break;
            }
        }

        /// <summary>
        /// Logs the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="level">The level.</param>
        /// <param name="args">The arguments.</param>
        public void Log(string format, EventSeverity level, params object[] args)
        {
            var message = string.Format(CultureInfo.CurrentCulture, format, args);

            this.Log(message, level);
        }

        /// <summary>
        /// Error level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Err(string message)
        {
            this.Write(message, EventSeverity.Error, TraceSeverity.High);
        }

        /// <summary>
        /// Errors the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Err(string format, params object[] args)
        {
            this.Err(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        /// Information level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            this.Write(message, EventSeverity.Verbose, TraceSeverity.Monitorable);
        }

        /// <summary>
        /// Information level log the specified message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Info(string format, params object[] args)
        {
            this.Info(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        /// Warn level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            this.Write(message, EventSeverity.Warning, TraceSeverity.Medium);
        }

        /// <summary>
        /// Warns with the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Warn(string format, params object[] args)
        {
            this.Warn(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="eventLevel">The event level.</param>
        /// <param name="level">The level.</param>
        private void Write(string message, EventSeverity eventLevel, TraceSeverity level)
        {
            this.diagnosticService.WriteTrace(
                this.id++,
                new SPDiagnosticsCategory(
                    UnifiedSystemLogger.category,
                    (Microsoft.SharePoint.Administration.TraceSeverity)level,
                    (Microsoft.SharePoint.Administration.EventSeverity)eventLevel),
                    (Microsoft.SharePoint.Administration.TraceSeverity)level,
                SupermarketResources.LogFormat,
                new object[] { message });
        }
    }
}
