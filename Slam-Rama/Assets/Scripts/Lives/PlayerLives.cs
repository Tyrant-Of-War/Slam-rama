using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
 public PlayerData playerData; // Reference to PlayerData ScriptableObject
 private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

   
    public void LoseLife()
    {
        playerData.lives--;  

        if (playerData.lives <= 0)
        {
            playerData.lives = 0; 
            gameManager.CheckRoundEnd(); 
        }
    }
}
