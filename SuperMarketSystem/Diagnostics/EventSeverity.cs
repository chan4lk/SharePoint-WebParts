using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Obsolete("Use EventSeverity.ErrorCritical")]
        ErrorServiceUnavailable = 10,
        
        /// <summary>
        /// Indicates that a security compromise has occurred and systems on the network
        /// are at risk.
        /// </summary>
        [Obsolete("Use EventSeverity.ErrorCritical")]
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
        [Obsolete("Use EventSeverity.Warning")]
        FailureAudit = 60,

        /// <summary>
        /// Indicates an audited access attempt is successful.
        /// </summary>
        [Obsolete("Use EventSeverity.Information")]
        SuccessAudit = 70,

        /// <summary>
        ///  Contains noncritical information provided for the administrator.
        /// </summary>
        Information = 80,

        /// <summary>
        /// Indicates a successful operation.
        /// </summary>
        [Obsolete("Use EventSeverity.Verbose")]
        Success = 90,

        /// <summary>
        /// Indicates a successful operation.
        /// </summary>
        Verbose = 100,
    }
}
