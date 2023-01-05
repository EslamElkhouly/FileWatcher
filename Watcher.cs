using System;
using System.IO;

namespace FileWatcher
{
    internal class Watcher
    {

        public static void MonitorDirectory(string path, string filter)
        {
            FileSystemWatcher watcher = new FileSystemWatcher(path, filter);
            watcher.NotifyFilter = NotifyFilters.FileName
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.Size
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.DirectoryName;
            watcher.EnableRaisingEvents = true;
            watcher.Created += FileSystemWatcher_Created;
            watcher.Changed += FileSystemWatcher_Changed;
            watcher.Renamed += FileSystemWatcher_Renamed;
            watcher.Deleted += FileSystemWatcher_Deleted;
            watcher.Error += FileSystemWatcher_Error;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File created : {0} , File Path : {1} ", e.Name, e.FullPath);
        }

        private static void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File renamed : {0} , File Path : {1} ", e.Name, e.FullPath);

        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File deleted : {0} ", e.Name);
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {

            Console.WriteLine("File changed : {0} ,File Path : {1}", e.ChangeType, e.FullPath);
            ReadFromFile(e.FullPath);
        }

        private static void FileSystemWatcher_Error(object sender, ErrorEventArgs e)
        {
            PrintException(e.GetException());
        }

        private static void ReadFromFile(string path)
        {
            string text = null;
            using(var str=new StreamReader(path))
            {
               text= str.ReadToEnd();
            }
            Console.WriteLine(text);
            Console.WriteLine("End Of text");
        }
        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }

}