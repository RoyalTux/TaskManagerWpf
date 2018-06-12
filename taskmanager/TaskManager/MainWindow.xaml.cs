using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TaskManager
{
    public partial class MainWindow
    {
        string newProc;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            if (listBox.SelectedItems.Count > 0)
            {
                var viewModel = (ViewModel)DataContext;
                viewModel.SelectedProcess = ((ProcessListItem)listBox.SelectedItems[0]).Process;
            }
        }

        private void KillProcess_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var viewModel = (ViewModel)DataContext;
                viewModel.KillSelectedProcess();
            }
            catch (Exception)
            {
                MessageBox.Show("Нет прав/не выбран процесс!","Ошибка...");
            }

        }

        private void OpenProc()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "all(*.*)|*.*";
            if(dialog.ShowDialog()==true)
            {
                Process process = new Process();
                process.StartInfo.FileName = dialog.FileName;
                process.Start();
            }
        }

        private void OpenProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (newProc != null)
                {
                    Process.Start(newProc);
                    textbox.Clear();
                    newProc = null;
                }
                else OpenProc();
                    //MessageBox.Show("Напишите название процесса!");
                    

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка...");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            newProc = textbox.Text;
        }
    }
}
