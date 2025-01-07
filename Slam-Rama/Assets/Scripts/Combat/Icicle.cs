using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{

    //Make the icenoise 
    public AudioSource iceWind;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the bomb to the correct rotation
        transform.Rotate(-90, -180, 0);
    }

    void EndPowerUp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collider is a player, is not a trigger, and is not the current players hitbox
        if (other.tag == "Player" && !other.isTrigger && other != GetComponent<CapsuleCollider>())
        {
            
            iceWind.Play();
            
            //Debug.Log("Collision Entry Detected");

            // Adds calls the freeze function on the player found
            other.GetComponent<Damage>().FreezePlayer();
        }
    }

    private void OnDestroy()
    {
        Destroy(transform.GetChild(1).gameObject);
    }
}
