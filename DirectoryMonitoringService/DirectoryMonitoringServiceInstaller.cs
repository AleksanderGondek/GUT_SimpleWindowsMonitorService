using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace DirectoryMonitoringService
{
    [RunInstaller(true)]
    public class DirectoryMonitoringServiceInstaller : Installer
    {
        public DirectoryMonitoringServiceInstaller()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.DisplayName = "DirectoryMonitoringService";
            serviceInstaller.StartType = ServiceStartMode.Manual;

            serviceInstaller.ServiceName = "DirectoryMonitoringService";

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
