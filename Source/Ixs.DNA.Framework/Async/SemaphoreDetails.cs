﻿using System.Threading;

namespace Ixs.DNA.Async
{
    /// <summary>
    ///     Contains information about a semaphore lock
    /// </summary>
    internal class SemaphoreDetails
    {
        #region Public Properties

        /// <summary>
        ///     The semaphore for this item
        /// </summary>
        public SemaphoreSlim Semaphore { get; set; }

        /// <summary>
        ///     The unique key for this semaphore lock
        /// </summary>
        public string Key { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="key">The unique key for this semaphore</param>
        /// <param name="maxAccessCount">The maximum number of access counts to this semaphore before waiting</param>
        public SemaphoreDetails(string key, int maxAccessCount)
        {
            Key = key;
            Semaphore = new SemaphoreSlim(maxAccessCount, maxAccessCount);
        }

        #endregion
    }
}
