using System;
using UnityEngine;
using UnityEngine.UI;

public class CardModel {

    int id;
    public int Id => id;

    Vector2 pos;
    public Vector2 Pos => pos;
    Vector2 originalPos;

    Vector2 size;

    CardSuit suit;
    public CardSuit Suit => suit;

    int rank;
    public int Rank => rank;

    public void Ctor(int id, CardSuit suit, int rank, Vector2 sizes) {
        this.id = id;
        this.suit = suit;
        this.rank = rank;
        this.size = sizes;
    }

    public void SetPos(Vector2 position) {
        pos = position;
    }

    public void SetOriginalPos(Vector2 position) {
        originalPos = position;
    }

    public AABB GetAABB() {
        Vector2 min = pos - size * 0.5f;
        Vector2 max = pos + size * 0.5f;
        return new AABB(min, max);
    }

}