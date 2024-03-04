using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pyramid
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Background = new ImageBrush(new BitmapImage(new Uri(ImagesPaths.MainWindowBackground)));
        }


        private void OnStartGameClick(object sender, RoutedEventArgs e)
        {
            new GameWindow().Show();

            Close();
        }

        private void OnShowRulesClick(object sender, RoutedEventArgs e)
        {
            new RulesWindow().Show();
        }

        private void OnExitGameClick(object sender, RoutedEventArgs e)
        {
            GameHelper.CloseGame();
        }
    }
}


















