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
        /// Logs with the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="level">The level.</param>
        /// <param name="args">The arguments.</param>
        void Log(string format, EventSeverity level, params object[] args);

        /// <summary>
        /// Error level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Err(string message);

        /// <summary>
        /// Errors the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void Err(string format, params object[] args);

        /// <summary>
        /// Information level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Information level log the specified message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void Info(string format, params object[] args);

        /// <summary>
        /// Warn level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(string message);

        /// <summary>
        /// Warns with the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void Warn(string format, params object[] args);
    }
}
