using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class RoundData : ScriptableObject
{
    //////////////

    // Should probably rework this entire thing

    //////////////
    ///na Shit works fine :)

    public int roundsLeft;
    public int roundsMax;
    public bool RandomRounds;

    [SerializeField] private PlayerData firstPlayerOut;

    [SerializeField] private PlayerInput lastPlayerStanding;

    public PlayerData FirstPlayerOut => firstPlayerOut;
    public PlayerInput LastPlayerStanding => lastPlayerStanding;

    public AudioSource roundWinner;

    public List<List<bool>> PlayerWin;
    public List<List<int>> Order;

    [Serializable]
    public enum RoundType
    {
        Boxing = 1,
        Clock = 2,
        Castle = 3,
        Random = 4,
        Witch = 5,
    }
    public RoundType roundType;
    public RoundType PreviousRound;

    private void Awake()
    {
        roundsLeft = 3; lastPlayerStanding = null; roundsMax = 3;
        RandomRounds = true;
        roundType = RoundType.Random;
        PlayerWin = new List<List<bool>>();
        PlayerWin.Add(new List<bool>());
        PlayerWin.Add(new List<bool>());
        PlayerWin.Add(new List<bool>());
        PlayerWin.Add(new List<bool>());
        Order = new List<List<int>>();
        for (int i = 0; i < roundsLeft; i++)
        {
            Order.Add(new List<int>());
        }
    }
    // Resets the tracker data for each new round
    public void ResetData()
    {
        firstPlayerOut = null;
        lastPlayerStanding = null;
    }
    public void AddLosingPlayer(PlayerInput playerInput, int Position)
    {
        if (PlayerInput.all[0] == playerInput)
        {
            Order[0][roundsMax - roundsLeft] = Position;
        }
        else if (PlayerInput.all[1] == playerInput)
        {
            Order[1][roundsMax - roundsLeft] = Position;
        }
        else if (PlayerInput.all[2] == playerInput)
        {
            Order[2][roundsMax - roundsLeft] = Position;
        }
        else if (PlayerInput.all[3] == playerInput)
        {
            Order[3][roundsMax - roundsLeft] = Position;
        }
    }
    // Updates the tracker data based on current player data
    public void UpdateData(List<PlayerData> playerData)
    {
        int playersWithLives = 0;
        lastPlayerStanding = null;

        foreach (PlayerData player in playerData)
        {
            if (player.lives > 0)
            {
                playersWithLives++;
                roundWinner.Play();
                lastPlayerStanding = PlayerInput.all[player.ID - 1];
            }
            else if (firstPlayerOut == null)
            {
                firstPlayerOut = player;
            }
        }
    }
    public void SwitchRoundType(int Value)
    {
        switch ((RoundType)Value)
        {
            case RoundType.Clock:
                roundType = RoundType.Clock;
                break;
            case RoundType.Castle:
                roundType = RoundType.Castle;
                break;
            case RoundType.Random:
                roundType = RoundType.Random;
                break;
            case RoundType.Witch:
                roundType = RoundType.Witch;
                break;
            case RoundType.Boxing:
                roundType = RoundType.Boxing;
                break;

        }
    }

    // Checks if the round should end (only one player with lives left)
    public bool IsRoundOver()
    {
        return lastPlayerStanding != null && firstPlayerOut != null;

    }
    public void SetLastPlayer(PlayerInput player)
    {
        lastPlayerStanding = player;
    }
    public void PlayerWinUpdate()
    {
        switch (lastPlayerStanding)
        {
            case var Player when Player == PlayerInput.all[0]:
                PlayerWin[0].Add(true);
                PlayerWin[1].Add(false);
                PlayerWin[2].Add(false);
                PlayerWin[3].Add(false);
                break;
            case var Player when Player == PlayerInput.all[1]:
                PlayerWin[0].Add(false);
                PlayerWin[1].Add(true);
                PlayerWin[2].Add(false);
                PlayerWin[3].Add(false);
                break;
            case var Player when Player == PlayerInput.all[2]:
                PlayerWin[0].Add(false);
                PlayerWin[1].Add(false);
                PlayerWin[2].Add(true);
                PlayerWin[3].Add(false);
                break;
            case var Player when Player == PlayerInput.all[3]:
                PlayerWin[0].Add(false);
                PlayerWin[1].Add(false);
                PlayerWin[2].Add(false);
                PlayerWin[3].Add(true);
                break;

        }
    }
}
