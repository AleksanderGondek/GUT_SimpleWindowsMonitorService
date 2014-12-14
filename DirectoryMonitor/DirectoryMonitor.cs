using System.IO;
using System.Security.Permissions;
using DirectoryMonitor.Communication;
using log4net;

namespace DirectoryMonitor
{
    public class DirectoryMonitor : IDirectoryMonitor
    {
        public string DirectoryToWatch { get { return _directoryToWatch; } set { _directoryToWatch = value; WatcherReset(); } }
        public NotifyFilters ChangesToWatch { get { return _changesToWatch; } set { _changesToWatch = value; WatcherReset(); } }
        public string FiletypeToWatch { get { return _filetypeToWatch; } set { _filetypeToWatch = value; WatcherReset(); } }
        public bool ShouldWatchSubdirectories { get { return _shouldWatchSubdirectories; } set { _shouldWatchSubdirectories = value; WatcherReset(); } }

        private FileSystemWatcher _fileSystemWatcher;
        private string _directoryToWatch;
        private NotifyFilters _changesToWatch;
        private string _filetypeToWatch;
        private bool _shouldWatchSubdirectories;

        private readonly ILog _logger;

        private readonly CommunicationHandler _communicationsHandler;

        public DirectoryMonitor()
        {
            if (_monitorSingleton == null) _monitorSingleton = this;
            _logger = LogManager.GetLogger(typeof(DirectoryMonitor));
            log4net.Config.XmlConfigurator.Configure();

            _communicationsHandler = new CommunicationHandler();
            _communicationsHandler.Start();

            _logger.Debug("DirectoryMonitor constructor called!");
        }

        public void Dispose()
        {
            if (_fileSystemWatcher != null)
            {
                _fileSystemWatcher.Dispose();
                _fileSystemWatcher = null;
            }

            _communicationsHandler.Dispose();
        }

        [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public void Start()
        {
            _logger.Debug("Directory Monitor Start method called");
            if (string.IsNullOrEmpty(DirectoryToWatch)) { return; }
            if (string.IsNullOrEmpty(FiletypeToWatch)) {  FiletypeToWatch = @"*.*";}

            SetUpWatcher();
            _fileSystemWatcher.EnableRaisingEvents = true;

            _logger.InfoFormat("Monitoring of directory {0} started", DirectoryToWatch);
        }

        public void Stop()
        {
            _logger.InfoFormat("Monitoring of directory {0} stopped", DirectoryToWatch);
            _fileSystemWatcher.Dispose();
            _fileSystemWatcher = null;
        }

        private void SetUpWatcher()
        {
            _logger.DebugFormat("Setting up FileSystemWatcher with params - Path {0}, NotifyFilter {1}, Filter {2}, IncludeSubdirectories {3}",
                DirectoryToWatch, ChangesToWatch, FiletypeToWatch, ShouldWatchSubdirectories);
            _fileSystemWatcher = new FileSystemWatcher
                                 {
                                     Path = DirectoryToWatch,
                                     NotifyFilter = ChangesToWatch,
                                     Filter = FiletypeToWatch,
                                     IncludeSubdirectories = ShouldWatchSubdirectories,
                                 };

            _logger.Debug("Assiging FileWatcher event listeners");
            _fileSystemWatcher.Changed += OnChanged;
            _fileSystemWatcher.Created += OnCreated;
            _fileSystemWatcher.Deleted += OnDeleted;
            _fileSystemWatcher.Renamed += OnRenamed;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            _logger.InfoFormat("File {0} (Path: {1}) was changed - type of change {2}",
                e.Name, e.FullPath, e.ChangeType);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _logger.InfoFormat("File {0} (Path: {1}) was created", e.Name, e.FullPath);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            _logger.InfoFormat("File {0} (Path: {1}) was deleted", e.Name, e.FullPath);
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            _logger.InfoFormat("File {0} (Path: {1}) was renamed - old name {2}, new name {3}",
                e.Name, e.FullPath, e.OldName, e.Name);
        }

        private void WatcherReset()
        {
            if (_fileSystemWatcher == null) return;
            _logger.InfoFormat("Monitoring of directory {0} restarted", DirectoryToWatch);
            
            Stop();
            Start();
        }

        private static DirectoryMonitor _monitorSingleton;
        public static IDirectoryMonitor GetSingletonInstance()
        {
            return _monitorSingleton as IDirectoryMonitor;
        }
    }
}
