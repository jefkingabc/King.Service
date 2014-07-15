﻿namespace Worker
{
    using King.Azure.BackgroundWorker;
    using King.Azure.BackgroundWorker.Data;
    using System.Collections.Generic;
    using Worker.Queue;

    public class Factory : TaskFactory
    {
        public override IEnumerable<IRunnable> Tasks(object passthrough)
        {
            //Build a manifest of Tasks to run
            var tasks = new List<IRunnable>();

            // Initialization Task(s)
            tasks.Add(new InitTask());

            // Initialize Table; creates table if it doesn't already exist
            var table = new TableStorage("table", "UseDevelopmentStorage=true;");
            tasks.Add(new InitializeStorageTask(table));

            // Initialize Queue; creates queue if it doesn't already exist
            var queue = new StorageQueue("queue", "UseDevelopmentStorage=true;");
            tasks.Add(new InitializeStorageTask(queue));

            // Initialize Queue; creates queue if it doesn't already exist
            var container = new Container("container", "UseDevelopmentStorage=true;");
            tasks.Add(new InitializeStorageTask(container));

            //Task(s)
            tasks.Add(new Task());

            //Cordinated Tasks between Instances
            var task = new Coordinated();
            // Add once to ensure that Table is created for Instances to communicate with
            tasks.Add(task.InitializeTask());

            // Add your coordinated task(s)
            tasks.Add(task);
            
            //Backoff Tasks
            tasks.Add(new Backoff());

            //Dequeue Tasks
            tasks.Add(new BackoffRunner(new CompanyDequeuer()));

            return tasks;
        }
    }
}