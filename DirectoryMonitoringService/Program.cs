using System.ServiceProcess;

namespace DirectoryMonitoringService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[] 
                                          { 
                                              new DirectoryMonitoringService()
                                          };
            ServiceBase.Run(servicesToRun);
        }
    }
}
