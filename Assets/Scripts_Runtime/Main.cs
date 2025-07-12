using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    [SerializeField] TableContext tableContext;
    [SerializeField] LogicContext logicContext;
    [SerializeField] ViewContext viewContext;
    [SerializeField] CanvasScaler mainCanvasScaler;

    float lastScreenWidth;
    float lastScreenHeight;
    const float DesignWidth = 1920f;
    const float DesignHeight = 1080f;
    const float DesignAspect = DesignWidth / DesignHeight;

    void AdaptCanvas() {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenAspect = screenWidth / screenHeight;

        if (screenAspect > DesignAspect) {
            mainCanvasScaler.matchWidthOrHeight = 1;
        } else {
            mainCanvasScaler.matchWidthOrHeight = 0;
        }
    }

    void Start() {
        Application.targetFrameRate = 60;
        AdaptCanvas();

        CardController.CreateAllCard(logicContext);
        CardController.DrawAllCard(logicContext);
    }

    void Binding() {

    }

    void LateTick(float dt, float time) {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight) {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            AdaptCanvas();
        }
    }

    void OnDestroy() {
        CardController.DestoryAllCard(logicContext);
    }

}