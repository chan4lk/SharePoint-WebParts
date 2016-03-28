using Microsoft.Practices.Unity;
using SuperMarketSystem.Common;
using SuperMarketSystem.Diagnostics;
using System.Threading.Tasks;

namespace SuperMarketSystem.Commands
{
    /// <summary>
    /// The command contract.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command()
        {
            this.Logger = ConfigurationManager.Container.Resolve<ILogger>();
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns>
        /// The task.
        /// </returns>
        public abstract Task ExecuteAsync();
    }
}
