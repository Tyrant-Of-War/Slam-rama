using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class GamemanagerUI : MonoBehaviour
{
    // The list of the ready status of all players
    public List<bool> AllReady;
    
    // The list of all players joined
    Playercontrols[] Players;

    // The count of readied players
    [SerializeField] int readycount = 0;

    // Various UI elements
    public GameObject PlayerSelectCanvas;
    public GameObject GameSettingsCanvas;
    public GameObject canvasFirstSelect;

    // Unsure??????
    private bool inMenu;

    private void Start()
    {
        inMenu = true;
    }


    private void FixedUpdate()
    {
        // Checks if any players ready values are being recorded
        if (AllReady.Count != 0)
        {
            // Runs through each ready value in the list and adds a value to the ready count if its true
            foreach (bool ready in AllReady)
            {
                if (ready)
                {
                    readycount += 1;
                }
            }

            // Checks if any players have joined and if the amount of players is greater than the amount of values stored in the ready value list
            if (UnityEngine.InputSystem.PlayerInput.all.Count != 0 && UnityEngine.InputSystem.PlayerInput.all.Count > AllReady.Count)
            {
                // Runs through each player existing and adds their ready status to the list
                foreach (UnityEngine.InputSystem.PlayerInput player in UnityEngine.InputSystem.PlayerInput.all)
                {
                    AllReady.Add(player.GetComponentInChildren<PlayerUIController>().ready);
                }
            }

            // Checks if the count of ready players is equal to the count of players who exist and that inMenu is true
            if (readycount == AllReady.Count && AllReady.Count != 0 && inMenu != false)
            {
                // Calls the ready function
                Ready();
                // Sets in menu to false
                inMenu = false;
            }
            else // if not clears the player ready list and the ready player count
            {
                readycount = 0;
                AllReady.Clear();
            }
        }
        else if (AllReady.Count == 0) // If not
        {
            // Checks if any players exist
            if (UnityEngine.InputSystem.PlayerInput.all.Count != 0)
            {
                // Runs through each player and adds their ready value to the list
                foreach (UnityEngine.InputSystem.PlayerInput player in UnityEngine.InputSystem.PlayerInput.all)
                {
                    AllReady.Add(player.GetComponentInChildren<PlayerUIController>().ready);
                }
            }
        }

    }

    //Ready Up State
    public void Ready()
    {
        // Disables the player canvases and enables the game settings canvas
        PlayerSelectCanvas.SetActive(false);
        GameSettingsCanvas.SetActive(true); 

        // Runs through each player in existance
        foreach (UnityEngine.InputSystem.PlayerInput player in UnityEngine.InputSystem.PlayerInput.all)
        {
            // Disables their mesh renderer so they are not in the way of the UI
            player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

            // Checks if this player is player 1 and gives them control if so
            if (player == UnityEngine.InputSystem.PlayerInput.all[0])
            {
                player.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = GameSettingsCanvas;
                player.GetComponentInChildren<MultiplayerEventSystem>().SetSelectedGameObject(canvasFirstSelect);
            }
        }
    }
    //Settings and such

    public void ToloadNextScene()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
