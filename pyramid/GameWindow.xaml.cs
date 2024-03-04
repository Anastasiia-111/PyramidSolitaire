using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace pyramid
{
    public partial class GameWindow : Window
    {
        private PyramidGameCore pyramidCore;
        private List<CardView> cardsView;

        private const char PYRAMID_KEY = 'p';


        public GameWindow()
        {
            InitializeComponent();

            InitializeBaseUI();
            InitializeGameState();
        }

        private void InitializeBaseUI()
        {
            cardsView = new List<CardView>
            {
                new(p_0_0),
                new(p_1_0), new(p_1_1),
                new(p_2_0), new(p_2_1), new(p_2_2),
                new(p_3_0), new(p_3_1), new(p_3_2), new(p_3_3),
                new(p_4_0), new(p_4_1), new(p_4_2), new(p_4_3), new(p_4_4),
                new(p_5_0), new(p_5_1), new(p_5_2), new(p_5_3), new(p_5_4), new(p_5_5),
                new(p_6_0), new(p_6_1), new(p_6_2), new(p_6_3), new(p_6_4), new(p_6_5), new(p_6_6),
            };

            Background = new ImageBrush(new BitmapImage(new Uri(ImagesPaths.GameWindowBackground)));
            RestartGameButton.Content = new Image { Source = new BitmapImage(new Uri(ImagesPaths.RestartButtonIcon)) };
            MoveSpareDeckLeftButton.Content = new Image { Source = new BitmapImage(new Uri(ImagesPaths.RepeatButtonIcon)) };
        }

        private void InitializeGameState()
        {
            pyramidCore = new PyramidGameCore();

            UpdatePyramidCardsView();
            UpdateSpareDeckView();

            DoSubscribe();
        }


        private void DoSubscribe()
        {
            pyramidCore.OnFullGameUpdate += OnFullGameUpdate;
            pyramidCore.OnSpareDeckUpdate += OnSpareDeckUpdate;
            pyramidCore.OnPyramidUpdate += OnPyramidUpdate;
            pyramidCore.OnGameOver += OnGameOverState;
        }

        private void DoUnsubscribe()
        {
            pyramidCore.OnFullGameUpdate -= OnFullGameUpdate;
            pyramidCore.OnPyramidUpdate -= OnPyramidUpdate;
            pyramidCore.OnSpareDeckUpdate -= OnSpareDeckUpdate;
            pyramidCore.OnGameOver -= OnGameOverState;
        }

        private void OnFullGameUpdate()
        {
            OnPyramidUpdate();
            OnSpareDeckUpdate();
        }

        private void OnPyramidUpdate()
        {
            UpdatePyramidCardsView();
        }

        private void OnSpareDeckUpdate()
        {
            UpdateSpareDeckView();
        }

        private void OnGameOverState()
        {
            FinishWindow finishWindow = new FinishWindow();
            finishWindow.Initialize(pyramidCore.GameState, OnRestartClickAction);
            finishWindow.Show();

            Hide();

            void OnRestartClickAction()
            {
                Show();
                pyramidCore.RestartGame();
            }
        }


        private void OnCardClick(object sender, MouseEventArgs e)
        {
            if (sender is not Image image)
                return;

            string[] nameParams = image.Name.Split('_');

            string cardKey = nameParams[0];
            if (cardKey.Length != 1 || cardKey[0] != PYRAMID_KEY)
                return;

            int indexX = Convert.ToInt32(nameParams[2]);
            int indexY = Convert.ToInt32(nameParams[1]);

            pyramidCore.SelectPyramidCard(new SelectionParams(false, indexX, indexY));
        }

        private void OnMoveOpenedSpareCardsToClosed(object sender, RoutedEventArgs e)
        {
            pyramidCore.MoveOpenedSpareCardsToClosed();
        }

        private void OnClosedSpareDeckClick(object sender, RoutedEventArgs e)
        {
            pyramidCore.OpenLastClosedSpareCard();
        }

        private void OnOpenedSpareDeckClick(object sender, RoutedEventArgs e)
        {
            pyramidCore.SelectOpenedSpareCard();
        }

        private void OnRestartGameClick(object sender, RoutedEventArgs e)
        {
            pyramidCore.RestartGame();
        }

        private void OnExitGameClick(object sender, CancelEventArgs e)
        {
            DoUnsubscribe();
            GameHelper.CloseGame();
        }

        private void OnHeaderRulesClick(object sender, RoutedEventArgs e)
        {
            new RulesWindow().Show();
        }


        private void UpdatePyramidCardsView()
        {
            bool hasSelection = pyramidCore.HasFirstSelection;
            Card firstSelectionCard = pyramidCore.FirstSelectionCard;

            int cardViewIndex = 0;
            foreach (Card[] cardsLine in pyramidCore.PyramidCards)
            {
                foreach (Card card in cardsLine)
                {
                    CardView cardView = cardsView[cardViewIndex];

                    cardView.SetCardImage(card);
                    cardView.SetSelected(hasSelection && card != null && card.Equals(firstSelectionCard));

                    cardViewIndex++;
                }
            }
        }

        private void UpdateSpareDeckView()
        {
            SpareDeckClosedButton.Source = pyramidCore.ClosedSpareDeckCards.Count > 0 ? new BitmapImage(new Uri(ImagesPaths.CardReverseImage)) : null;
            SpareDeckOpenedButton.Source = pyramidCore.OpenedSpareDeckCards.Count > 0 ? new BitmapImage(new Uri(ImagesPaths.CardAverseImage(pyramidCore.OpenedSpareDeckCards.Peek()))) : null;

            bool isSelectedCard = pyramidCore.HasFirstSelection && pyramidCore.FirstSelection.IsSpareCard;
            SpareDeckOpenedButton.BitmapEffect = isSelectedCard ? GameParams.EnabledSelectionEffect : GameParams.DisabledSelectionEffect;

            MoveSpareDeckLeftButton.Opacity = pyramidCore.CanShowResetSpareDeckButton ? 1 : 0;
        }
    }
}
