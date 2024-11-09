using UnityEngine;
using UnityEngine.InputSystem.UI;
public class PlayerUIController : MonoBehaviour
{
    Playercontrols PlayerID;
    [SerializeField] GameObject RootCanvas;
    public bool ready = false;
    [SerializeField] GameObject[] HeadArrows;
    [SerializeField] GameObject[] BodyArrows;
    [SerializeField] GameObject[] legArrows;

    private void Start()
    {
        RootCanvas = this.GetComponent<MultiplayerEventSystem>().playerRoot;
        foreach (Transform child in RootCanvas.transform)
        {
            child.gameObject.SetActive(true);
        }
        ready = false;
    }
    void Join(Playercontrols inputs)
    {
        PlayerID = inputs;
    }

    void OnReadyUp()
    {
        ready = !ready;
        foreach (Transform Child in RootCanvas.transform)
        {
            Child.gameObject.SetActive(!ready);
        }

    }

}
