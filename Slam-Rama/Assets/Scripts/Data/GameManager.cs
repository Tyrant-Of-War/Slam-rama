using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();
    int playerCount;
    [SerializeField] LevelData levelData;

    int playersWithLives;

    private void Start()
    {
        playerCount = UnityEngine.InputSystem.PlayerInput.all.Count;
        if (playerCount != 0)
        {
            for (int i = 0; i < playerCount; i++)
            {
                UnityEngine.InputSystem.PlayerInput.all[i].SwitchCurrentActionMap("Player");
                AssignPlayerData(UnityEngine.InputSystem.PlayerInput.all[i], UnityEngine.InputSystem.PlayerInput.all[i].gameObject.GetComponent<Damage>().playerData, true);
            }
        }
    }

    // Is called when a player joins and checks which player it is
    public void PlayerSetup(UnityEngine.InputSystem.PlayerInput input)
    {
        playerCount = UnityEngine.InputSystem.PlayerInput.all.Count;

        switch (playerCount)
        {
            case 1:
                AssignPlayerData(input, playerData[0], false);
                break;
            case 2:
                if (input.GetComponent<Damage>().playerData == null)
                    AssignPlayerData(input, playerData[1], false);
                break;
            case 3:
                if (input.GetComponent<Damage>().playerData == null)
                    AssignPlayerData(input, playerData[2], false);
                break;
            case 4:
                if (input.GetComponent<Damage>().playerData == null)
                    AssignPlayerData(input, playerData[3], false);
                break;
        }
    }

    // Assigns the player and level data to the various scripts on each player object
    private void AssignPlayerData(UnityEngine.InputSystem.PlayerInput input, PlayerData playerData, bool Existing)
    {
        input.GetComponent<PlayerMovement>().playerData = playerData;
        switch (Existing)
        {
            case false:
                input.GetComponent<Damage>().playerData = playerData;

                input.GetComponent<Knockback>().playerData = playerData;
                input.GetComponent<Knockout>().playerData = playerData;
                input.GetComponent<MeshRenderer>().material = playerData.playerMaterial;
                playerData.PlayerObject = input.gameObject;
                break;
        }
        // Assigns player data to the various scripts
        input.transform.GetChild(0).gameObject.SetActive(false);
        input.GetComponent<PlayerMovement>().enabled = true;
        // Assigns the level data to the knockout script
        input.GetComponent<Knockout>().levelData = levelData;
        // Assigns the correct material to the player
        input.SwitchCurrentActionMap("Player");
        input.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

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
            SceneManager.LoadScene("LoserCards"); // need to change to this scene
        }
    }
}


