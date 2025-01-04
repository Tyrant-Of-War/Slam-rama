using System.Collections.Generic; // Required for List<T>
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LoserCardsUiManager : MonoBehaviour
{
    RoundData.RoundType PreviousRound;
    RoundData.RoundType RoundSetting;
    public GameObject Canvas;
    [SerializeField] GameObject[] BGGraphic;
    [SerializeField] RoundData RoundData;
    public CardSetup Cards;

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
                playerInputs[0].gameObject.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = Cards.currentCards[0];
            }
            else
            {
                //Next Scene
            }
        }
    }
}
