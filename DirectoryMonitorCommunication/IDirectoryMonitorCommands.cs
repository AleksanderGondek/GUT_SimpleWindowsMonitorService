using System.ServiceModel;
using System.ServiceModel.Web;

namespace DirectoryMonitorCommunication
{
    [ServiceContract]
    public interface IDirectoryMonitorCommands
    {
        [WebGet()]
        [OperationContract]
        string GetStatus();
        [OperationContract]
        string GetDirectoryToWatch();
        [OperationContract]
        string GetChangesToWatch();
        [OperationContract]
        string GetFiletypeToWatch();
        [OperationContract]
        string GetShouldWatchSubdirectories();
    }
}
