using System;
using System.IO;
using System.Security.Permissions;

namespace DirectoryMonitor
{
    public class DirectoryMonitor : IDirectoryMonitor
    {
        public string DirectoryToWatch { get { return _directoryToWatch; } set { _directoryToWatch = value; WatcherReset(); } }
        public NotifyFilters ChangesToWatch { get { return _changesToWatch; } set { _changesToWatch = value; WatcherReset(); } }
        public string FiletypeToWatch { get { return _filetypeToWatch; } set { _filetypeToWatch = value; WatcherReset(); } }

        private FileSystemWatcher _fileSystemWatcher;
        private string _directoryToWatch;
        private NotifyFilters _changesToWatch;
        private string _filetypeToWatch; 
        
        [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public void Start()
        {
            if (string.IsNullOrEmpty(DirectoryToWatch)) { return; }
            if (string.IsNullOrEmpty(FiletypeToWatch)) {  FiletypeToWatch = @"*.*";}

            SetUpWatcher();
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _fileSystemWatcher.Dispose();
            _fileSystemWatcher = null;
        }

        private void SetUpWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher
                                 {
                                     Path = DirectoryToWatch,
                                     NotifyFilter = ChangesToWatch,
                                     Filter = FiletypeToWatch
                                 };

            _fileSystemWatcher.Changed += OnChanged;
            _fileSystemWatcher.Created += OnCreated;
            _fileSystemWatcher.Deleted += OnDeleted;
            _fileSystemWatcher.Renamed += OnRenamed;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WatcherReset()
        {
            if (_fileSystemWatcher == null) return;
            Stop();
            Start();
        }
    }
}
