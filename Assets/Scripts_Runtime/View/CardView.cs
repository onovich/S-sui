using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField] RectTransform trans;
    [SerializeField] Button button;
    [SerializeField] Image suitImage;
    [SerializeField] Image miniSuitImage;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] RectTransform stackedPointTrans;

    public Vector2 StackPoint => stackedPointTrans.position;

    int cardId;
    public int CardId => cardId;
    bool isDragging;
    Vector2 offset;
    Canvas canvas;
    int originalSiblingIndex;

    static readonly Vector3[] cornersBuffer = new Vector3[4];

    public System.Action<int> OnClickHandler;
    public System.Action<int> OnBeginDragEvent;
    public System.Action<int> OnDragEvent;
    public System.Action<int> OnEndDragEvent;

    void Awake() {
        canvas = GetComponentInParent<Canvas>();
        button.onClick.AddListener(() => {
            if (!isDragging) {
                OnClickHandler?.Invoke(cardId);
            }
        });
    }

    public void Ctor(int id, Sprite suitSprite, Color suitColor, string rankString) {
        cardId = id;
        suitImage.sprite = suitSprite;
        suitImage.color = suitColor;
        miniSuitImage.sprite = suitSprite;
        miniSuitImage.color = suitColor;
        rankText.text = rankString;
        rankText.color = suitColor;
    }

    public void SetPos(Vector2 pos) {
        trans.position = pos;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        isDragging = true;

        originalSiblingIndex = trans.GetSiblingIndex();
        trans.SetAsLastSibling();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset);
        Vector2 pos = trans.localPosition;
        offset -= pos;

        OnBeginDragEvent?.Invoke(cardId);
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPointerPosition)) {
            trans.localPosition = localPointerPosition - offset;
        }

        OnDragEvent?.Invoke(cardId);
    }

    public void OnEndDrag(PointerEventData eventData) {
        isDragging = false;
        // trans.SetSiblingIndex(originalSiblingIndex);
        OnEndDragEvent?.Invoke(cardId);
    }
}