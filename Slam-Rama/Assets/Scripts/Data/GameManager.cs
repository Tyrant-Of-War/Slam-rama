using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();

    int playerCount;

    [SerializeField] LevelData levelData;

    void Start()
    {
        
    }

    void Update()
    {
        if (playerData[0].playerY < levelData.killHeight)
        {
            Debug.Log("Player Kill");
        }
    }

    public void playerSetup(UnityEngine.InputSystem.PlayerInput input)
    {
        playerCount = UnityEngine.InputSystem.PlayerInput.all.Count;

        switch(playerCount)
        {
            case 1:
                input.GetComponent<Damage>().playerData = playerData[0];
                input.GetComponent<Knockback>().playerData = playerData[0];
                input.GetComponent<MeshRenderer>().material = playerData[0].playerMaterial;
                break;
            case 2:
                input.GetComponent<Damage>().playerData = playerData[1];
                input.GetComponent<Knockback>().playerData = playerData[1];
                input.GetComponent<MeshRenderer>().material = playerData[1].playerMaterial;
                break;
            case 3:
                input.GetComponent<Damage>().playerData = playerData[2];
                input.GetComponent<Knockback>().playerData = playerData[2];
                input.GetComponent<MeshRenderer>().material = playerData[2].playerMaterial;
                break;
            case 4:
                input.GetComponent<Damage>().playerData = playerData[3];
                input.GetComponent<Knockback>().playerData = playerData[3];
                input.GetComponent<MeshRenderer>().material = playerData[3].playerMaterial;
                break;
        }
    }
}
