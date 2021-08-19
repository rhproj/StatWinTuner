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

namespace StatWinTuner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageBrush myBrush = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tuner tuner = new Tuner();

            tBlMessage.Text = tuner.ApplySettings();  //string.Join("\n", tuner.DisplayAppCfg());


            if (tuner.success)
            {
                //gridy.Background = Brushes.LightGreen;
                myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Img/success.png"));  //new Uri(@"/Img/success.png", UriKind.Absolute));
                gridy.Background = myBrush;
            }
            else
            {
                //gridy.Background = Brushes.LightSalmon;
                myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Img/failed.png"));      //@"/Img/error.jpg", UriKind.Absolute));
                gridy.Background = myBrush;
            }

            btnRun.Content = "Закрыть";
            btnRun.Click += (a, b) => this.Close();
        }
    }
}
