using SuperMarketSystem.Diagnostics;

namespace SuperMarketSystem.Tests
{
    /// <summary>
    /// Mock logger for testing purpose.
    /// </summary>
    /// <seealso cref="SuperMarketSystem.Diagnostics.ILogger" />
    public class MockLogger: ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        public void Log(string message, EventSeverity level)
        {
            this.Write(message);
        }

        /// <summary>
        /// Logs with the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="level">The level.</param>
        /// <param name="args">The arguments.</param>
        public void Log(string format, EventSeverity level, params object[] args)
        {
            var message = string.Format(format, args);
            this.Write(message);
        }

        /// <summary>
        /// Error level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            this.Write(message);
        }

        /// <summary>
        /// Errors the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Error(string format, params object[] args)
        {
            var message = string.Format(format, args);
            this.Write(message);
        }

        /// <summary>
        /// Information level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            this.Write(message);
        }

        /// <summary>
        /// Write the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Info(string format, params object[] args)
        {
            var message = string.Format(format, args);
            this.Write(message);
        }

        /// <summary>
        /// Warn level log the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            this.Write(message);
        }

        /// <summary>
        /// Warns with the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Warn(string format, params object[] args)
        {
            var message = string.Format(format, args);
            this.Write(message);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Write(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
