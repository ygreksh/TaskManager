using System;
using System.Threading.Tasks;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Task[] tasks1 = new Task[3]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };
            foreach (var t in tasks1)
                t.Start();
            Task.WaitAll(tasks1); // ожидаем завершения задач 
     
            Console.WriteLine("Завершение метода Main");
 
            Console.ReadLine();
        }
    }
}