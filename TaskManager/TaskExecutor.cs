using System;

namespace TaskManager
{
    public class TaskExecutor : ITaskExecutor
    {
        public int Amount { get; }
        public void Start(int maxConcurrent)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Add(Action action)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}