using System;
using System.Collections.Generic;
using System.Linq;

namespace pyramid
{
    public class PyramidGameCore
    {
        public event Action OnFullGameUpdate;
        public event Action OnSpareDeckUpdate;
        public event Action OnPyramidUpdate;
        public event Action OnGameOver;

        public GameState GameState { get; private set; }
        public SelectionParams FirstSelection { get; private set; }
        public Card[][] PyramidCards { get; private set; }
        public Stack<Card> ClosedSpareDeckCards { get; private set; }
        public Stack<Card> OpenedSpareDeckCards { get; private set; }

        public bool HasFirstSelection => FirstSelection != null;
        public Card FirstSelectionCard => HasFirstSelection ? GetCardBySelection(FirstSelection) : null;
        public bool CanShowResetSpareDeckButton => ClosedSpareDeckCards.Count == 0 && OpenedSpareDeckCards.Count != 0;


        public PyramidGameCore()
        {
            Initialize();
        }

        public void SelectPyramidCard(SelectionParams selection)
        {
            if (!IsInteractablePyramidCard(selection.IndexX, selection.IndexY))
                return;

            ProcessCardSelection(selection, OnPyramidUpdate);
        }

        public void SelectOpenedSpareCard()
        {
            if (OpenedSpareDeckCards.Count == 0)
                return;

            SelectionParams selection = new SelectionParams(true);
            ProcessCardSelection(selection, OnSpareDeckUpdate);
        }

        public void OpenLastClosedSpareCard()
        {
            if (ClosedSpareDeckCards.Count == 0)
                return;

            Card lastCard = ClosedSpareDeckCards.Pop();
            OpenedSpareDeckCards.Push(lastCard);

            if (FirstSelection != null && FirstSelection.IsSpareCard)
                FirstSelection = null;

            OnSpareDeckUpdate?.Invoke();
        }

        public void MoveOpenedSpareCardsToClosed()
        {
            if (!CanShowResetSpareDeckButton)
                return;

            int openedCardsCount = OpenedSpareDeckCards.Count;

            for (int i = 0; i < openedCardsCount; i++)
                ClosedSpareDeckCards.Push(OpenedSpareDeckCards.Pop());

            OnSpareDeckUpdate?.Invoke();
        }

        public void RestartGame()
        {
            Initialize();

            OnFullGameUpdate?.Invoke();
        }


        private void Initialize()
        {
            Card[] fullDeckCards = GameParams.ClearCardsDeck;
            ShuffleCardsArray(fullDeckCards);
            (Card[] pyramid, Card[] spare) = SplitDeckIntoPyramidAndSpareCards(fullDeckCards, GameParams.DECK_SIZE, GameParams.PYRAMID_CARDS_COUNT);

            PyramidCards = CardsArrayToPyramidStructure(pyramid, GameParams.PYRAMID_LINES_COUNT);
            ClosedSpareDeckCards = new Stack<Card>(spare.Reverse());
            OpenedSpareDeckCards = new Stack<Card>();

            GameState = GameState.Active;
            FirstSelection = null;
        }

        private void ShuffleCardsArray(Card[] cardsDeck)
        {
            Random random = new Random();

            for (int i = cardsDeck.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Card card = cardsDeck[j];
                cardsDeck[j] = cardsDeck[i];
                cardsDeck[i] = card;
            }
        }

        private (Card[], Card[]) SplitDeckIntoPyramidAndSpareCards(Card[] cards, int deckSize, int pyramidCardsCount)
        {
            if (cards == null || cards.Length != deckSize)
                throw new ArgumentException($"Invalid deck cards count! {cards?.Length ?? null}");

            Card[] pyramidCards = cards.Take(pyramidCardsCount).ToArray();
            Card[] spareCards = cards.Skip(pyramidCardsCount).ToArray();

            return (pyramidCards, spareCards);
        }

        private Card[][] CardsArrayToPyramidStructure(Card[] pyramidCards, int linesCount)
        {
            Card[][] result = new Card[linesCount][];

            int counter = 0;

            for (int i = 0; i < linesCount; i++)
            {
                Card[] cards = new Card[i + 1];
                for (int j = 0; j < cards.Length; j++)
                {
                    cards[j] = pyramidCards[counter];
                    counter++;
                }
                result[i] = cards;
            }

            return result;
        }


        private void ProcessCardSelection(SelectionParams selection, Action callback)
        {
            if (FirstSelection == null)
            {
                ProcessSelectionIfZeroSelected(selection);
                callback?.Invoke();
                return;
            }

            if (FirstSelection.Equals(selection))
            {
                FirstSelection = null;
                callback?.Invoke();
                return;
            }

            ProcessSelectionIfSecondSelected(selection);
            OnFullGameUpdate?.Invoke();

        }

        private void ProcessSelectionIfZeroSelected(SelectionParams firstSelection)
        {
            Card cardToSelect = GetCardBySelection(firstSelection);

            if (cardToSelect.Value == GameParams.NEEDED_CARDS_SUM)
            {
                RemoveCardBySelection(firstSelection);

                TryUpdateGameState();
                return;
            }

            this.FirstSelection = firstSelection;
        }

        private void ProcessSelectionIfSecondSelected(SelectionParams secondSelection)
        {
            if (FirstSelection.IsSpareCard && secondSelection.IsSpareCard)
                throw new ArgumentException("Both selections cannot be spare!");

            Card firstCard = GetCardBySelection(FirstSelection);
            Card secondCard = GetCardBySelection(secondSelection);

            int valuesSum = firstCard.Value + secondCard.Value;
            if (valuesSum == GameParams.NEEDED_CARDS_SUM)
            {
                RemoveCardBySelection(FirstSelection);
                RemoveCardBySelection(secondSelection);

                TryUpdateGameState();
            }

            FirstSelection = null;
        }

