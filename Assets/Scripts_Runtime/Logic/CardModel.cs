using System;
using UnityEngine;
using UnityEngine.UI;

public class CardModel {

    int id;
    public int Id => id;

    CardSuit suit;
    public CardSuit Suit => suit;

    int rank;
    public int Rank => rank;

    public void Ctor(int id, CardSuit suit, int rank) {
        this.id = id;
        this.suit = suit;
        this.rank = rank;
    }

}