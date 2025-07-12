using UnityEngine;

public class Main : MonoBehaviour {

    [SerializeField] TableContext tableContext;
    [SerializeField] LogicContext logicContext;
    [SerializeField] ViewContext viewContext;

    void Awake() {
        logicContext.Inject(tableContext, viewContext);
        viewContext.Inject(tableContext);
        CardController.CreateAllCard(logicContext);
        CardController.DrawAllCard(logicContext);
    }

    void Binding() {

    }

}