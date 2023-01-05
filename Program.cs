using System;
using System.IO;

namespace FileWatcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Monitoring");

            Watcher.MonitorDirectory(@"E:\Decoment\New folder (2)", "*.txt");

        }
    }
}
