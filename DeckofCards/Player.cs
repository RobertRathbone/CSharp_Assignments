using System.Collections.Generic;

namespace DeckOfCards
{
    public class Player{
        public string Name {get; set;}
        public List<Card> Hand { get; set;} = new List<Card>();
        public Card Draw(Deck deck)
        {
            Card dealtCard = deck.Deal();
            if (dealtCard != null)
            {
                Hand.Add(dealtCard);
            }
            return dealtCard;
        }
        public Card Discard(Card input)
        {
            for (var i = 0; i< Hand.Count; i++ )
            {
                if (input.Val == Hand[i].Val)
                {
                    Hand.Remove(Hand[i]);
                    return Hand[i];
                }
            }
            return null;
        }
    }
}