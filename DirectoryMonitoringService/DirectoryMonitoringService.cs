using System;
using System.Linq;
using System.ServiceProcess;
using DirectoryMonitor;

namespace DirectoryMonitoringService
{
    public partial class DirectoryMonitoringService : ServiceBase
    {
        private readonly IDirectoryMonitor _directoryMonitor;

        public DirectoryMonitoringService()
        {
            InitializeComponent();
            _directoryMonitor = new DirectoryMonitor.DirectoryMonitor();
        }

        protected override void OnStart(string[] args)
        {
            if (args.Any())
            {
                _directoryMonitor.DirectoryToWatch = args.First();
                _directoryMonitor.FiletypeToWatch = args.ElementAt(1);
                _directoryMonitor.ShouldWatchSubdirectories = Convert.ToBoolean(args.ElementAt(2));
            }
            else
            {
                _directoryMonitor.DirectoryToWatch = @"C:\Users";
                _directoryMonitor.FiletypeToWatch = @"*.*";
                _directoryMonitor.ShouldWatchSubdirectories = true;
            }

            _directoryMonitor.Start();
        }

        protected override void OnStop()
        {
            _directoryMonitor.Stop();
        }
    }
}
