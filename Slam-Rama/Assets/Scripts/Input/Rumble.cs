using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumble : MonoBehaviour
{
    // For getting the right controller to rumble
    public PlayerData playerData;

    public void SetRumble(float power, float duration)
    {
        Debug.Log("----Rumble Details----");
        Debug.Log("Power: " + power + ", Duration: " + duration);

        // Set the controller attached to this player to vibrate with the given power
        playerData.playerController.SetMotorSpeeds(power, power);

        // Invoke the rumble to stop after the set duration passes
        Invoke("StopRumble", duration);
    }

    private void StopRumble()
    {
        // Stop the vibration
        playerData.playerController.SetMotorSpeeds(0, 0);
    }
}
