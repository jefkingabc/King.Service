﻿namespace King.Azure.BackgroundWorker
{
    using System;

    /// <summary>
    /// Initialize Task
    /// </summary>
    public abstract class InitializeTask : TaskManager
    {
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InitializeTask()
            : base(7, TimeSpan.FromDays(88.5d).TotalSeconds)
        {
        }
        #endregion
    }
}