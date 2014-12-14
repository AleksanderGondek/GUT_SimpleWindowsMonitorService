using System;
using System.IO;
using DirectoryMonitor;

namespace MonitorConsoleRunner
{
    class Program
    {
        private static IDirectoryMonitor _monitor;

        static void Main(string[] args)
        {
            Console.WriteLine(@"Directory Monitor is starting up!");
            _monitor = new DirectoryMonitor.DirectoryMonitor
                       {
                           DirectoryToWatch = @"C:\Users\Alex\Desktop\Watch",
                           FiletypeToWatch = @"*.*",
                           ChangesToWatch =
                               NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                               NotifyFilters.DirectoryName,
                           ShouldWatchSubdirectories = true
                       };
            _monitor.Start();
            Console.WriteLine(@"Directory Monitor is watching!");
            Console.WriteLine(@"Press 'q' to quit");

            while (Console.Read() != 'q')
            {
            }

            _monitor.Stop();
        }
    }
}
