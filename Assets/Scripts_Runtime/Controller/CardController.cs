using System;
using UnityEngine;
using UnityEngine.UI;

public static class CardController {

    public static void CreateAllCard(LogicContext ctx) {
        for (int i = 0; i < 52; i++) {
            CardSuit suit = (CardSuit)(i / 13);
            int rank = i % 13 + 1;
            CardSystem.CreateCard(ctx, suit, rank);
        }
        CardSystem.ShuffleDeck(ctx);
    }

    public static void DrawAllCard(LogicContext ctx) {
        while (ctx.CountDeckCards() > 0) {
            for (int i = 0; i < 8; i++) {
                bool succ = CardSystem.TryDrawCard(ctx, out CardModel card);
                if (!succ) {
                    return;
                }
                int id = card.Id;
                CardSuit suit = card.Suit;
                int rank = card.Rank;
                CardViewSystem.SpawnCard(ctx.viewCtx, id, suit, rank, i);
            }
        }
    }

    public static void MoveCard(LogicContext ctx, int cardId, int originLine, int targetLine) {
        CardViewSystem.MoveCard(ctx.viewCtx, cardId, originLine, targetLine);
    }

    public static void RemoveCard(LogicContext ctx, int cardId, int line) {
        CardSystem.RemoveCard(ctx, cardId);
        CardViewSystem.DestoryCard(ctx.viewCtx, cardId);
    }

}