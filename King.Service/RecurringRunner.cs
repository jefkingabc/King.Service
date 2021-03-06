﻿namespace King.Service
{
    using System.Threading.Tasks;

    /// <summary>
    /// Recurring Runner
    /// </summary>
    public class RecurringRunner : RecurringTask
    {
        #region Members
        /// <summary>
        /// Runs
        /// </summary>
        protected readonly IRuns run = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="run">Run</param>
        public RecurringRunner(IRuns run)
            : base(run.MinimumPeriodInSeconds)
        {
            this.run = run;

            base.Name = string.Format("{0}+{1}", this.GetType(), this.run.GetType());
        }
        #endregion

        #region Methods
        /// <summary>
        /// Run
        /// </summary>
        public override void Run()
        {
            var t = this.run.Run();

            Task.WaitAll(t);
        }
        #endregion
    }
}