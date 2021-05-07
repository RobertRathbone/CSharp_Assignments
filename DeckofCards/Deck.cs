using System;
using System.Collections.Generic;

namespace DeckOfCards
{
    public class Deck
    {
        public List<Card> Cards {get; set;} = new List<Card>();
        public Deck()
        {
            Reset();
        }
        public void Reset()
        {
            string[] suits = new string[4]
            {
                "Clubs",
                "Diamonds",
                "Spades",
                "Hearts"
            };
            Dictionary<int, string> cardNamesTable = new Dictionary<int, string>()
            {
                {1, "Ace"},
                {11, "Jack"},
                {12, "Queen"},
                {13, "King"},
            };
            foreach (string suit in suits)
            {
                for (int i = 1; i  < 14; i++)
                {
                    string cardName = i.ToString();
                    if (cardNamesTable.ContainsKey(i))
                    {
                        cardName = cardNamesTable[i];
                    }
                    Card currentCard = new Card(cardName, suit, i);
                    Cards.Add(currentCard);
                }
            }
        }
        public Card Deal()
        {
            if (Cards.Count == 0)
            {
                return null;
            }
            Card firstCard = Cards[0];
            Cards.RemoveAt(0);
            return firstCard;
        }
        public void Shuffle()
        {
            Random rand = new Random();
            for(int i = 0; i < Cards.Count; i++)
            {
                int randIdx = rand.Next(0, Cards.Count);
                Card temp = Cards[i];
                Cards[i] = Cards[randIdx];
                Cards[randIdx] = temp;
            }
        }
        public override string ToString()
        {
            string deckStr = "";
            foreach (Card card in Cards)
            {
                deckStr += "\n" + card;
            }
            return deckStr;
        }
    }
}