using System;
using System.Linq;
using System.ServiceProcess;
using DirectoryMonitor.MonitorsManager;

namespace DirectoryMonitoringService
{
    public partial class DirectoryMonitoringService : ServiceBase
    {
        private readonly Manager _directoryMonitor;

        public DirectoryMonitoringService()
        {
            InitializeComponent();
            _directoryMonitor = new Manager();
        }

        protected override void OnStart(string[] args)
        {
            if (args.Any())
            {
                _directoryMonitor.FiletypeToWatch = args.ElementAt(1);
                _directoryMonitor.ShouldWatchSubdirectories = Convert.ToBoolean(args.ElementAt(2));
                _directoryMonitor.SetWatchedDirectories(args.First());
            }
            else
            {
                _directoryMonitor.FiletypeToWatch = @"*.*";
                _directoryMonitor.ShouldWatchSubdirectories = true;
                _directoryMonitor.SetWatchedDirectories(@"C:\Users");
            }

            _directoryMonitor.Start();
        }

        protected override void OnStop()
        {
            _directoryMonitor.Stop();
        }
    }
}
