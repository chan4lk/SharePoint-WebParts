namespace SuperMarketSystem.Diagnostics
{
    /// <summary>
    /// The logger contract.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        void Log(string message, EventSeverity level);

        /// <summary>
        /// Error level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        /// <summary>
        /// Information level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Warn level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(string message);
    }
}
