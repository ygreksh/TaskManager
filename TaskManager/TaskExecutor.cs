using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskExecutor : ITaskExecutor
    {
        //public int Amount { get; }
        private int maxThreads;
        private int nowThreads = 0;
        private bool isRunning;
        public LinkedList<Task> tasks = new LinkedList<Task>();

        public void Start(int maxConcurrent)
        {
            isRunning = true;
            
            maxThreads = maxConcurrent;
            while (nowThreads < maxThreads)
            {
                if (tasks.Count > 0)
                {
                    nowThreads++;
                    Task task;
                    task = tasks.First.Value;
                    tasks.RemoveFirst();
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        task.Start();
                        /*                    
                        while (true)
                        {
                            if (task.Status == TaskStatus.RanToCompletion)
                            {
                                nowThreads--;
                                break;
                            }
                        }
                        */
                    });
                    nowThreads--;
                }
                else
                {
                    isRunning = false;
                    break;
                }
            }
        }

        public void Stop()
        {
            
        }

        public void Add(Action action)
        {
            Task newTask = new Task(action);
            tasks.AddLast(newTask);
            if (!isRunning)
            {
                Start(maxThreads);
            }
        }

        public void Clear()
        {
            
        }
    }
}