using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace DirectoryMonitor.Communication
{
    internal class CommunicationHandler : IDisposable
    {
        private readonly WebServiceHost _host;
        public CommunicationHandler()
        {
            _host = new WebServiceHost(typeof(DirectoryMonitorCommands), new Uri("http://localhost:8000/"));
            _host.AddServiceEndpoint(typeof(IDirectoryMonitorCommands), new WebHttpBinding(), "");
        }

        public void Start()
        {
            _host.Open();
        }

        public void Stop()
        {
            _host.Close();
        }

        public void Dispose()
        {
            _host.Close();
        }
    }
}
