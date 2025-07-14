using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicContext : MonoBehaviour {

    // Internal
    List<CardModel> deckCards;
    Dictionary<int, CardModel> tableauCards;
    Dictionary<int, Stack<CardModel>> tableauCardsLineDict;
    CardModel[] tempArray;
    public IDService ids;
    public RandomService rd;

    // External
    public TableContext tableCtx;
    public ViewContext viewCtx;

    public LogicContext() {
        deckCards = new List<CardModel>();
        tableauCards = new Dictionary<int, CardModel>();
        tableauCardsLineDict = new Dictionary<int, Stack<CardModel>>();
        tempArray = new CardModel[16];
        ids = new IDService();
        rd = new RandomService();
    }

    public void AddCardToDeck(CardModel card) {
        deckCards.Add(card);
    }

    public int CountDeckCards() {
        return deckCards.Count;
    }

    public void SetCardToLine(CardModel card, int line) {
        if (!tableauCardsLineDict.ContainsKey(line)) {
            tableauCardsLineDict[line] = new Stack<CardModel>();
        }
        tableauCardsLineDict[line].Push(card);
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
        tableauCards.Add(card.Id, card);
        return true;
    }

    public void RemoveCard(int id) {
        if (tableauCards.ContainsKey(id)) {
            tableauCards.Remove(id);
        }
        foreach (var kvp in tableauCardsLineDict) {
            if (kvp.Value.Count > 0 && kvp.Value.Peek().Id == id) {
                kvp.Value.Pop();
                break;
            }
        }
    }

    public bool TryGetLastByLine(int line, out CardModel card) {
        if (tableauCardsLineDict.TryGetValue(line, out Stack<CardModel> stack) && stack.Count > 0) {
            card = stack.Peek();
            return true;
        }
        card = null;
        return false;
    }

    public int CopyAllLastByLines(out CardModel[] cardArray) {
        int count = 0;
        foreach (var key in tableauCardsLineDict.Keys) {
            if (TryGetLastByLine(key, out CardModel card)) {
                if (tempArray.Length <= count) {
                    Array.Resize(ref tempArray, tempArray.Length * 2);
                }
                tempArray[count] = card;
                count++;
            }
        }
        cardArray = tempArray;
        return count;
    }

    public void UpdateCardLine(int originLine, int targetLine) {
        if (!tableauCardsLineDict.TryGetValue(originLine, out Stack<CardModel> stack)) {
            return;
        }
        if (stack.Count == 0) {
            return;
        }

        CardModel card = stack.Pop();
        if (!tableauCardsLineDict.ContainsKey(targetLine)) {
            tableauCardsLineDict[targetLine] = new Stack<CardModel>();
        }
        tableauCardsLineDict[targetLine].Push(card);
    }

    public int CopyAllTableCards(out CardModel[] cardArray) {
        int count = 0;
        foreach (var card in tableauCards.Values) {
            if (tempArray.Length <= count) {
                Array.Resize(ref tempArray, tempArray.Length * 2);
            }
            tempArray[count] = card;
            count++;
        }

        cardArray = new CardModel[count];
        Array.Copy(tempArray, cardArray, count);
        return count;
    }

    public bool TryGetTableauCard(int id, out CardModel card) {
        if (tableauCards.TryGetValue(id, out card)) {
            return true;
        }
        card = null;
        return false;
    }

    public void ShuffleDeck(RandomService rd) {
        deckCards.Shuffle(rd);
    }

    public void Clear() {
        deckCards.Clear();
        tableauCards.Clear();
        tableauCardsLineDict.Clear();
    }

}