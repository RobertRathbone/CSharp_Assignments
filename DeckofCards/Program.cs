using System;
using System.Collections.Generic;

namespace DeckOfCards
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
            [x] Create a Card Class
            [x] Give the Card class a property "stringVal" which will hold the value of the card ex. (Ace, 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King). This "val" should be a string.
            [x] Give the Card a property "suit" which will hold the suit of the card (Clubs, Spades, Hearts, Diamonds).
            [x] Give the Card a property "val" which will hold the numerical value of the card 1-13 as integers.
            [x] Next, create a class called "Deck"
            [x] Give the Deck class a property called "cards" which is a list of Card objects.
            [x] When initializing the deck, make sure that it has a list of 52 unique cards as its "cards" property.
            [x] Give the Deck a deal method that selects the "top-most" card, removes it from the list of cards, and returns the Card.
            [x] Give the Deck a reset method that resets the cards property to contain the original 52 cards.
            [x] Give the Deck a shuffle method that randomly reorders the deck's cards.
            [x] Finally, create a class called "Player"
            [x] Give the Player class a name property.
            [] Give the Player a hand property that is a List of type Card.
            [x] Give the Player a draw method of which draws a card from a deck, adds it to the player's hand and returns the Card.
            [x] Note this method will require reference to a deck object
            [] Give the Player a discard method which discards the Card at the specified index from the player's hand and returns this Card or null if the index does not exist.
            */

            Deck myDeck = new Deck();
            myDeck.Shuffle();
            Console.WriteLine(myDeck);


        }
    }
}