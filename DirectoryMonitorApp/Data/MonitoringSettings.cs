using System;
using DirectoryMonitorApp.DirectoryMonitorServiceClient;

namespace DirectoryMonitorApp.Data
{
    internal class MonitoringSettings
    {
        private DmsClient _dmsClient;

        private string _directoryToWatch;
        private string[] _changesToWatch;
        private string _fileTypeToWatch;
        private bool _shouldWatchSubDirs;

        public bool Status { get; private set; }

        public void GetData()
        {
            if (_dmsClient == null)
            {
                _dmsClient = new DmsClient();
            }

            Status = @"OK".Equals(_dmsClient.GetStatus());
            _directoryToWatch = _dmsClient.GetDirectoryToWatch();
            _changesToWatch = _dmsClient.GetChangesToWatch().Split(',');
            _fileTypeToWatch = _dmsClient.GetFiletypeToWatch();
            _shouldWatchSubDirs = Convert.ToBoolean(_dmsClient.GetShouldWatchSubdirectories());
        }


        public string DirectoryToWatch
        {
            get { return _directoryToWatch; }
            set
            {
                if (_dmsClient.SetDirectoryToWatch(value))
                {
                    _directoryToWatch = value;
                }
            }
        }

        public string[] ChangesToWatch
        {
            get { return _changesToWatch; }
            set
            {
                if (_dmsClient.SetChangesToWatch(string.Join(",",value)))
                {
                    _changesToWatch = value;
                }
            }
        }

        public string FileTypeToWatch
        {
            get { return _fileTypeToWatch; }
            set
            {
                if (_dmsClient.SetFiletypeToWatch(value))
                {
                    _fileTypeToWatch = value;
                }
            }
        }

        public bool ShouldWatchSubDirs
        {
            get { return _shouldWatchSubDirs; }
            set
            {
                if (_dmsClient.SetShouldWatchSubdirectories(Convert.ToString(value)))
                {
                    _shouldWatchSubDirs = value;
                }
            }
        }
    }
}
