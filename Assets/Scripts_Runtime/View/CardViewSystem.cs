using UnityEngine;

public static class CardViewSystem {

    public static void SpawnCard(ViewContext ctx, int id, CardSuit suit, int rank, int line) {
        CardView card = GameObject.Instantiate(ctx.cardPrefab, ctx.Table.Body);
        var table = ctx.tableCtx.cardTable;
        var suitSprite = table.GetSprite(suit);
        var rankString = rank.ToRankString();
        card.Ctor(id, suitSprite, rankString);
        ctx.AddCard(card, line);
        InitPlaceCard(ctx, id, line);
    }

    static void InitPlaceCard(ViewContext ctx, int cardId, int line) {
        bool has = ctx.TryGetCard(cardId, out CardView card);
        if (!has) {
            return;
        }
        card.transform.SetParent(ctx.Table.Body, false);

        Vector2 pos;
        bool succ = ctx.TryGetLastByLine(line, out CardView lastCard);
        if (!succ) {
            pos = ctx.Table.GetFoundationPos(line);
        } else {
            pos = lastCard.StackPoint;
        }

        card.transform.localPosition = pos;
    }

    public static void MoveCard(ViewContext ctx, int cardId, int originLine, int targetLine) {
        bool has = ctx.TryGetCard(cardId, out CardView card);
        if (!has) {
            return;
        }

        ctx.UpdateCardLine(originLine, targetLine);

        Vector2 pos;
        bool succ = ctx.TryGetLastByLine(targetLine, out CardView lastCard);
        if (!succ) {
            pos = ctx.Table.GetFoundationPos(targetLine);
        } else {
            pos = lastCard.StackPoint;
        }
        card.transform.localPosition = pos;
    }

    public static void DestoryCard(ViewContext ctx, int cardId) {
        bool has = ctx.TryGetCard(cardId, out CardView card);
        if (!has) {
            return;
        }
        GameObject.Destroy(card.gameObject);
        ctx.RemoveCard(cardId);
    }

}