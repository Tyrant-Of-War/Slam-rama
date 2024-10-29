using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();
    int playerCount;
    [SerializeField] LevelData levelData;

    int playersWithLives;

    void FixedUpdate()
    {

    }

    // Is called when a player joins and checks which player it is
    public void PlayerSetup(UnityEngine.InputSystem.PlayerInput input)
    {
        playerCount = UnityEngine.InputSystem.PlayerInput.all.Count;

        switch (playerCount)
        {
            case 1:
                AssignPlayerData(input, playerData[0]);
                break;
            case 2:
                AssignPlayerData(input, playerData[1]);
                break;
            case 3:
                AssignPlayerData(input, playerData[2]);
                break;
            case 4:
                AssignPlayerData(input, playerData[3]);
                break;
        }
    }

    // Assigns the player and level data to the various scripts on each player object
    private void AssignPlayerData(UnityEngine.InputSystem.PlayerInput input, PlayerData playerData)
    {
        // Assigns player data to the various scripts
        input.GetComponent<Damage>().playerData = playerData;
        input.GetComponent<Knockback>().playerData = playerData;
        input.GetComponent<Knockout>().playerData = playerData;

        // Assigns the level data to the knockout script
        input.GetComponent<Knockout>().levelData = levelData;

        // Assigns the correct material to the player
        input.GetComponent<MeshRenderer>().material = playerData.playerMaterial;
    }

    public void CheckRoundEnd()
    {
        playersWithLives = 0;
        PlayerData lastPlayerStanding = null;

        foreach (PlayerData player in playerData)
        {
            if (player.lives > 0)
            {
                playersWithLives++;
                lastPlayerStanding = player;
            }
        }

        if (playersWithLives <= 1)
        {
            EndRound(lastPlayerStanding);
        }
    }


    private void EndRound(PlayerData winner)
    {
        if (winner != null)
        {
            SceneManager.LoadScene("LoserCards");
        }
    }
}


