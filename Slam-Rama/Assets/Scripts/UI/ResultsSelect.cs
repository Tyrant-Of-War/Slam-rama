using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class ResultsSelect : MonoBehaviour
{
    [SerializeField] Button Button;
    void Start()
    {
        PlayerInput.all[0].gameObject.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = Button.gameObject;
        Button.Select();
    }

}
