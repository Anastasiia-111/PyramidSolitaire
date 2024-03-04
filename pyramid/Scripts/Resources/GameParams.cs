using System.Windows.Media;
using System.Windows.Media.Effects;

namespace pyramid
{
    public static class GameParams
    {
        public const int DECK_SIZE = PYRAMID_CARDS_COUNT + SPARE_CARDS_COUNT;
        public const int PYRAMID_CARDS_COUNT = 28;
        public const int SPARE_CARDS_COUNT = 24;

        public const int PYRAMID_LINES_COUNT = 7;
        public const int NEEDED_CARDS_SUM = 13;


        public static DropShadowBitmapEffect DisabledSelectionEffect => new DropShadowBitmapEffect { Opacity = 0 };
        public static DropShadowBitmapEffect EnabledSelectionEffect => new DropShadowBitmapEffect { Softness = 1, Opacity = 1, ShadowDepth = 0, Color = new Color() { R = 234, G = 252, B = 113, A = 1 } };


        public static Card[] ClearCardsDeck => new Card[]
        {
            new Card(ValueType.Card_2, SuitType.Hearts),
            new Card(ValueType.Card_3, SuitType.Hearts),
            new Card(ValueType.Card_4, SuitType.Hearts),
            new Card(ValueType.Card_5, SuitType.Hearts),
            new Card(ValueType.Card_6, SuitType.Hearts),
            new Card(ValueType.Card_7, SuitType.Hearts),
            new Card(ValueType.Card_8, SuitType.Hearts),
            new Card(ValueType.Card_9, SuitType.Hearts),
            new Card(ValueType.Card_10, SuitType.Hearts),
            new Card(ValueType.Card_J, SuitType.Hearts),
            new Card(ValueType.Card_Q, SuitType.Hearts),
            new Card(ValueType.Card_K, SuitType.Hearts),
            new Card(ValueType.Card_A, SuitType.Hearts),
            new Card(ValueType.Card_2, SuitType.Clubs),
            new Card(ValueType.Card_3, SuitType.Clubs),
            new Card(ValueType.Card_4, SuitType.Clubs),
            new Card(ValueType.Card_5, SuitType.Clubs),
            new Card(ValueType.Card_6, SuitType.Clubs),
            new Card(ValueType.Card_7, SuitType.Clubs),
            new Card(ValueType.Card_8, SuitType.Clubs),
            new Card(ValueType.Card_9, SuitType.Clubs),
            new Card(ValueType.Card_10, SuitType.Clubs),
            new Card(ValueType.Card_J, SuitType.Clubs),
            new Card(ValueType.Card_Q, SuitType.Clubs),
            new Card(ValueType.Card_K, SuitType.Clubs),
            new Card(ValueType.Card_A, SuitType.Clubs),
            new Card(ValueType.Card_2, SuitType.Spades),
            new Card(ValueType.Card_3, SuitType.Spades),
            new Card(ValueType.Card_4, SuitType.Spades),
            new Card(ValueType.Card_5, SuitType.Spades),
            new Card(ValueType.Card_6, SuitType.Spades),
            new Card(ValueType.Card_7, SuitType.Spades),
            new Card(ValueType.Card_8, SuitType.Spades),
            new Card(ValueType.Card_9, SuitType.Spades),
            new Card(ValueType.Card_10, SuitType.Spades),
            new Card(ValueType.Card_J, SuitType.Spades),
            new Card(ValueType.Card_Q, SuitType.Spades),
            new Card(ValueType.Card_K, SuitType.Spades),
            new Card(ValueType.Card_A, SuitType.Spades),
            new Card(ValueType.Card_2, SuitType.Diamonds),
            new Card(ValueType.Card_3, SuitType.Diamonds),
            new Card(ValueType.Card_4, SuitType.Diamonds),
            new Card(ValueType.Card_5, SuitType.Diamonds),
            new Card(ValueType.Card_6, SuitType.Diamonds),
            new Card(ValueType.Card_7, SuitType.Diamonds),
            new Card(ValueType.Card_8, SuitType.Diamonds),
            new Card(ValueType.Card_9, SuitType.Diamonds),
            new Card(ValueType.Card_10, SuitType.Diamonds),
            new Card(ValueType.Card_J, SuitType.Diamonds),
            new Card(ValueType.Card_Q, SuitType.Diamonds),
            new Card(ValueType.Card_K, SuitType.Diamonds),
            new Card(ValueType.Card_A, SuitType.Diamonds)
        };
    }
}
