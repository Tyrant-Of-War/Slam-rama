using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // The direction the bomb should fly
    public Vector3 direction;

    // The time until the bomb explodes
    [SerializeField] float bombTimer;

    // The power with which the bomb should fly foward
    [SerializeField] int launchPower;

    // The rigidbody of the bomb
    Rigidbody bombRB;

    // List of players in the attack hitbox
    List<GameObject> explodeTargets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Gets the rigidbody
        bombRB = GetComponent<Rigidbody>();

        // Sets the bomb to the correct rotation
        transform.Rotate(-90, 0, 0);

        // Adds a force in the supplied direction
        bombRB.AddForce(((Vector3.up * 2f) + direction) * launchPower, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // Counts down bomb timer and destroys when it runs out
        if (bombTimer > 0)
        {
            bombTimer -= Time.deltaTime;
        }
        else
        {
            // Runs through each player in the collider radius and applies an explosion force
            for (int i = 0; i < explodeTargets.Count; ++i)
            {
                // Calls the knockback on the players in the radius
                explodeTargets[i].GetComponent<Knockback>().explodeKnockback(100f, transform.position, 2f);
                explodeTargets[i].GetComponent<Damage>().DamagePlayer(10);
            }

            // Clears the list
            explodeTargets.Clear();

            // Destroys the bomb
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collider is a player, is not a trigger, and is not the current players hitbox
        if (other.tag == "Player" && !other.isTrigger && other != GetComponent<CapsuleCollider>())
        {
            //Debug.Log("Collision Entry Detected");

            // Adds the collider game object to the target list
            explodeTargets.Add(other.gameObject);

            //for (int i = 0; i < attackTargets.Count; i++)
            //{
            //    Debug.Log(attackTargets[i].name);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if the exiting player is in the list
        if (explodeTargets.Contains(other.gameObject) && !other.isTrigger)
        {
            //Debug.Log(other.name + " Has Exited Collision");

            // Removes the player from the list
            explodeTargets.Remove(other.gameObject);
        }
    }
}
