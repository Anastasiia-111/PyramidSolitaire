using System.Windows;

namespace pyramid
{
    public static class GameHelper
    {
        public static void CloseGame()
        {
            Application.Current.Shutdown();
        }
    }
}
