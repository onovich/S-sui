using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour {

    [SerializeField] RectTransform trans;
    [SerializeField] Button button;
    [SerializeField] Image suitImage;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] Vector2 stackedPoint;

    public Vector2 StackPoint => stackedPoint;

    int cardId;
    public int CardId => cardId;

    Action<int> OnClickHandler;

    public void Ctor(int id, Sprite suitSprite, string randString) {
        cardId = id;
        button.onClick.AddListener(() => {
            OnClickHandler?.Invoke(cardId);
        });
        suitImage.sprite = suitSprite;
        rankText.text = randString;
    }

    public void MoveTo(Vector2 pos) {
        trans.anchoredPosition = pos;
    }

    public Vector2 GetSize() {
        return trans.sizeDelta;
    }

    void OnDrawGizmos() {
        Vector2 size = GetSize();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(stackedPoint, size);
    }

}