        private Card GetCardBySelection(SelectionParams selection)
        {
            if (selection == null)
                throw new ArgumentException($"{nameof(SelectionParams)} is null!");

            return selection.IsSpareCard ? GetSpareDeckCard() : GetPyramidCard(selection);


            Card GetSpareDeckCard()
            {
                if (OpenedSpareDeckCards.Count == 0)
                    throw new InvalidOperationException($"{nameof(OpenedSpareDeckCards)} is empty!");

                return OpenedSpareDeckCards.Peek();
            }

            Card GetPyramidCard(SelectionParams selection)
            {
                if (selection.IndexY >= PyramidCards.GetLength(0) || selection.IndexX >= PyramidCards[selection.IndexY].Length)
                    throw new ArgumentException($"Invalid {nameof(SelectionParams)} indexes: {selection}");

                return PyramidCards[selection.IndexY][selection.IndexX];
            }
        }

        private void RemoveCardBySelection(SelectionParams selection)
        {
            if (selection == null)
                throw new ArgumentException($"{nameof(SelectionParams)} is null!");

            if (selection.IsSpareCard)
                RemoveSpareDeckCard();
            else
                RemovePyramidCard(selection);


            void RemoveSpareDeckCard()
            {
                if (OpenedSpareDeckCards.Count == 0)
                    throw new InvalidOperationException($"{nameof(OpenedSpareDeckCards)} is empty!");

                OpenedSpareDeckCards.Pop();
            }

            void RemovePyramidCard(SelectionParams selection)
            {
                if (selection.IndexY >= PyramidCards.GetLength(0) || selection.IndexX >= PyramidCards[selection.IndexY].Length)
                    throw new ArgumentException($"Invalid {nameof(SelectionParams)} indexes: {selection}");

                PyramidCards[selection.IndexY][selection.IndexX] = null;
            }
        }


        private void TryUpdateGameState()
        {
            if (CheckIsGameSuccess())
            {
                GameState = GameState.Sucess;
                OnGameOver?.Invoke();
                return;
            }

            if (CheckIsGameFailure())
            {
                GameState = GameState.Failure;
                OnGameOver?.Invoke();
                return;
            }
        }

        private bool CheckIsGameSuccess()
        {
            return PyramidCards[0][0] == null;
        }

        private bool CheckIsGameFailure()
        {
            List<int> availablePyramidValues = GetAvailablePyramidValues();

            bool hasSingleClosingCard = CheckForSingleClosingCard(availablePyramidValues);
            if (hasSingleClosingCard)
                return false;

            bool hasPairsInPyramid = CheckSingleCollectionValuesComparison(availablePyramidValues);
            if (hasPairsInPyramid)
                return false;

            List<int> spareDeckValues = GetSpareDeckValues();
            bool canMoveWithSpareDeck = CheckTwoCollectionsValuesComparison(availablePyramidValues, spareDeckValues);

            return !canMoveWithSpareDeck;
        }

        private List<int> GetAvailablePyramidValues()
        {
            List<int> availableValuesPyramid = new List<int>();

            for (int indexY = 0; indexY < PyramidCards.GetLength(0); indexY++)
            {
                for (int indexX = 0; indexX < PyramidCards[indexY].Length; indexX++)
                {
                    Card card = PyramidCards[indexY][indexX];
                    if (card == null)
                        continue;

                    bool isClickable = IsInteractablePyramidCard(indexX, indexY);
                    if (!isClickable)
                        continue;

                    availableValuesPyramid.Add(card.Value);
                }
            }

            return availableValuesPyramid;
        }

        private List<int> GetSpareDeckValues()
        {
            List<int> spareDeckValues = new List<int>();

            foreach (Card card in ClosedSpareDeckCards)
                spareDeckValues.Add(card.Value);

            foreach (Card card in OpenedSpareDeckCards)
                spareDeckValues.Add(card.Value);

            return spareDeckValues;
        }

        private bool CheckForSingleClosingCard(List<int> availablePyramidValues)
        {
            foreach (int value in availablePyramidValues)
            {
                if (value == GameParams.NEEDED_CARDS_SUM)
                    return true;
            }

            return false;
        }

        private bool CheckSingleCollectionValuesComparison(List<int> availablePyramidValues)
        {
            for (int i = 0; i < availablePyramidValues.Count; i++)
            {
                for (int j = i + 1; j < availablePyramidValues.Count; j++)
                {
                    int valuesSum = availablePyramidValues[i] + availablePyramidValues[j];

                    if (valuesSum == GameParams.NEEDED_CARDS_SUM)
                        return true;
                }
            }

            return false;
        }

        private bool CheckTwoCollectionsValuesComparison(List<int> fistCollection, List<int> secondCollection)
        {
            foreach (int firstValue in fistCollection)
            {
                foreach (int secondValue in secondCollection)
                {
                    int valuesSum = firstValue + secondValue;

                    if (valuesSum == GameParams.NEEDED_CARDS_SUM)
                        return true;
                }
            }

            return false;
        }


        private bool IsInteractablePyramidCard(int indexX, int indexY)
        {
            if (indexY == PyramidCards.GetLength(0) - 1)
                return true;

            Card[] nextRow = PyramidCards[indexY + 1];
            Card upperLeft = nextRow[indexX];
            Card upperRight = nextRow[indexX + 1];

            if (upperLeft == null && upperRight == null)
                return true;

            return false;
        }
    }
}
