using UnityEngine;
using UnityEngine.InputSystem.UI;
public class PlayerUIController : MonoBehaviour
{
    // The ID of the player based on the order they joined
    Playercontrols PlayerID;

    // The canvas each players UI areas exists on
    public GameObject RootCanvas;

    // Used to check if this player is ready to enter the game
    public bool ready = false;

    // The various UI elements the player can take control of
    [SerializeField] GameObject[] HeadArrows;
    [SerializeField] GameObject[] BodyArrows;
    [SerializeField] GameObject[] legArrows;

    private void Start()
    {
        // Gets the canvas that the player is associated with?
        RootCanvas = this.GetComponent<MultiplayerEventSystem>().playerRoot;
        // Runs through each child of that canvas and enables it
        foreach (Transform child in RootCanvas.transform)
        {
            child.gameObject.SetActive(true);
        }
        
        // Sets ready to false until the player readies up
        ready = false;
    }

    // Is called when the player joins
    void Join(Playercontrols inputs)
    {
        PlayerID = inputs;
    }

    // Is called when the player presses B on this screen
    void OnReadyUp()
    {
        // Sets ready to the opposite of what it is currently to allow unreadying
        ready = !ready;

        // Disables or enables the UI elements based on the ready up state
        foreach (Transform Child in RootCanvas.transform)
        {
            Child.gameObject.SetActive(!ready);
        }

    }

}
