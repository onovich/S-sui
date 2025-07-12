using UnityEngine;

[CreateAssetMenu(fileName = "CardTable", menuName = "~/Sōsui/CardTable", order = 0)]
public class CardTable : ScriptableObject {

    [SerializeField] CardKVModel[] cardKVModels;

    public Sprite GetSprite(CardSuit suit) {
        foreach (var kv in cardKVModels) {
            if (kv.suit == suit) {
                return kv.suitSprite;
            }
        }
        return null;
    }

    public Color GetColor(CardSuit suit) {
        foreach (var kv in cardKVModels) {
            if (kv.suit == suit) {
                return kv.suitColor;
            }
        }
        return Color.green;
    }

}