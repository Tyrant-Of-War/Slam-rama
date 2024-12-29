using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTint : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject CounterPartObject;
    public Color Tint;
    private void Start()
    {
        Tint = Color.red;
    }
    public void OnSelect(BaseEventData eventData)
    {
        CounterPartObject.GetComponent<Image>().color = Tint;
        this.GetComponent<Image>().color = Tint;
    }
    public void OnDeselect(BaseEventData data)
    {
        CounterPartObject.GetComponent<Image>().color = Color.white;
        this.GetComponent<Image>().color = Color.white;
        Debug.Log(this.name + " Was deSelected");
    }
}
