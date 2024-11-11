using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class GamemanagerUI : MonoBehaviour
{
    public List<bool> AllReady;
    Playercontrols[] Players;
    [SerializeField] int readycount = 0;
    public GameObject PlayerSelectCanvas;
    public GameObject GameSettingsCanvas;
    public GameObject canvasFirstSelect;
    private bool inmenu;
    private void Start()
    {
        inmenu = true;
    }
    private void FixedUpdate()
    {
        if (AllReady.Count != 0)
        {
            foreach (bool ready in AllReady)
            {
                if (ready)
                {
                    readycount += 1;
                }
            }
            if (UnityEngine.InputSystem.PlayerInput.all.Count != 0 && UnityEngine.InputSystem.PlayerInput.all.Count > AllReady.Count)
            {
                foreach (UnityEngine.InputSystem.PlayerInput player in UnityEngine.InputSystem.PlayerInput.all)
                {
                    AllReady.Add(player.GetComponentInChildren<PlayerUIController>().ready);
                }
            }
            if (readycount == AllReady.Count && AllReady.Count != 0 && inmenu != false)
            {
                Ready();
                inmenu = false;
            }
            else
            {
                readycount = 0;
                AllReady.Clear();
            }
        }
        else if (AllReady.Count == 0)
        {
            if (UnityEngine.InputSystem.PlayerInput.all.Count != 0)
            {
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
        PlayerSelectCanvas.SetActive(false);
        GameSettingsCanvas.SetActive(true); // i need someone to fix the GUI so it is acutally good.
        foreach (UnityEngine.InputSystem.PlayerInput player in UnityEngine.InputSystem.PlayerInput.all)
        {
            player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
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
