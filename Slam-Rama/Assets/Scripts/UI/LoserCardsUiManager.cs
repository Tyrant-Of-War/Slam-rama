using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        foreach (PlayerInput input in PlayerInput.all)
        {
            input.gameObject.GetComponentInChildren<MultiplayerEventSystem>().enabled = false;
        }
        SetCurrentPlayer();
    }

    void SetCurrentPlayer()
    {
        // Ensure the current player index is within bounds
        while (index < PlayerInput.all.Count)
        {
            var currentPlayer = PlayerInput.all[index];

            // Skip the player if they are the last player standing
            if (currentPlayer == RoundData.LastPlayerStanding)
            {
                index++;
                continue; // Move to the next player
            }

            // Enable and set up the current player's event system
            var eventSystem = currentPlayer.gameObject.GetComponentInChildren<MultiplayerEventSystem>();
            eventSystem.enabled = true;
            eventSystem.playerRoot = Canvas;
            eventSystem.firstSelectedGameObject = Cards.currentCards[0];
            eventSystem.firstSelectedGameObject.GetComponent<Button>().Select();

            return; // Exit the method as we've set up the current player
        }

        // If all players are processed, re-enable all event systems and load the next scene
        foreach (PlayerInput input in PlayerInput.all)
        {
            var eventSystem = input.gameObject.GetComponentInChildren<MultiplayerEventSystem>();
            eventSystem.enabled = true;
            eventSystem.playerRoot = null;
            eventSystem.firstSelectedGameObject = null;
        }

        SceneManager.LoadScene("LoadingScreen");
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
            var currentPlayer = PlayerInput.all[index];
            var eventSystem = currentPlayer.gameObject.GetComponentInChildren<MultiplayerEventSystem>();
            eventSystem.playerRoot = null;
            eventSystem.firstSelectedGameObject = null;
            eventSystem.enabled = false;

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
        try
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
        }
        catch { };
        NextPlayer();
    }
}
