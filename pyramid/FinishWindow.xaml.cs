using System;
using System.Windows;

namespace pyramid
{
    public partial class FinishWindow : Window
    {
        private Action OnRestartClick;


        public FinishWindow()
        {
            InitializeComponent();
        }

        public void Initialize(GameState gameState, Action OnRestartClick)
        {
            this.OnRestartClick = OnRestartClick;

            switch (gameState)
            {
                case GameState.Sucess:
                    ShowResultPanel(true);
                    break;
                case GameState.Failure:
                    ShowResultPanel(false);
                    break;

                default: return;
            }
        }


        private void OnExitGameClick(object sender, RoutedEventArgs e)
        {
            GameHelper.CloseGame();
        }

        private void OnRestartGameClick(object sender, RoutedEventArgs e)
        {
            OnRestartClick?.Invoke();

            Close();
        }

        private void ShowResultPanel(bool isSucess)
        {
            PanelSuccess.Opacity = isSucess ? 1 : 0;
            PanelFailure.Opacity = isSucess ? 0 : 1;
        }
    }
}
