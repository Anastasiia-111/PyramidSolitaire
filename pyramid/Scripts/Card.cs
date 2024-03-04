namespace pyramid
{
    public class Card
    {
        public SuitType SuitType { get; private set; }
        public ValueType ValueType { get; private set; }
        public int Value { get; private set; }


        public Card(ValueType valueType, SuitType suitType)
        {
            ValueType = valueType;
            SuitType = suitType;

            Value = GetNumValue(valueType);
        }


        private int GetNumValue(ValueType valueType)
        {
            switch (valueType)
            {
                case ValueType.Card_2: return 2;
                case ValueType.Card_3: return 3;
                case ValueType.Card_4: return 4;
                case ValueType.Card_5: return 5;
                case ValueType.Card_6: return 6;
                case ValueType.Card_7: return 7;
                case ValueType.Card_8: return 8;
                case ValueType.Card_9: return 9;
                case ValueType.Card_10: return 10;
                case ValueType.Card_J: return 11;
                case ValueType.Card_Q: return 12;
                case ValueType.Card_K: return 13;
                case ValueType.Card_A: return 1;

                default: return -1;
            }
        }
    }
}
