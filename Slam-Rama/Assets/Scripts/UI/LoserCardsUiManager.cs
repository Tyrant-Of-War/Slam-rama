using System.Collections.Generic; // Required for List<T>
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static LooserCardPowers;

public class LoserCardsUiManager : MonoBehaviour
{
    RoundData.RoundType PreviousRound;
    public GameObject Canvas;
    [SerializeField] GameObject[] BGGraphic;
    [SerializeField] RoundData RoundData;
    public CardSetup Cards;

    public enum PowerUp
    {
        Magnetism = 1,
        LongArms = 2,
        RecoveryJump = 3,
        DamageBuff = 4,
        DashDamage = 5,
        Powerups = 6
    };

    List<PlayerInput> playerInputs = new List<PlayerInput>(); // Changed to List<PlayerInput
    List<PlayerData> playerDatas = new List<PlayerData>();


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

                playerDatas.Add(input.gameObject.GetComponent<UseItem>().playerData);
            }
            else
            {
                input.GetComponent<UseItem>().playerData.loserCardID = 64;
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

    public void NextPlayer()
    {
        if (playerInputs.Count > 0)
        {
            playerInputs.RemoveAt(0); // Remove the first player
            playerDatas.RemoveAt(0);
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
            case PowerUp.Magnetism:
                playerDatas[0].loserCardID = (int)PowerUp.Magnetism;
                break;
            case PowerUp.LongArms:
                playerDatas[0].loserCardID = (int)PowerUp.LongArms;
                break;
            case PowerUp.RecoveryJump:
                playerDatas[0].loserCardID = (int)PowerUp.RecoveryJump;
                break;
            case PowerUp.DamageBuff:
                playerDatas[0].loserCardID = (int)PowerUp.DamageBuff;
                break;
            case PowerUp.DashDamage:
                playerDatas[0].loserCardID = (int)PowerUp.DashDamage;
                break;
            case PowerUp.Powerups:
                playerDatas[0].loserCardID = (int)PowerUp.Powerups;
                playerDatas[0].itemID = Random.Range(1, 7);
                break;
        }
        NextPlayer();
    }
}
