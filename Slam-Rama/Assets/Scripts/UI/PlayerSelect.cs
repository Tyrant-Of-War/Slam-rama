using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerSelect : MonoBehaviour
{
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();
    int playerCount;
    [SerializeField] LevelData levelData;
    [SerializeField] GameObject[] RootUI;
    // Is called when a player joins and checks which player it is
    public void PlayerSetup(UnityEngine.InputSystem.PlayerInput input)
    {
        playerCount = UnityEngine.InputSystem.PlayerInput.all.Count;

        switch (playerCount)
        {
            case 1:
                AssignPlayerData(input, playerData[0]);
                input.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = RootUI[0];
                input.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = RootUI[0].transform.GetChild(0).gameObject;
                break;
            case 2:
                AssignPlayerData(input, playerData[1]);
                input.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = RootUI[1];
                input.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = RootUI[1].transform.GetChild(0).gameObject;
                break;
            case 3:
                AssignPlayerData(input, playerData[2]);
                input.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = RootUI[2];
                input.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = RootUI[2].transform.GetChild(0).gameObject;
                break;
            case 4:
                AssignPlayerData(input, playerData[3]);
                input.GetComponentInChildren<MultiplayerEventSystem>().playerRoot = RootUI[3];
                input.GetComponentInChildren<MultiplayerEventSystem>().firstSelectedGameObject = RootUI[3].transform.GetChild(0).gameObject;
                break;
        }
    }

    // Assigns the player and level data to the various scripts on each player object
    private void AssignPlayerData(UnityEngine.InputSystem.PlayerInput input, PlayerData playerData)
    {
        playerData.ResetData();

        // Assigns player data to the various scripts
        input.GetComponent<Damage>().playerData = playerData;
        input.GetComponent<Knockback>().playerData = playerData;
        input.GetComponent<Knockout>().playerData = playerData;
        input.GetComponent<Attack>().playerData = playerData;
        input.GetComponent<PlayerMovement>().playerData = playerData;
        input.GetComponent<UseItem>().playerData = playerData;
        input.GetComponent<Rumble>().playerData = playerData;
        input.GetComponent<PlayerCosmeticInput>().enabled = true;
        // Assigns the level data to the knockout script
        input.GetComponent<Knockout>().levelData = levelData;
        input.GetComponent<PlayerMovement>().levelData = levelData;

        // Assigns the correct material to the player
        input.GetComponentInChildren<SkinnedMeshRenderer>().material = playerData.playerMaterial;
        input.GetComponent<PlayerMovement>().enabled = false;

        input.GetComponent<Rigidbody>().position = new Vector3(levelData.SpawnLocation[UnityEngine.InputSystem.PlayerInput.all.Count - 1].x, 0);
        input.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerData.PlayerObject = input.gameObject;

        if (input.GetDevice<Gamepad>() != null)
        {
            playerData.playerController = input.GetDevice<Gamepad>();
        }
    }


}
