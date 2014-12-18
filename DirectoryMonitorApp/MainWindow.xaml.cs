using System.Windows;
using DirectoryMonitorApp.Data;

namespace DirectoryMonitorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MonitoringSettings _monitoringSettings;
        public MainWindow()
        {
            InitializeComponent();
            _monitoringSettings = DataContext as MonitoringSettings;
            if (_monitoringSettings != null)
            {
                _monitoringSettings.GetData();
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _monitoringSettings.GetData();
        }
    }
}
