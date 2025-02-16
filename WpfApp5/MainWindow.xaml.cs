using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();   
            timer.Tick += Update_Tick;
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();

            UpdateProcesses(null, null);
        }

        private void EndProcess(object sender, RoutedEventArgs e)
        {
            if (ProcessList.SelectedItem is Processes selectedInfo)
            {
                try
                {
                    Process.GetProcessById(selectedInfo.ProcId).Kill();
                    UpdateProcesses(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите процесс!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            UpdateProcesses(null, null);
        }

        private void UpdateProcesses(object sender, RoutedEventArgs e)
        {
            int? selectedId = null;
            if (ProcessList.SelectedItem is Processes selectedProc)
            {
                selectedId = selectedProc.ProcId;
            }

            ProcessList.Items.Clear();

            var procs = Process.GetProcesses().OrderBy(p => p.ProcessName).ToArray();

            var procInfos = new List<Processes>();
            foreach (var p in procs)
            {
                procInfos.Add(new Processes
                {
                    ProcId = p.Id,
                    ProcName = p.ProcessName + ".exe",
                    RefProc = p
                });
            }

            foreach (var info in procInfos)
            {
                ProcessList.Items.Add(info);
            }

            if (selectedId.HasValue)
            {
                foreach (Processes info in ProcessList.Items)
                {
                    if (info.ProcId == selectedId.Value)
                    {
                        ProcessList.SelectedItem = info;
                        break;
                    }
                }
            }
        }

        private void RunProcess(object sender, RoutedEventArgs e)
        {
            string path = ProcessPath.Text.Trim();
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    Process.Start(path);
                    UpdateProcesses(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите путь!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
