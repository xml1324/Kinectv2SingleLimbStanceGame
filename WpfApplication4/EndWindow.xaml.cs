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
using System.IO;

namespace WpfApplication4
{

    public partial class EndWindow : Window
    {

        public EndWindow()
        {
            InitializeComponent();

            BestTimeText.Text = "Best Time: " + Math.Round(SubWindow.bestTimeLeft,2) + "s (Left), " + Math.Round(SubWindow.bestTimeRight,2) + "s (Right)" ;
            BSText.Text = "Balance Score: " + Math.Round(SubWindow.finalbsLeft,2) + "% (Left), " + Math.Round(SubWindow.finalbsRight,2) + "% (Right)";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
