using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // A list of all player datas that will be used for assinging
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();

    // The current count of players in the game
    int playerCount;

    // The level data of the level this game manager occupies
    [SerializeField] LevelData levelData;

    // The round data that the players can configurate
    [SerializeField] RoundData roundData;

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
        // Checks which number player is joining
        playerCount = UnityEngine.InputSystem.PlayerInput.all.Count;

        // Runs the assiging of player data feeding the correct number data to the correct number player
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
        // Checks if the player was already created or not
        switch (Existing)
        {
            case false:

                // Assigns the player data to every script that needs it 
                input.GetComponent<PlayerMovement>().playerData = playerData;
                input.GetComponent<Damage>().playerData = playerData;
                input.GetComponent<UseItem>().playerData = playerData;
                input.GetComponent<Knockback>().playerData = playerData;
                input.GetComponent<Knockout>().playerData = playerData;

                // Gives the player data the object it now belongs to
                playerData.PlayerObject = input.gameObject;

                // Sets the correct control scheme to the player
                input.SwitchCurrentActionMap("Player");

                break;
        }

        // ????
        input.transform.GetChild(0).gameObject.SetActive(false);

        // Enables the player movement (when does it get disabled?)
        input.GetComponent<PlayerMovement>().enabled = true;

        // Assigns the level data to the scripts that use it
        input.GetComponent<Knockout>().levelData = levelData;
        input.GetComponent<Rigidbody>().position = levelData.SpawnLocation[playerData.ID - 1];

        // ????
        input.GetComponentInChildren<MultiplayerEventSystem>().gameObject.SetActive(false);
        input.transform.GetChild(0).gameObject.SetActive(true);

        // Enables the animated character models renderer
        input.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

        // Assigns the right color for the player
        input.GetComponentInChildren<SkinnedMeshRenderer>().material = playerData.playerMaterial;

        // Sets the player to not dead
        playerData.isDead = false;
    }

    public void CheckRoundEnd()
    {
        // Update the round data based on the current player data
        roundData.UpdateData(playerData);
        // Check if the round should end
        if (roundData.IsRoundOver())
        {
            EndRound(roundData.LastPlayerStanding);
        }
    }

    private void EndRound(PlayerData winner)
    {
        if (winner != null)
        {
            SceneManager.LoadScene("LoserCards");
        }
    }

    public void ApplyPowerUp(LooserCardPowers.PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case LooserCardPowers.PowerUpType.ItemMagnet:

                // Need to put in item magnet logic
                break;

            case LooserCardPowers.PowerUpType.AttackRangeBuff:
                // Need to put in attack range logic
                break;

            case LooserCardPowers.PowerUpType.RecoveryJump:
                // Need to put in recovery jUMp logic
                break;
        }
    }

}


//roundData.ResetData(); need to use when the player has picked a powerup later in the game. 

