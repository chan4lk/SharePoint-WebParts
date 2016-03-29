using System;

namespace SuperMarketSystem.Diagnostics
{
    /// <summary>
    /// Specifies the severity of events written to the Windows event log.
    /// </summary>
    public enum EventSeverity
    {
        /// <summary>
        /// Indicates no event entries are written.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that primary functionality of the server is not functioning and
        /// the application is unavailable to users. Manual intervention is required
        /// immediately.
        /// </summary>
        ErrorServiceUnavailable = 10,
        
        /// <summary>
        /// Indicates that a security compromise has occurred and systems on the network
        /// are at risk.
        /// </summary>
        ErrorSecurityBreach = 20,

        /// <summary>
        /// Indicates a problem state that needs the immediate attention of an administrator.
        /// </summary>
        ErrorCritical = 30,

        /// <summary>
        /// Indicates a problem state that needs attention by an administrator.
        /// </summary>
        Error = 40,

        /// <summary>
        ///  Indicates conditions that are not immediately significant but that may eventually
        ///  cause failure.
        /// </summary>
        Warning = 50,

        /// <summary>
        /// Indicates an audited access attempt has failed.
        /// </summary>
        FailureAudit = 60,

        /// <summary>
        /// Indicates an audited access attempt is successful.
        /// </summary>
        SuccessAudit = 70,

        /// <summary>
        ///  Contains noncritical information provided for the administrator.
        /// </summary>
        Information = 80,

        /// <summary>
        /// Indicates a successful operation.
        /// </summary>
        Success = 90,

        /// <summary>
        /// Indicates a successful operation.
        /// </summary>
        Verbose = 100,
    }
}
