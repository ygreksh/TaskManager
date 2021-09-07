using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskExecutor : ITaskExecutor
    {
        public int Amount { get; }
        private List<Task> list = new List<Task>();
        public void Start(int maxConcurrent)
        {
            foreach (var task in list)
            {
                task.Start();
            }
        }

        public void Stop()
        {
            foreach (var task in list)
            {
                task.Wait();
            }
        }

        public void Add(Action action)
        {
            var newTask = new Task(action);
            list.Add(newTask);
            newTask.Start();
        }

        public void Clear()
        {
            this.Stop();
            list.Clear();
        }
    }
}