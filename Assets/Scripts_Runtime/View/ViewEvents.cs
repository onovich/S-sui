using System;

public class ViewEvents {

    public Action<int> OnCardClickHandler;
    public void OnCardClick(int cardId) {
        OnCardClickHandler?.Invoke(cardId);
    }

    public Action<int> OnCardBeginDragHandler;
    public void OnCardBeginDrag(int cardId) {
        OnCardBeginDragHandler?.Invoke(cardId);
    }

    public Action<int> OnCardDragHandler;
    public void OnCardDrag(int cardId) {
        OnCardDragHandler?.Invoke(cardId);
    }

    public Action<int> OnCardEndDragHandler;
    public void OnCardEndDrag(int cardId) {
        OnCardEndDragHandler?.Invoke(cardId);
    }

    public void Clear() {
        OnCardClickHandler = null;
        OnCardBeginDragHandler = null;
        OnCardDragHandler = null;
        OnCardEndDragHandler = null;
    }

}