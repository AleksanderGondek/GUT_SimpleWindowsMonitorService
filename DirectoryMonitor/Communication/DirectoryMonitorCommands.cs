using System;
using DirectoryMonitor.Helpers;
using DirectoryMonitor.MonitorsManager;

namespace DirectoryMonitor.Communication
{
    public class DirectoryMonitorCommands : IDirectoryMonitorCommands
    {
        private readonly Manager _monitorManager = Manager.GetSingletonInstance();

        public string GetStatus()
        {
            return string.Format("OK");
        }

        public string GetDirectoriesToWatch()
        {
            return _monitorManager.GetWatchedDirectories();
        }

        public string GetChangesToWatch()
        {
            return _monitorManager.ChangesToWatch.ToString();
        }

        public string GetFiletypeToWatch()
        {
            return _monitorManager.FiletypeToWatch;
        }

        public string GetShouldWatchSubdirectories()
        {
            return Convert.ToString(_monitorManager.ShouldWatchSubdirectories);
        }

        public bool SetDirectoryToWatch(string directory)
        {
            try
            {
                return _monitorManager.SetWatchedDirectories(directory);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetChangesToWatch(string changes)
        {
            try
            {
                _monitorManager.ChangesToWatch = NotifyFiltersHelper.GetNotifyFiltersFromString(changes);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetFiletypeToWatch(string filetype)
        {
            try
            {
                _monitorManager.FiletypeToWatch = filetype;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetShouldWatchSubdirectories(string watchSubdirs)
        {
            try
            {
                _monitorManager.ShouldWatchSubdirectories = Convert.ToBoolean(watchSubdirs);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Start()
        {
            try
            {
                _monitorManager.Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Stop()
        {
            try
            {
                _monitorManager.Stop();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
