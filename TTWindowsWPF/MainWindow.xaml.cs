using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeTrackerLib.Models;
using TimeTrackerLib.Repositories;

namespace TTWindowsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _datasource = @"c:\temp\timetrack.db";
        public List<TTProject> _projectList;
        public MainWindow()
        {
            InitializeComponent();

            var rep = new SQLiteRepository(_datasource);

            _projectList = rep.GetProjects();

            DataContext = _projectList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((TTProject)lstProjects.SelectedItem).ProjectId.ToString());
        }
    }
}
