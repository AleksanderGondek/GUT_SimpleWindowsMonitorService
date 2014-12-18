using System.ServiceModel;
using System.ServiceModel.Web;

namespace DirectoryMonitor.Communication
{
    [ServiceContract]
    public interface IDirectoryMonitorCommands
    {
        [WebGet]
        [OperationContract]
        string GetStatus();
        [WebGet]
        [OperationContract]
        string GetDirectoriesToWatch();
        [WebGet]
        [OperationContract]
        string GetChangesToWatch();
        [WebGet]
        [OperationContract]
        string GetFiletypeToWatch();
        [WebGet]
        [OperationContract]
        string GetShouldWatchSubdirectories();

        [WebGet]
        [OperationContract]
        bool SetDirectoryToWatch(string directory);
        [WebGet]
        [OperationContract]
        bool SetChangesToWatch(string changes);
        [WebGet]
        [OperationContract]
        bool SetFiletypeToWatch(string filetype);
        [WebGet]
        [OperationContract]
        bool SetShouldWatchSubdirectories(string watchSubdirs);

        [WebGet]
        [OperationContract]
        bool Start();
        [WebGet]
        [OperationContract]
        bool Stop();
    }
}
