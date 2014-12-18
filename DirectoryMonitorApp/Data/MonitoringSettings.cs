using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DirectoryMonitorApp.DirectoryMonitorServiceClient;

namespace DirectoryMonitorApp.Data
{
    internal class MonitoringSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DmsClient _dmsClient;

        private bool _status;
        private string _directoryToWatch;
        private IList<string> _changesToWatch;
        private string _fileTypeToWatch;
        private bool _shouldWatchSubDirs;

        private bool _monitorAttributes;
        private bool _monitorCreationTime;
        private bool _monitorDirectoryName;
        private bool _monitorFileName;
        private bool _monitorLastAccess;
        private bool _monitorLastWrite;
        private bool _monitorSecurity;
        private bool _monitorSize;

        public void GetData()
        {
            if (_dmsClient == null)
            {
                _dmsClient = new DmsClient();
                _dmsClient.Connect();
            }

            Status = @"OK".Equals(_dmsClient.GetStatus());
            OnPropertyChanged("Status");
            if (!Status) return;

            _directoryToWatch = _dmsClient.GetDirectoryToWatch();
            OnPropertyChanged("DirectoryToWatch");
            _changesToWatch = _dmsClient.GetChangesToWatch().Split(',').Select(x => x.Trim()).ToList();
            OnPropertyChanged("ChangesToWatch");
            SetUpMonitorFlags();
            _fileTypeToWatch = _dmsClient.GetFiletypeToWatch();
            OnPropertyChanged("FileTypeToWatch");
            _shouldWatchSubDirs = Convert.ToBoolean(_dmsClient.GetShouldWatchSubdirectories());
            OnPropertyChanged("ShouldWatchSubDirs");
        }


        public bool Status
        {
            get { return _status; }
            set
            {
                OnPropertyChanged("Status");
                _status = value;
            }
        }

        public string DirectoryToWatch
        {
            get { return _directoryToWatch; }
            set
            {
                if (_dmsClient.SetDirectoryToWatch(value))
                {
                    OnPropertyChanged("DirectoryToWatch");
                    _directoryToWatch = value;
                }
            }
        }

        public IList<string> ChangesToWatch
        {
            get { return _changesToWatch; }
            set
            {
                if (_dmsClient.SetChangesToWatch(string.Join(",",value)))
                {
                    OnPropertyChanged("ChangesToWatch");
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
                    OnPropertyChanged("FileTypeToWatch");
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
                    OnPropertyChanged("ShouldWatchSubDirs");
                    _shouldWatchSubDirs = value;
                }
            }
        }

        public bool MonitorAttributes
        {
            get { return _monitorAttributes; }
            set
            {
                _monitorAttributes = value;
                OnPropertyChanged("MonitorAttributes");
                if (value && !ChangesToWatch.Contains(@"Attributes"))
                {
                    ChangesToWatch.Add(@"Attributes");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
                else if(!value && ChangesToWatch.Contains(@"Attributes"))
                {
                    ChangesToWatch.Remove(@"Attributes");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
            }
        }

        public bool MonitorCreationTime
        {
            get { return _monitorCreationTime; }
            set
            {
                _monitorCreationTime = value;
                OnPropertyChanged("MonitorCreationTime");
                if (value && !ChangesToWatch.Contains(@"CreationTime"))
                {
                    ChangesToWatch.Add(@"CreationTime");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
                else if(!value && ChangesToWatch.Contains(@"CreationTime"))
                {
                    ChangesToWatch.Remove(@"CreationTime");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
            }
        }
        public bool MonitorDirectoryName
        {
            get { return _monitorDirectoryName; }
            set
            {
                _monitorDirectoryName = value;
                OnPropertyChanged("MonitorDirectoryName");
                if (value && !ChangesToWatch.Contains(@"DirectoryName"))
                {
                    ChangesToWatch.Add(@"DirectoryName");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
                else if(!value && ChangesToWatch.Contains(@"DirectoryName"))
                {
                    ChangesToWatch.Remove(@"DirectoryName");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
            }
        }
        public bool MonitorFileName
        {
            get { return _monitorFileName; }
            set
            {
                _monitorFileName = value;
                OnPropertyChanged("MonitorFileName");
                if (value && !ChangesToWatch.Contains(@"FileName"))
                {
                    ChangesToWatch.Add(@"FileName");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
                else if(!value && ChangesToWatch.Contains(@"FileName"))
                {
                    ChangesToWatch.Remove(@"FileName");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
            }
        }
        public bool MonitorLastAccess
        {
            get { return _monitorLastAccess; }
            set
            {
                _monitorLastAccess = value;
                OnPropertyChanged("MonitorLastAccess");
                if (value && !ChangesToWatch.Contains(@"LastAccess"))
                {
                    ChangesToWatch.Add(@"LastAccess");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
                else if(!value && ChangesToWatch.Contains(@"LastAccess"))
                {
                    ChangesToWatch.Remove(@"LastAccess");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
            }
        }
        public bool MonitorLastWrite
        {
            get { return _monitorLastWrite; }
            set
            {
                _monitorLastWrite = value;
                OnPropertyChanged("MonitorLastWrite");
                if (value && !ChangesToWatch.Contains(@"LastWrite"))
                {
                    ChangesToWatch.Add(@"LastWrite");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
                else if(!value && ChangesToWatch.Contains(@"LastWrite"))
                {
                    ChangesToWatch.Remove(@"LastWrite");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
            }
        }
        public bool MonitorSecurity
        {
            get { return _monitorSecurity; }
            set
            {
                _monitorSecurity = value;
                OnPropertyChanged("MonitorSecurity");
                if (value && !ChangesToWatch.Contains(@"Security"))
                {
                    ChangesToWatch.Add(@"Security");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
                else if(!value && ChangesToWatch.Contains(@"Security"))
                {
                    ChangesToWatch.Remove(@"Security");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
            }
        }
        public bool MonitorSize
        {
            get { return _monitorSize; }
            set
            {
                _monitorSize = value;
                OnPropertyChanged("MonitorSize");
                if (value && !ChangesToWatch.Contains(@"Size"))
                {
                    ChangesToWatch.Add(@"Size");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
                else if(!value && ChangesToWatch.Contains(@"Size"))
                {
                    ChangesToWatch.Remove(@"Size");
                    ChangesToWatch = ChangesToWatch.Select(item => (string)item.Clone()).ToList();
                }
            }
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void SetUpMonitorFlags()
        {
            foreach (var filter in ChangesToWatch)
            {
                switch (filter)
                {
                    case @"Attributes":
                        MonitorAttributes = true;
                        break;
                    case @"CreationTime":
                        MonitorCreationTime = true;
                        break;
                    case @"DirectoryName":
                        MonitorDirectoryName = true;
                        break;
                    case @"FileName":
                        MonitorFileName = true;
                        break;
                    case @"LastAccess":
                        MonitorLastAccess = true;
                        break;
                    case @"LastWrite":
                        MonitorLastWrite = true;
                        break;
                    case @"Security":
                        MonitorSecurity = true;
                        break;
                    case @"Size":
                        MonitorSize = true;
                        break;
                }
            }
        }
    }
}
