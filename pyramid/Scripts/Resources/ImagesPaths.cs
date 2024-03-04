namespace pyramid
{
    public static class ImagesPaths
    {
        public static string MainWindowBackground => ToImageFilePath(mainWindowBackground);
        public static string RulesWindowBackground => ToImageFilePath(rulesWindowBackground);
        public static string GameWindowBackground => ToImageFilePath(gameWindowBackground);
        public static string RestartButtonIcon => ToImageFilePath(restartButtonIcon);
        public static string RepeatButtonIcon => ToImageFilePath(repeatButtonIcon);
        public static string CardReverseImage => ToImageFilePath(cardReverseImage);
        public static string CardAverseImage(Card card) => ToImageFilePath(string.Format(cardAverseImage, ValueToImageKey(card.ValueType), SuitToImageKey(card.SuitType)));


        private static string mainWindowBackground = "main.jpg";
        private static string rulesWindowBackground = "rulesBackground.jpg";
        private static string gameWindowBackground = "wood.png";
        private static string restartButtonIcon = "restart.png";
        private static string repeatButtonIcon = "repeat.png";
        private static string cardAverseImage = "{0}_of_{1}.png";
        private static string cardReverseImage = "card_reverse.png";

        private static string applicationPath = "pack://application:,,,";
        private static string imagesPath = "/Images/";


        private static string ToImageFilePath(this string filePath)
        {
            return $"{applicationPath}{imagesPath}{filePath}";
        }

        private static string ValueToImageKey(ValueType cardValue)
        {
            switch (cardValue)
            {
                case ValueType.Card_2: return "2";
                case ValueType.Card_3: return "3";
                case ValueType.Card_4: return "4";
                case ValueType.Card_5: return "5";
                case ValueType.Card_6: return "6";
                case ValueType.Card_7: return "7";
                case ValueType.Card_8: return "8";
                case ValueType.Card_9: return "9";
                case ValueType.Card_10: return "10";
                case ValueType.Card_J: return "jack";
                case ValueType.Card_Q: return "queen";
                case ValueType.Card_K: return "king";
                case ValueType.Card_A: return "ace";

                default: return string.Empty;
            }
        }

        private static string SuitToImageKey(SuitType cardSuit)
        {
            switch (cardSuit)
            {
                case SuitType.Hearts: return "hearts";
                case SuitType.Clubs: return "clubs";
                case SuitType.Spades: return "spades";
                case SuitType.Diamonds: return "diamonds";

                default: return string.Empty;
            }
        }
    }
}
