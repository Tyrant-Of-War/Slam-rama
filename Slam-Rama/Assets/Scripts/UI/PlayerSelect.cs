using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();
    int playerCount;
    [SerializeField] LevelData levelData;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
        //input.GetComponent<Knockout>().playerData = playerData;

        //// Assigns the level data to the knockout script
        //input.GetComponent<Knockout>().levelData = levelData;

        // Assigns the correct material to the player
        input.GetComponent<MeshRenderer>().material = playerData.playerMaterial;
        input.GetComponent<PlayerMovement>().enabled = false;
        input.transform.position = new Vector3(levelData.SpawnLocation[UnityEngine.InputSystem.PlayerInput.all.Count - 1].x, 0);
    }
}
