using System;
using DirectoryMonitor;

namespace DirectoryMonitorCommunication
{
    public class DirectoryMonitorCommands : IDirectoryMonitorCommands
    {
        public IDirectoryMonitor DirectoryMonitorRef;

        public string GetStatus()
        {
            return string.Format("OK");
        }

        public string GetDirectoryToWatch()
        {
            return DirectoryMonitorRef.DirectoryToWatch;
        }

        public string GetChangesToWatch()
        {
            return DirectoryMonitorRef.ChangesToWatch.ToString();
        }

        public string GetFiletypeToWatch()
        {
            return DirectoryMonitorRef.FiletypeToWatch;
        }

        public string GetShouldWatchSubdirectories()
        {
            return Convert.ToString(DirectoryMonitorRef.ShouldWatchSubdirectories);
        }
    }
}
