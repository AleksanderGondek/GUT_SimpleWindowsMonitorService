using System;
using DirectoryMonitor.Helpers;

namespace DirectoryMonitor.Communication
{
    public class DirectoryMonitorCommands : IDirectoryMonitorCommands
    {
        private readonly IDirectoryMonitor _directoryMonitorRef = DirectoryMonitor.GetSingletonInstance();

        public string GetStatus()
        {
            return string.Format("OK");
        }

        public string GetDirectoryToWatch()
        {
            return _directoryMonitorRef.DirectoryToWatch;
        }

        public string GetChangesToWatch()
        {
            return _directoryMonitorRef.ChangesToWatch.ToString();
        }

        public string GetFiletypeToWatch()
        {
            return _directoryMonitorRef.FiletypeToWatch;
        }

        public string GetShouldWatchSubdirectories()
        {
            return Convert.ToString(_directoryMonitorRef.ShouldWatchSubdirectories);
        }

        public bool SetDirectoryToWatch(string directory)
        {
            try
            {
                _directoryMonitorRef.DirectoryToWatch = directory;
                return true;
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
                _directoryMonitorRef.ChangesToWatch = NotifyFiltersHelper.GetNotifyFiltersFromString(changes);
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
                _directoryMonitorRef.FiletypeToWatch = filetype;
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
                _directoryMonitorRef.ShouldWatchSubdirectories = Convert.ToBoolean(watchSubdirs);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
