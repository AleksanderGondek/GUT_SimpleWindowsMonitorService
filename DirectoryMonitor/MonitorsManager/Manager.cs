using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DirectoryMonitor.Communication;

namespace DirectoryMonitor.MonitorsManager
{
    public class Manager : IDisposable
    {
        public NotifyFilters ChangesToWatch { get { return _changesToWatch; } set { _changesToWatch = value; AssignNewValues(); } }
        public string FiletypeToWatch { get { return _filetypeToWatch; } set { _filetypeToWatch = value; AssignNewValues(); } }
        public bool ShouldWatchSubdirectories { get { return _shouldWatchSubdirectories; } set { _shouldWatchSubdirectories = value; AssignNewValues(); } }

        private readonly Dictionary<string, IDirectoryMonitor> _monitors = new Dictionary<string, IDirectoryMonitor>();
        private readonly CommunicationHandler _communicationsHandler;

        private NotifyFilters _changesToWatch;
        private string _filetypeToWatch;
        private bool _shouldWatchSubdirectories;

        public Manager() 
        {
            if (_managerSingleton == null)
            {
                _managerSingleton = this;
            }

            _communicationsHandler = new CommunicationHandler();
            _communicationsHandler.Start();
        }

        public void Dispose()
        {
            _communicationsHandler.Dispose();
        }

        public string GetWatchedDirectories()
        {
            return string.Join(",", _monitors.Keys);
        }

        public bool SetWatchedDirectories(string directories)
        {
            try
            {
                foreach (var directory in directories.Split(',').Select(x => x.Trim()).ToList())
                {
                    if (!_monitors.ContainsKey(directory))
                    {
                        var newMonitor = new DirectoryMonitor
                                         {
                                             DirectoryToWatch = directory,
                                             FiletypeToWatch = FiletypeToWatch,
                                             ChangesToWatch = ChangesToWatch,
                                             ShouldWatchSubdirectories = ShouldWatchSubdirectories
                                         };
                        _monitors.Add(directory, newMonitor);
                        newMonitor.Start();
                    }
                }

                //TODO : Removing dirs

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Start()
        {
            foreach (var monitor in _monitors.Values)
            {
                monitor.Start();
            }
        }

        public void Stop()
        {
            foreach (var monitor in _monitors.Values)
            {
                monitor.Stop();
            }
        }

        private void AssignNewValues()
        {
            foreach (var monitor in _monitors)
            {
                monitor.Value.DirectoryToWatch = monitor.Key;
                monitor.Value.ChangesToWatch = ChangesToWatch;
                monitor.Value.FiletypeToWatch = FiletypeToWatch;
                monitor.Value.ShouldWatchSubdirectories = ShouldWatchSubdirectories;
            }
        }

        private static Manager _managerSingleton;
        public static Manager GetSingletonInstance()
        {
            return _managerSingleton as Manager;
        }
    }
}
