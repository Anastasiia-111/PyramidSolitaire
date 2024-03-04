using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace pyramid
{
    public class CardView
    {
        private Image cardImage;


        public CardView(Image cardImage)
        {
            this.cardImage = cardImage;
        }

        public void SetCardImage(Card card)
        {
            cardImage.Source = card != null ? new BitmapImage(new Uri(ImagesPaths.CardAverseImage(card))) : null;
        }

        public void SetSelected(bool isSelected)
        {
            cardImage.BitmapEffect = isSelected ? GameParams.EnabledSelectionEffect : GameParams.DisabledSelectionEffect;
        }
    }
}
