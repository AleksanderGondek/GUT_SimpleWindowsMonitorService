using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using DirectoryMonitor.Communication;

namespace DirectoryMonitorApp.DirectoryMonitorServiceClient
{
    internal class DmsClient : IDisposable
    {
        private IDirectoryMonitorCommands _directoryMonitorChannel;
        public bool Connect()
        {
            try
            {
                var config = new ChannelFactory<IDirectoryMonitorCommands>(new WebHttpBinding(), "http://localhost:8000");
                config.Endpoint.Behaviors.Add(new WebHttpBehavior());
                _directoryMonitorChannel = config.CreateChannel();

                return @"OK".Equals(_directoryMonitorChannel.GetStatus());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetStatus()
        {
            try
            {
                return _directoryMonitorChannel.GetStatus();
            }
            catch (Exception)
            {
                return @"OFFLINE";
            }

        }
        public string GetDirectoriesToWatch()
        {
            return _directoryMonitorChannel.GetDirectoriesToWatch();
        }
        public string GetChangesToWatch()
        {
            return _directoryMonitorChannel.GetChangesToWatch();
        }
        public string GetFiletypeToWatch()
        {
            return _directoryMonitorChannel.GetFiletypeToWatch();
        }
        public string GetShouldWatchSubdirectories()
        {
            return _directoryMonitorChannel.GetShouldWatchSubdirectories();
        }
        public bool SetDirectoryToWatch(string directory)
        {
            return _directoryMonitorChannel.SetDirectoryToWatch(directory);
        }
        public bool SetChangesToWatch(string changes)
        {
            return _directoryMonitorChannel.SetChangesToWatch(changes);
        }
        public bool SetFiletypeToWatch(string filetype)
        {
            return _directoryMonitorChannel.SetFiletypeToWatch(filetype);
        }
        public bool SetShouldWatchSubdirectories(string watchSubdirs)
        {
            return _directoryMonitorChannel.SetShouldWatchSubdirectories(watchSubdirs);
        }

        public void Dispose()
        {
            _directoryMonitorChannel = null;
        }
    }
}
