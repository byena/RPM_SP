using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("все запущенные процессы:");
        foreach (Process process in Process.GetProcesses())
        {
            Console.WriteLine($"id: {process.Id} name: {process.ProcessName}");
        }

        Console.WriteLine("потоки процесса rider:");
        Process[] riderProcs = Process.GetProcessesByName("rider64");
        if (riderProcs.Length > 0)
        {
            ProcessThreadCollection processThreads = riderProcs[0].Threads;
            foreach (ProcessThread thread in processThreads)
            {
                Console.WriteLine($"threadid: {thread.Id}");
            }
        }
        else
        {
            Console.WriteLine("процесс visual studio не найден.");
        }

        Console.WriteLine("запуск калькулятора и блокнота ");
        Process.Start("notepad.exe");
        Process.Start("calc.exe");
    }
}