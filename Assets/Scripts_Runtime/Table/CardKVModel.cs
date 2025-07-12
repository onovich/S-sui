using System;
using TriInspector;
using UnityEngine;

[Serializable]
[DeclareHorizontalGroup("CardKV")]
public struct CardKVModel {
    [Group("Card")] public CardSuit suit;
    [Group("Card")] public Sprite suitSprite;
}