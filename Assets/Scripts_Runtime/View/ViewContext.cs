using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewContext : MonoBehaviour {

    // Serialized
    public CardView cardPrefab;
    public TableauView tableau;
    public TableContext tableCtx;

    // Internal
    Dictionary<int, CardView> cards;
    Dictionary<int, Stack<CardView>> cardsLineDict;
    CardView[] tempArray;
    public TableauView Tableau => tableau;

    public ViewContext() {
        cards = new Dictionary<int, CardView>();
        cardsLineDict = new Dictionary<int, Stack<CardView>>();
        tempArray = new CardView[16];
    }

    public void AddCard(CardView card, int line) {
        if (cards.ContainsKey(card.CardId)) {
            return;
        }
        cards[card.CardId] = card;
        if (!cardsLineDict.ContainsKey(line)) {
            cardsLineDict[line] = new Stack<CardView>();
        }
        cardsLineDict[line].Push(card);
    }

    public bool TryGetCard(int id, out CardView card) {
        if (cards.TryGetValue(id, out card)) {
            return true;
        }
        card = null;
        return false;
    }

    public bool TryGetLastByLine(int line, out CardView card) {
        if (cardsLineDict.TryGetValue(line, out Stack<CardView> stack) && stack.Count > 0) {
            card = stack.Peek();
            return true;
        }
        card = null;
        return false;
    }

    public int CopyAllCards(out CardView[] cardArray) {
        int count = 0;
        foreach (var kvp in cards) {
            if (tempArray.Length <= count) {
                Array.Resize(ref tempArray, tempArray.Length * 2);
            }
            tempArray[count] = kvp.Value;
            count++;
        }

        cardArray = new CardView[count];
        Array.Copy(tempArray, cardArray, count);
        cards.Clear();
        return count;
    }

    public void RemoveCard(int id) {
        if (cards.ContainsKey(id)) {
            cards.Remove(id);
        }
        foreach (var kvp in cardsLineDict) {
            if (kvp.Value.Count > 0 && kvp.Value.Peek().CardId == id) {
                kvp.Value.Pop();
                break;
            }
        }
    }

    public void UpdateCardLine(int originLine, int targetLine) {
        if (!cardsLineDict.TryGetValue(originLine, out Stack<CardView> stack)) {
            return;
        }
        if (stack.Count == 0) {
            return;
        }

        CardView card = stack.Pop();
        if (!cardsLineDict.ContainsKey(targetLine)) {
            cardsLineDict[targetLine] = new Stack<CardView>();
        }
        cardsLineDict[targetLine].Push(card);
    }

    public void Clear() {
        cards.Clear();
        cardsLineDict.Clear();
    }

}