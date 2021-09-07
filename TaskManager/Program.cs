using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            /*
            Task[] tasks1 = new Task[]
            {
                new Task(() => Console.WriteLine("Task 1")),
                new Task(() => Console.WriteLine("Task 2")),
                new Task(() => Console.WriteLine("Task 3")),
                new Task(() => Console.WriteLine("Task 4"))
            };
            foreach (var t in tasks1)
                t.Start();
            Task.WaitAll(tasks1); // ожидаем завершения задач 
            */
            TaskExecutor taskExecutor = new TaskExecutor();
            taskExecutor.Start(2);
            for (int i = 1; i <= 10; i++)
            {
                taskExecutor.Add(new Action(() =>
                {
                    int count = i;
                    Random random = new Random();
                    Console.WriteLine($"Работа {count} в потоке {Thread.CurrentThread.GetHashCode()} началась");
                    Thread.Sleep(random.Next(0,2000));
                    Console.WriteLine($"Работа {count} в потоке {Thread.CurrentThread.GetHashCode()} завершилась");
                    Thread.Sleep(random.Next(0,2000));
                }));
            }

            Task.WaitAll(taskExecutor.tasks.ToArray());

            //Task.WaitAll(taskExecutor.list);
            Console.WriteLine("Завершение метода Main");
 
            //Console.ReadKey();
        }
    }
}