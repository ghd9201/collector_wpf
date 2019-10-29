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

        private void HistoryMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Collapsed;
            RateContents.Visibility = Visibility.Collapsed;
            FixContents.Visibility = Visibility.Collapsed;
            ImprovementContents.Visibility = Visibility.Collapsed;
            //HistoryContents.Visibility = Visibility.Visible;
        }

        private void ImprovementMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Collapsed;
            RateContents.Visibility = Visibility.Collapsed;
            FixContents.Visibility = Visibility.Collapsed;
            ImprovementContents.Visibility = Visibility.Visible;
            //HistoryContents.Visibility = Visibility.Collapsed;
        }

        private void FixMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Collapsed;
            RateContents.Visibility = Visibility.Collapsed;
            FixContents.Visibility = Visibility.Visible;
            ImprovementContents.Visibility = Visibility.Collapsed;
            //HistoryContents.Visibility = Visibility.Collapsed;
        }

        private void RatingMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Collapsed;
            RateContents.Visibility = Visibility.Visible;
            FixContents.Visibility = Visibility.Collapsed;
            ImprovementContents.Visibility = Visibility.Collapsed;
            //HistoryContents.Visibility = Visibility.Collapsed;
        }

        private void HomeMenuBotton_Click(object sender, RoutedEventArgs e)
        {
            HomeContents.Visibility = Visibility.Visible;
            RateContents.Visibility = Visibility.Collapsed;
            FixContents.Visibility = Visibility.Collapsed;
            ImprovementContents.Visibility = Visibility.Collapsed;
            //HistoryContents.Visibility = Visibility.Collapsed;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
