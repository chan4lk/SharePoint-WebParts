namespace SuperMarketSystem.Diagnostics
{
    /// <summary>
    /// The severity of the trace.
    /// </summary>
    public enum TraceSeverity
    {
        /// <summary>
        /// The none.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents an unexpected code path and actions that should be monitored.
        /// </summary>
        Unexpected = 10,

        /// <summary>
        /// Represents an unusual code path and actions that should be monitored.
        /// </summary>
        Monitorable = 15,

        /// <summary>
        /// Writes high-level detail to the trace log file.
        /// </summary>
        High = 20,

        /// <summary>
        /// Writes medium-level detail to the trace log file.
        /// </summary>
        Medium = 50,

        /// <summary>
        /// Writes low-level detail to the trace log file.
        /// </summary>
        Verbose = 100,

        /// <summary>
        /// Writes low-level detail to the trace log file.
        /// </summary>
        VerboseEx = 200,
    }
}
