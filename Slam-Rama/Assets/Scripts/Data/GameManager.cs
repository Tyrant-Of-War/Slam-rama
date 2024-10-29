using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();
    int playerCount;
    [SerializeField] LevelData levelData;

    void Start()
    {

    }

    void FixedUpdate()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerData[i].playerY < levelData.killHeight)
            {
                PlayerDeathHandler(i);
            }
        }
        CheckRoundEnd();
    }
    public void playerSetup(UnityEngine.InputSystem.PlayerInput input)
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
    private void AssignPlayerData(UnityEngine.InputSystem.PlayerInput input, PlayerData data)
    {
        input.GetComponent<Damage>().playerData = data;
        input.GetComponent<Knockback>().playerData = data;
        input.GetComponent<MeshRenderer>().material = data.playerMaterial;
    }

    private void PlayerDeathHandler(int playerIndex)
    {
        PlayerData currentPlayer = playerData[playerIndex];
        currentPlayer.lives--;

        if (currentPlayer.lives <= 0)
        {
            currentPlayer.lives = 0;
        }
    }
    public void CheckRoundEnd()
    {
        int playersWithLives = 0;
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


