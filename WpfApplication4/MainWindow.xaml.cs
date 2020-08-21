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

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            // 평균대 이미지 불러오기
            System.Drawing.Bitmap img = WpfApplication4.Properties.Resources.balancebeam;
            MemoryStream imgStream = new MemoryStream();
            img.Save(imgStream, System.Drawing.Imaging.ImageFormat.Bmp);
            imgStream.Seek(0, SeekOrigin.Begin);
            BitmapFrame newimg = BitmapFrame.Create(imgStream);
            BalanceBeam.Source = newimg;

            // 연구실 로고 이미지 불러오기
            System.Drawing.Bitmap img2 = WpfApplication4.Properties.Resources.HFE_logo;
            MemoryStream imgStream2 = new MemoryStream();
            img2.Save(imgStream2, System.Drawing.Imaging.ImageFormat.Bmp);
            imgStream2.Seek(0, SeekOrigin.Begin);
            BitmapFrame newimg2 = BitmapFrame.Create(imgStream2);
            Logo.Source = newimg2;

        }

        // 시작버튼을 누르면 게임화면이 열림.
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SubWindow subWindow = new SubWindow();
            this.Close();
            subWindow.Show();
        }

        private void StartButton_Click_1(object sender, RoutedEventArgs e)
        {
            SubWindow2 subWindow2 = new SubWindow2();
            this.Close();
            subWindow2.Show();
        }
    }

}
