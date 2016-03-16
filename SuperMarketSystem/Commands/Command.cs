using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public abstract void ExecuteAsync();
    }
}
