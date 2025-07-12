using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicContext : MonoBehaviour {

    // Internal
    List<CardModel> deckCards;
    Dictionary<int, CardModel> tableCards;
    CardModel[] tempArray;
    public IDService ids;
    public RandomService rd;

    // External
    public TableContext tableCtx;
    public ViewContext viewCtx;

    public LogicContext() {
        deckCards = new List<CardModel>();
        tableCards = new Dictionary<int, CardModel>();
        tempArray = new CardModel[16];
        ids = new IDService();
        rd = new RandomService();
    }

    public void Inject(TableContext tableContext, ViewContext viewContext) {
        tableCtx = tableContext;
        viewCtx = viewContext;
    }

    public void AddCard(CardModel card) {
        deckCards.Add(card);
    }

    public int CountDeckCards() {
        return deckCards.Count;
    }

    public bool TryDrawFromDeckTop(out CardModel card) {
        int count = 0;
        for (int i = 0; i < deckCards.Count; i++) {
            if (tempArray.Length <= count) {
                Array.Resize(ref tempArray, tempArray.Length * 2);
            }

            tempArray[count] = deckCards[i];
            count++;
        }

        if (count == 0) {
            card = null;
            return false;
        }

        int index = count - 1;
        card = tempArray[index];

        deckCards.Remove(card);
        tableCards.Add(card.Id, card);
        return true;
    }

    public void RemoveCard(int id) {
        if (tableCards.ContainsKey(id)) {
            tableCards.Remove(id);
        }
    }

    public int CopyAllCards(out CardModel[] cardArray) {
        int count = 0;
        foreach (var card in deckCards) {
            if (tempArray.Length <= count) {
                Array.Resize(ref tempArray, tempArray.Length * 2);
            }
            tempArray[count] = card;
            count++;
        }

        cardArray = new CardModel[count];
        Array.Copy(tempArray, cardArray, count);
        deckCards.Clear();
        return count;
    }

    public void ShuffleDeck(RandomService rd) {
        deckCards.Shuffle(rd);
    }

    public void Clear() {
        deckCards.Clear();
    }

}