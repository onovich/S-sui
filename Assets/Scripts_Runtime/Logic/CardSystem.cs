using System;
using UnityEngine;
using UnityEngine.UI;

public static class CardSystem {

    public static void CreateCard(LogicContext ctx, CardSuit suit, int rank) {
        int cardId = ctx.ids.PickNextCardEntityID();
        CardModel cardModel = new CardModel();
        cardModel.Ctor(cardId, suit, rank);
        ctx.AddCard(cardModel);
    }

    public static void ShuffleDeck(LogicContext ctx) {
        ctx.ShuffleDeck(ctx.rd);
    }

    public static bool TryDrawCard(LogicContext ctx, out CardModel card) {
        if (ctx.TryDrawFromDeckTop(out card)) {
            return true;
        }
        card = null;
        return false;
    }

    public static void RemoveCard(LogicContext ctx, int cardId) {
        ctx.RemoveCard(cardId);
    }

    public static void Clear(LogicContext ctx) {
        ctx.Clear();
    }

}