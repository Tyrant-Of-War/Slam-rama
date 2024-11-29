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
    int totalDead;
    [SerializeField] InGameUI gameUI;

    // Is used to stop the reset round being called a bunch of times over and over again
    bool isResetting;

    private void Start()
    {

    }

    private void Awake()
    {
        isResetting = false;

        // Gets the current player count from the input system
        playerCount = UnityEngine.InputSystem.PlayerInput.all.Count;

        // Checks if any players exist
        if (playerCount != 0)
        {
            // Runs through all players and changes all the data that they need changed per scene such as the level data
            for (int i = 0; i < playerCount; i++)
            {
                UnityEngine.InputSystem.PlayerInput.all[i].SwitchCurrentActionMap("Player");
                AssignPlayerData(UnityEngine.InputSystem.PlayerInput.all[i], UnityEngine.InputSystem.PlayerInput.all[i].gameObject.GetComponent<Damage>().playerData, true);
            }
        }
    }

    private void LateUpdate()
    {
        if (!isResetting)
        {
            foreach (PlayerData player in playerData)
            {
                switch (player.isDead)
                {
                    case true:
                        totalDead++;
                        break;
                    case false:
                        break;
                }


                if (totalDead == UnityEngine.InputSystem.PlayerInput.all.Count - 1 && totalDead != 0)
                {
                    ResetRound();

                    roundData.roundsLeft--;

                    isResetting = true;
                }
            }
        }
        

        totalDead = 0;  
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
        Debug.Log("Assigned player data:" + playerData.ID);

        // Checks if the player was already created or not
        switch (Existing)
        {
            case false:

                // Assigns the player data to every script that needs it 
                input.GetComponent<Attack>().playerData = playerData;
                input.GetComponent<PlayerMovement>().playerData = playerData;
                input.GetComponent<Damage>().playerData = playerData;
                input.GetComponent<UseItem>().playerData = playerData;
                input.GetComponent<Knockback>().playerData = playerData;
                input.GetComponent<Knockout>().playerData = playerData;

                // Gives the player data the object it now belongs to
                playerData.PlayerObject = input.gameObject;
                break;
        }
        input.SwitchCurrentActionMap("Player");
        // Enables the player movement (when does it get disabled?)
        input.GetComponent<PlayerMovement>().enabled = true;

        // Assigns the level data to the scripts that use it
        input.GetComponent<Knockout>().levelData = levelData;
        input.GetComponent<Knockout>().gameUI = gameUI;
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
        input.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
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
    private void ResetRound()
    {

        if (roundData.roundsLeft <= 0)
        {
            SceneManager.LoadScene("Results");
        }
        else
        {
            foreach (var player in playerData)
            {
                player.ResetData();
            }
            if (roundData.RandomRounds)
            {
                int scene = Random.Range(0, 4);
                switch (scene)
                {
                    case 1:
                        SceneManager.LoadSceneAsync("Boxing");
                        break;
                    case 2:
                        SceneManager.LoadSceneAsync("Clock");
                        break;
                    case 3:
                        SceneManager.LoadSceneAsync("Boxing");
                        break;
                    case 4:
                        SceneManager.LoadSceneAsync("Clock");
                        break;

                }
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}


//roundData.ResetData(); need to use when the player has picked a powerup later in the game. 

