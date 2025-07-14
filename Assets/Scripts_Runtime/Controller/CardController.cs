using System;
using UnityEngine;
using UnityEngine.UI;

public static class CardController {

    public static void CreateAllCard(LogicContext ctx) {
        var table = ctx.tableCtx.cardTable;
        var size = table.cardSize;
        for (int i = 0; i < 52; i++) {
            CardSuit suit = (CardSuit)(i / 13);
            int rank = i % 13 + 1;
            CardSystem.CreateCard(ctx, suit, rank, size);
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

    public static void DestoryAllCard(LogicContext ctx) {
        CardSystem.Clear(ctx);
        int count = ctx.viewCtx.CopyAllCards(out CardView[] cardArray);
        for (int i = 0; i < count; i++) {
            CardViewSystem.DestoryCard(ctx.viewCtx, cardArray[i].CardId);
        }
    }

    public static void CheckOverlaps(LogicContext ctx, int cardId, int originLine) {
        bool has = ctx.TryGetTableauCard(cardId, out CardModel selectedCard);
        if (!has) {
            Debug.LogError($"Card with ID {cardId} not found in tableau.");
            return;
        }
        AABB selectedAABB = selectedCard.GetAABB();

        int count = ctx.CopyAllLastByLines(out CardModel[] cardArray);
        for (int i = 0; i < count; i++) {
            CardModel card = cardArray[i];
            if (card.Id == cardId) {
                continue;
            }
            AABB otherAABB = card.GetAABB();
            if (selectedAABB.Intersects(otherAABB)) {
                Debug.Log($"Card {cardId} overlaps with card {card.Id} in line {originLine}.");
            }
        }
    }

}