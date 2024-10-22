using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerCamScript : MonoBehaviour
{
    public CinemachineTargetGroup MainCamera;
    private List<UnityEngine.InputSystem.PlayerInput> activePlayers = new List<UnityEngine.InputSystem.PlayerInput>();

    private void Start()
    {
        // Add any existing players to the Target Group at the start
        foreach (UnityEngine.InputSystem.PlayerInput playerInput in UnityEngine.InputSystem.PlayerInput.all)
        {
            AddPlayerToTargetGroup(playerInput);
        }
    }

    public void OnPlayerJoined(UnityEngine.InputSystem.PlayerInput input)
    {
        // Add the new player to the Target Group
        AddPlayerToTargetGroup(input);
    }

    public void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput input)
    {
        // Remove the player from the Target Group
        RemovePlayerFromTargetGroup(input);
    }

    private void AddPlayerToTargetGroup(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        // Ensure the player has a valid Transform and is not already in the list
        if (playerInput != null && !activePlayers.Contains(playerInput))
        {
            activePlayers.Add(playerInput);
            MainCamera.AddMember(playerInput.transform, 1.0f, 0); // Add to Target Group
        }
    }

    private void RemovePlayerFromTargetGroup(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        // Ensure the player is in the active list
        if (activePlayers.Contains(playerInput))
        {
            activePlayers.Remove(playerInput);
            MainCamera.RemoveMember(playerInput.transform); // Remove from Target Group
        }
    }
}
