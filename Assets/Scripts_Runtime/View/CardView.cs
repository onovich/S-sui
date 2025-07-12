using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour {

    [SerializeField] RectTransform trans;
    [SerializeField] Button button;
    [SerializeField] Image suitImage;
    [SerializeField] Image miniSuitImage;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] RectTransform stackedPointTrans;

    public Vector2 StackPoint => stackedPointTrans.position;

    int cardId;
    public int CardId => cardId;

    Action<int> OnClickHandler;

    public void Ctor(int id, Sprite suitSprite, Color suitColor, string randString) {
        cardId = id;
        button.onClick.AddListener(() => {
            OnClickHandler?.Invoke(cardId);
        });
        suitImage.sprite = suitSprite;
        suitImage.color = suitColor;
        miniSuitImage.sprite = suitSprite;
        miniSuitImage.color = suitColor;
        rankText.text = randString;
    }

    public void MoveTo(Vector2 pos) {
        trans.position = pos;
    }

}