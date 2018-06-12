using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace TaskManager
{
    public class ViewModel
    {
        public ViewModel()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += UpdateProcesses;
            timer.Start();
        }
        public Process SelectedProcess { get; set; }

        public ObservableCollection<ProcessListItem> Processes { get; } = new ObservableCollection<ProcessListItem>();

        public void UpdateProcesses(object sender, EventArgs e)
        {
            var currentIds = Processes.Select(p => p.Id).ToList();

            foreach (var p in Process.GetProcesses())
            {
                if (!currentIds.Remove(p.Id)) 
                {
                    Processes.Add(new ProcessListItem(p));
                }
            }

            foreach (var id in currentIds) 
            {
                var process = Processes.First(p => p.Id == id);
                if (process.KeepAlive)
                {
                    Process.Start(process.ProcessName, process.Arguments);
                }
                Processes.Remove(process);
            }
        }

        public void KillSelectedProcess()
        {
            SelectedProcess.Kill();
        }
    }
}
