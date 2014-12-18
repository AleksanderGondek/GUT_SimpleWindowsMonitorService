using System.IO;
using System.Linq;

namespace DirectoryMonitor.Helpers
{
    public static class NotifyFiltersHelper
    {
        public static NotifyFilters GetNotifyFiltersFromString(string values)
        {
            var result = new NotifyFilters();
            if(string.IsNullOrEmpty(values)) return result;

            foreach (var value in values.Split(',').Select(x => x.Trim()).ToList())
            {
                switch(value)
                {
                    case @"Attributes":
                        result = result | NotifyFilters.Attributes;
                        break;
                    case @"CreationTime":
                        result = result | NotifyFilters.CreationTime;
                        break;
                    case @"DirectoryName":
                        result = result | NotifyFilters.DirectoryName;
                        break;
                    case @"FileName":
                        result = result | NotifyFilters.FileName;
                        break;
                    case @"LastAccess":
                        result = result | NotifyFilters.LastAccess;
                        break;
                    case @"LastWrite":
                        result = result | NotifyFilters.LastWrite;
                        break;
                    case @"Security":
                        result = result | NotifyFilters.Security;
                        break;
                    case @"Size":
                        result = result | NotifyFilters.Size;
                        break;
                }
            }

            return result;
        }
    }
}
