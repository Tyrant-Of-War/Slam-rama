using System.Collections.Generic; // Required for List<T>
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoserCardsUiManager : MonoBehaviour
{
    RoundData.RoundType PreviousRound;
    RoundData.RoundType RoundSetting;
    public GameObject Canvas;
    [SerializeField] GameObject[] BGGraphic;
    [SerializeField] RoundData RoundData;
    public CardSetup Cards;
    public enum PowerUp
    {
        LongArms = 1,
        DamageBuff = 2,
        DashDamage = 3,
        Magnetism = 4,
        RecoveryJump = 5,
        Powerups = 6
    };
    PowerUp SelectedPowerUp;
    private List<PlayerInput> playerInputs = new List<PlayerInput>(); // Changed to List<PlayerInput>
    private void Start()
    {
        PreviousRound = RoundData.PreviousRound;
        SetBGGraphic();

        // Add all PlayerInput instances except the last player standing
        foreach (PlayerInput input in PlayerInput.all)
        {
            if (input != RoundData.LastPlayerStanding)
            {
                playerInputs.Add(input);
            }
        }

        // Switch all player inputs to the "UI" action map
        foreach (PlayerInput input in playerInputs)
        {
            input.SwitchCurrentActionMap("UI");
        }

        // Set the first selected game object for the first player's event system
        playerInputs[0].gameObject.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = Canvas;
        playerInputs[0].gameObject.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = Cards.currentCards[0];
        playerInputs[0].gameObject.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject.GetComponent<Button>().Select();
    }

    public void SetBGGraphic()
    {
        switch (PreviousRound)
        {
            case RoundData.RoundType.Boxing:
                BGGraphic[0].SetActive(true);
                break;
            case RoundData.RoundType.Clock:
                BGGraphic[1].SetActive(true);
                break;
            case RoundData.RoundType.Castle:
                BGGraphic[2].SetActive(true);
                break;
            case RoundData.RoundType.Witch:
                BGGraphic[3].SetActive(true);
                break;
        }
    }

    public void EnableSpecificUI()
    {
        // Implementation for enabling specific UI
    }

    public void NextPlayer()
    {
        if (playerInputs.Count > 0)
        {
            playerInputs.RemoveAt(0); // Remove the first player
            if (playerInputs.Count > 1)
            {
                playerInputs[0].gameObject.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = Canvas;
                playerInputs[0].gameObject.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = Cards.currentCards[0];
                playerInputs[0].gameObject.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject.GetComponent<Button>().Select();
            }
            else
            {
                SceneManager.LoadScene("LoadingScreen");
            }
        }

    }
    public void Select(int powerUp)
    {
        switch ((PowerUp)powerUp)
        {
            case PowerUp.LongArms:
                // Apply Power Up
                break;
            case PowerUp.DamageBuff:
                // Apply Power Up
                break;
            case PowerUp.DashDamage:
                break;
            case PowerUp.Magnetism:
                break;
            case PowerUp.RecoveryJump:
                break;
            case PowerUp.Powerups:
                break;
        }
        NextPlayer();
    }
}
