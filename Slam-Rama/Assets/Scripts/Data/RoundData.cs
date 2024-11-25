using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RoundData : ScriptableObject
{
    //////////////
    
    // Should probably rework this entire thing

    //////////////

    [SerializeField] private PlayerData firstPlayerOut;

    [SerializeField] private PlayerData lastPlayerStanding;

    public PlayerData FirstPlayerOut => firstPlayerOut;
    public PlayerData LastPlayerStanding => lastPlayerStanding;

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
                lastPlayerStanding = player;
            }
            else if (firstPlayerOut == null)
            {
                firstPlayerOut = player;
            }
        }
    }

    // Checks if the round should end (only one player with lives left)
    public bool IsRoundOver()
    {
        return lastPlayerStanding != null && firstPlayerOut != null;
    }
}
