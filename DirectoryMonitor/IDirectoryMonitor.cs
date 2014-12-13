using System.IO;

namespace DirectoryMonitor
{
    interface IDirectoryMonitor
    {
        string DirectoryToWatch { get; set; }
        NotifyFilters ChangesToWatch { get; set; }
        string FiletypeToWatch { get; set; }
        void Start();
        void Stop();
    }
}
