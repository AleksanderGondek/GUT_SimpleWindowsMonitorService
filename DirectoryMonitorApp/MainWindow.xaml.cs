using System.Linq;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result.ToString().Equals(@"OK"))
            {
                _monitoringSettings.DirectoriesToWatch.Add(dialog.SelectedPath);
                _monitoringSettings.DirectoriesToWatch = _monitoringSettings.DirectoriesToWatch.Select(item => (string)item.Clone()).ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedDir = ComboBoxDirs.SelectedValue.ToString();
            _monitoringSettings.DirectoriesToWatch.Remove(selectedDir);
            _monitoringSettings.DirectoriesToWatch = _monitoringSettings.DirectoriesToWatch.Select(item => (string)item.Clone()).ToList();
        }
    }
}
