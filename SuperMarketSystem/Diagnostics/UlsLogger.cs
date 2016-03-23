using Microsoft.SharePoint.Administration;

namespace SuperMarketSystem.Diagnostics
{
    /// <summary>
    /// The logger which writes to ULS log.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Diagnostics.ILogger" />
    public class UlsLogger : ILogger
    {
        /// <summary>
        /// The category.
        /// </summary>
        private const string Category = "Supermarket System";

        /// <summary>
        /// The diagnostic service.
        /// </summary>
        private SPDiagnosticsService diagnosticService = SPDiagnosticsService.Local;

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
                    this.Error(message);
                    break;
                case EventSeverity.Error:
                    this.Error(message);
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
        /// Error level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            this.Write(message, EventSeverity.Error, TraceSeverity.High);
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
        /// Warn level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            this.Write(message, EventSeverity.Warning, TraceSeverity.Medium);
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
                0,
                new SPDiagnosticsCategory(
                    UlsLogger.Category,
                    (Microsoft.SharePoint.Administration.TraceSeverity)level,
                    (Microsoft.SharePoint.Administration.EventSeverity)eventLevel),
                    (Microsoft.SharePoint.Administration.TraceSeverity)level,
                "Writing to the ULS log:  {0}",
                new object[] { message });
        }
    }
}
