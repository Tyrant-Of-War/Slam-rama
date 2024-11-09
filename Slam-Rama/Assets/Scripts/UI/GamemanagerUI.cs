using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamemanagerUI : MonoBehaviour
{
    public List<bool> AllReady;
    Playercontrols[] Players;
    [SerializeField] int readycount = 0;
    public GameObject PlayerSelectCanvas;
    public GameObject GameSettingsCanvas;
    private void Start()
    {
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
            if (readycount == AllReady.Count && AllReady.Count != 0)
            {
                Invoke("Ready", 3);
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
        //GameSettingsCanvas.SetActive(true); // i need someone to fix the GUI so it is acutally good.
        ToloadNextScene();
    }
    //Settings and such

    void ToloadNextScene()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
