using System.Threading.Tasks;

namespace SuperMarketSystem.Commands
{
    /// <summary>
    /// The command contract.
    /// </summary>
    public abstract class Command
    {
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
