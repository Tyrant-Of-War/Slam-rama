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

    public bool RandomRounds;

    [SerializeField] private PlayerData firstPlayerOut;

    [SerializeField] private PlayerInput lastPlayerStanding;

    public PlayerData FirstPlayerOut => firstPlayerOut;
    public PlayerInput LastPlayerStanding => lastPlayerStanding;

    public AudioSource roundWinner;

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
        roundsLeft = 3; lastPlayerStanding = null;
        RandomRounds = true;
        roundType = RoundType.Random;
    }
    // Resets the tracker data for each new round
    public void ResetData()
    {
        firstPlayerOut = null;
        lastPlayerStanding = null;
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
}
