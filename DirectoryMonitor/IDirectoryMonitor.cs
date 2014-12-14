using System.IO;

namespace DirectoryMonitor
{
    public interface IDirectoryMonitor
    {
        string DirectoryToWatch { get; set; }
        NotifyFilters ChangesToWatch { get; set; }
        string FiletypeToWatch { get; set; }
        bool ShouldWatchSubdirectories { get; set; }
        void Start();
        void Stop();
    }
}
