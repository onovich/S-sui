using System;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

public class TableauView : MonoBehaviour {

    [SerializeField] RectTransform body;
    [SerializeField] RectTransform freeCellRoot;
    [SerializeField] RectTransform fondationRoot;
    [SerializeField] Image[] freeCells;
    [SerializeField] Vector2[] foundations;

    public RectTransform Body => body;

    [Button("GetAllFreeCells")]
    public void GetAllFreeCells() {
        freeCells = freeCellRoot.GetComponentsInChildren<Image>();
        if (freeCells.Length == 0) {
            Debug.LogWarning("No free cells found in the FreeCellRoot.");
        }
    }

    [Button("GetAllFoundations")]
    public void GetAllFoundations() {
        foundations = new Vector2[fondationRoot.childCount];
        for (int i = 0; i < fondationRoot.childCount; i++) {
            foundations[i] = fondationRoot.GetChild(i).GetComponent<RectTransform>().position;
        }
        if (foundations.Length == 0) {
            Debug.LogWarning("No foundations found in the FoundationRoot.");
        }
    }

    public Vector2 GetFoundationPos(int line) {
        if (line < 0 || line >= foundations.Length) {
            Debug.LogError($"Invalid foundation line: {line}. Valid range is 0 to {foundations.Length - 1}.");
        }
        return foundations[line];
    }

}