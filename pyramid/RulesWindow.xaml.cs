using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pyramid
{
    public partial class RulesWindow : Window
    {
        public RulesWindow()
        {
            InitializeComponent();

            Background = new ImageBrush(new BitmapImage(new Uri(ImagesPaths.RulesWindowBackground)));
        }


        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
