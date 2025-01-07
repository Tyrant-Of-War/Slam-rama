using System.Collections.Generic; // Required for List<T>
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using static LooserCardPowers;

public class LoserCardsUiManager : MonoBehaviour
{
    RoundData.RoundType PreviousRound;
    public GameObject Canvas;
    [SerializeField] GameObject[] BGGraphic;
    [SerializeField] RoundData RoundData;
    public CardSetup Cards;
    int index = 0;

    public enum PowerUp
    {
        Magnetism = 1,
        LongArms = 2,
        RecoveryJump = 3,
        DamageBuff = 4,
        DashDamage = 5,
        Powerups = 6
    };

    private void Start()
    {
        index = 0;
        PreviousRound = RoundData.PreviousRound;
        SetBGGraphic();

        // Switch all player inputs to the "UI" action map
        foreach (PlayerInput input in PlayerInput.all)
        {
            input.SwitchCurrentActionMap("UI");
        }

        SetCurrentPlayer();
    }

    void SetCurrentPlayer()
    {
        if (PlayerInput.all[index] != RoundData.LastPlayerStanding)
        {
            // Set the first selected game object for the first player's event system
            PlayerInput.all[index].gameObject.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = Canvas;
            PlayerInput.all[index].gameObject.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = Cards.currentCards[0];
            PlayerInput.all[index].gameObject.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject.GetComponent<Button>().Select();
        }
        else
        {
            index++;

            SetCurrentPlayer();
        }
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
        if (index < PlayerInput.all.Count - 1)
        {
            index++;

            Cards.SetCurrentCards();
            Cards.Setup();

            SetCurrentPlayer();
        }
        else
        {
            SceneManager.LoadScene("LoadingScreen");
        }

    }
    public void Select(int powerUp)
    {
        switch ((PowerUp)powerUp)
        {
            case PowerUp.Magnetism:
                PlayerInput.all[index].gameObject.GetComponent<UseItem>().playerData.loserCardID = (int)PowerUp.Magnetism;
                break;
            case PowerUp.LongArms:
                PlayerInput.all[index].gameObject.GetComponent<UseItem>().playerData.loserCardID = (int)PowerUp.LongArms;
                break;
            case PowerUp.RecoveryJump:
                PlayerInput.all[index].gameObject.GetComponent<UseItem>().playerData.loserCardID = (int)PowerUp.RecoveryJump;
                break;
            case PowerUp.DamageBuff:
                PlayerInput.all[index].gameObject.GetComponent<UseItem>().playerData.loserCardID = (int)PowerUp.DamageBuff;
                break;
            case PowerUp.DashDamage:
                PlayerInput.all[index].gameObject.GetComponent<UseItem>().playerData.loserCardID = (int)PowerUp.DashDamage;
                break;
            case PowerUp.Powerups:
                PlayerInput.all[index].gameObject.GetComponent<UseItem>().playerData.loserCardID = (int)PowerUp.Powerups;
                PlayerInput.all[index].gameObject.GetComponent<UseItem>().playerData.itemID = Random.Range(1, 7);
                break;
        }
        NextPlayer();
    }
}
