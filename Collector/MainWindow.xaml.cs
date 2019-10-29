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

namespace Collector
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonPopupLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void HistoryMenuBotton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImprovementMenuBotton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FixMenuBotton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RatingMenuBotton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HomeMenuBotton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
