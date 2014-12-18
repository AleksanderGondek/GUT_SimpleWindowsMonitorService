using System;
using System.IO;
using DirectoryMonitor.MonitorsManager;

namespace MonitorConsoleRunner
{
    class Program
    {
        private static Manager _monitor;

        static void Main(string[] args)
        {
            Console.WriteLine(@"Directory Monitor is starting up!");
            _monitor = new Manager
                       {
                           FiletypeToWatch = @"*.*",
                           ChangesToWatch =
                               NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                               NotifyFilters.DirectoryName,
                           ShouldWatchSubdirectories = true
                       };
            _monitor.SetWatchedDirectories(@"C:\Users\Alex\Desktop\Watch");
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
