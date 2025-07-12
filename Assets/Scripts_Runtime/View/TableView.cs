using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableView : MonoBehaviour {

    [SerializeField] RectTransform body;
    [SerializeField] Image[] freeCells;
    [SerializeField] Vector2[] foundations;
    [SerializeField] CardView cardViewPrefab;

    public RectTransform Body => body;

    public Vector2 GetFoundationPos(int line) {
        if (line < 0 || line >= foundations.Length) {
            Debug.LogError($"Invalid foundation line: {line}. Valid range is 0 to {foundations.Length - 1}.");
        }
        return foundations[line];
    }

    void OnDrawGizmos() {
        foreach (var cell in foundations) {
            Gizmos.color = Color.green;
            Vector2 size = cardViewPrefab.GetSize();
            Gizmos.DrawWireCube(cell, size);
        }
    }

}