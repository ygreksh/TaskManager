using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskExecutor : ITaskExecutor
    {
        //public int Amount { get; }
        private int maxCountThreads;
        private int tasksNowCounter = 0;
        
        // Флаг занятости потока задачами true - выполняется задача, false - поток свободен.
        private static bool threadIsRunning;
        
        private LinkedList<Task> tasks = new LinkedList<Task>();

        public TaskExecutor(int maxCountThreads)
        {
            this.maxCountThreads = maxCountThreads;
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                // Note that the current thread is now processing work items.
                // This is necessary to enable inlining of tasks into this thread.
                threadIsRunning = true;
                try
                {
                    // Process all available items in the queue.
                    while (true)
                    {
                        Task item;
                        lock (tasks)
                        {
                            if (tasks.Count == 0)
                            {
                                --tasksNowCounter;
                                break;
                            }

                            item = tasks.First.Value;
                            tasks.RemoveFirst();
                        }

                        // Execute the task we pulled out of the queue
                        item.Wait();
                    }
                }
                // We're done processing items on the current thread
                finally { threadIsRunning = false; }
            }, null);
        }

        public void Stop()
        {
            foreach (var task in tasks)
            {
                task.Wait();
            }
        }

        public void Add(Action action)
        {
            lock (tasks)
            {
                var newTask = new Task(action);
                tasks.AddLast(newTask);
                //newTask.Start();
                if (tasksNowCounter < maxCountThreads)
                {
                    ++tasksNowCounter;
                    Start();
                }
            }
        }

        public void Clear()
        {
            this.Stop();
            tasks.Clear();
        }
    }
}