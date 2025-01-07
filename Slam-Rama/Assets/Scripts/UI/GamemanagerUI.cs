using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GamemanagerUI : MonoBehaviour
{
    [SerializeField] RoundData roundData;

    [SerializeField] TextMeshProUGUI roundText;
    public GameObject PlayerJoinThing;

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

    // Stops people from spawning when they're not supposed to as game knows its in the menu
    private bool inMenu;

    private void Awake()
    {
        roundData.Initialize();
        inMenu = true;
        // deletes preexisting players 
        if (UnityEngine.InputSystem.PlayerInput.all.Count > 0)
        {
            switch (UnityEngine.InputSystem.PlayerInput.all.Count)
            {
                case 1:
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[0].gameObject);
                    break;
                case 2:
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[1].gameObject);
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[0].gameObject);
                    break;
                case 3:
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[2].gameObject);
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[1].gameObject);
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[0].gameObject);
                    break;
                case 4:
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[3].gameObject);
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[2].gameObject);
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[1].gameObject);
                    Destroy(UnityEngine.InputSystem.PlayerInput.all[0].gameObject);
                    break;
            }
        }
        PlayerSelectCanvas.SetActive(true);
        GameSettingsCanvas.SetActive(false);
        UpdateRoundAmount();

        PlayerPrefs.SetInt("Spawn_Items", 1);
        PlayerPrefs.SetInt("Hazards_On", 1);
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
                if (PlayerInput.all.Count < 2)
                {
                    
                }
                else
                {
                    // Calls the ready function
                    Ready();
                    // Sets in menu to false
                    inMenu = false;
                }
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
        PlayerJoinThing.SetActive(false);
        // Runs through each player in existance
        foreach (UnityEngine.InputSystem.PlayerInput player in UnityEngine.InputSystem.PlayerInput.all)
        {
            // Disables their mesh renderer so they are not in the way of the UI
            SkinnedMeshRenderer[] renderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();
            PlayerCosmeticInput[] playerCosmeticInputs = player.GetComponentsInChildren<PlayerCosmeticInput>();
            // Loop through each SkinnedMeshRenderer and Controller to disable it
            foreach (var renderer in renderers)
            {
                renderer.enabled = false;
            }
            foreach (var controller in playerCosmeticInputs)
            {
                controller.enabled = false;
            }
            player.gameObject.GetComponent<PlayerCosmeticInput>().enabled = false;

            // Checks if this player is player 1 and gives them control if so
            if (player == UnityEngine.InputSystem.PlayerInput.all[0])
            {
                player.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = GameSettingsCanvas;
                player.GetComponentInChildren<MultiplayerEventSystem>().SetSelectedGameObject(canvasFirstSelect);
            }
        }
    }
    //Settings and such

    public void UpdateRoundAmount()
    {
        if (roundText.text == "1")
        {
            roundText.text = "3";

            roundData.roundsLeft = 3;
            roundData.roundsMax = 3;
        }
        else if (roundText.text == "3")
        {
            roundText.text = "5";

            roundData.roundsLeft = 5;
            roundData.roundsMax = 5;
        }
        else if (roundText.text == "5")
        {
            roundText.text = "7";

            roundData.roundsLeft = 7;
            roundData.roundsMax = 7;
        }
        else if (roundText.text == "7")
        {
            roundText.text = "1";

            roundData.roundsLeft = 1;
            roundData.roundsMax = 1;
        }
    }

    public void UpdateHazards()
    {
        if (PlayerPrefs.GetInt("Hazards_On") == 0)
        {
            PlayerPrefs.SetInt("Hazards_On", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Hazards_On", 0);
        }
    }

    public void UpdateItemSpawning()
    {
        if (PlayerPrefs.GetInt("Spawn_Items") == 0)
        {
            PlayerPrefs.SetInt("Spawn_Items", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Spawn_Items", 0);
        }
    }

    public void ToloadNextScene()
    {
        if (PlayerInput.all.Count < 2)
        { return; }
        else if (PlayerPrefs.GetInt("Tutorial_Completed") == 1)
        {
            SceneManager.LoadScene("LoadingScreen");
        }
        else if (PlayerPrefs.GetInt("Tutorial_Completed") == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }

    }
}
