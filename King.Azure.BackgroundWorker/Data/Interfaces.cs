﻿namespace King.Azure.BackgroundWorker.Data
{
    using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Scheduled Task Core for Mocking
    /// </summary>
    public interface ICoordinator
    {
        #region Methods
        /// <summary>
        /// Initialize Table
        /// </summary>
        Manager InitializeTask();

        /// <summary>
        /// Determine whether a new task needs to be executed
        /// </summary>
        /// <param name="entry">Scheduled Task Entry</param>
        /// <returns>True if need to execute, false if not</returns>
        bool Check(Type type);

        /// <summary>
        /// Start Task
        /// </summary>
        /// <param name="type"></param>
        /// <param name="identifier"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        Task Start(Type type, Guid identifier, DateTime start);

        /// <summary>
        /// Complete Task
        /// </summary>
        /// <param name="type"></param>
        /// <param name="identifier"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        Task Complete(Type type, Guid identifier, DateTime start, DateTime end, bool success);
        #endregion

        #region Properties
        int PeriodInSeconds
        {
            get;
        }
        #endregion
    }

    /// <summary>
    /// Table Storage Interface
    /// </summary>
    public interface ITableStorage
    {
        #region Methods
        /// <summary>
        /// Create If Not Exists
        /// </summary>
        /// <returns></returns>
        Task<bool> CreateIfNotExists();

        /// <summary>
        /// Create Table
        /// </summary>
        /// <param name="tableName">Table Name</param>
        Task<bool> Create();

        /// <summary>
        /// Delete Table
        /// </summary>
        /// <param name="tableName"></param>
        Task Delete();

        /// <summary>
        /// Insert or update the record in table
        /// </summary>
        /// <param name="item">Scheduled Task Entry</param>
        Task<TableResult> InsertOrReplace(ITableEntity entry);

        /// <summary>
        /// Insert Batch
        /// </summary>
        /// <param name="entities"></param>
        Task<IEnumerable<TableResult>> Insert(IEnumerable<ITableEntity> entities);

        /// <summary>
        /// Query By Partition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partition"></param>
        /// <returns></returns>
        IEnumerable<T> QueryByPartition<T>(string partition)
            where T : ITableEntity, new();
        #endregion
    }
}