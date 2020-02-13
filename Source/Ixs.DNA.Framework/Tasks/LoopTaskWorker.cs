using System;
using System.Threading.Tasks;
using static Ixs.DNA.FrameworkDI;

namespace Ixs.DNA
{
    /// <summary>
    /// <para>
    ///     Provides a thread-safe mechanism for starting and stopping the execution of an loop task
    ///     that can only have one instance of itself running regardless of the amount of times
    ///     start/stop is called and from what thread.
    /// </para>
    /// <para>
    ///     Supports cancellation requests via the <see cref="SingleTaskWorker.StopAsync"/> and the given task
    ///     will be provided with a cancellation token to monitor for when it should "stop"
    /// </para>
    /// </summary>
    public abstract class LoopTaskWorker : SingleTaskWorker
    {
        #region Protected Members

        /// <summary>
        /// Interval of the loop - in milliseconds
        /// </summary>
        public abstract int Interval { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoopTaskWorker() : base()
        {
        }

        #endregion

        #region Method Overrides

        /// <summary>
        /// Runs the worker task in separate thread without need to await the task
        /// It follows the original method with addition of the loop
        /// </summary>
        /// <returns>Returns once the worker task has completed</returns>
        protected new void RunWorkerTaskNoAwait()
        {
            // IMPORTANT: Not awaiting a Task leads to swallowed exceptions
            //            so we try/catch the entire task and report any unhandled
            //            exceptions to the log
            Task.Run(async () =>
            {
                // Log it
                Logger.LogTraceSource($"Worker loop started...");

                // Start the loop...
                while (!Stopping)
                {
                    try
                    {
                        // Log something
                        Logger.LogTraceSource($"Worker task started...");

                        // Run given task
                        await WorkerTaskAsync(mCancellationToken.Token);
                    }
                    // Swallow expected and normal task canceled exceptions
                    catch (TaskCanceledException) { }
                    catch (Exception ex)
                    {
                        // Unhandled exception
                        // Log it
                        Logger.LogCriticalSource($"Unhandled exception in loop task worker '{WorkerName}'. {ex}");
                    }
                    finally
                    {
                        // Log it
                        Logger.LogTraceSource($"Worker task finished");
                    }

                    // If we are not requesting cancelation...
                    if (!Stopping)
                        // Wait for the interval period
                        await Task.Delay(Interval);
                }

                // Log it
                Logger.LogTraceSource($"Worker loop finished");

                // Set finished event informing waiters we are finished working
                mWorkerFinishedEvent.Set();
            });
        }

        #endregion
    }
}
