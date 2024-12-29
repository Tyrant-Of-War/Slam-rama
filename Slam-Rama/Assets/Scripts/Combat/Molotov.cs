using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    // The direction the molotov should fly
    public Vector3 direction;

    // The power with which the molotov should fly foward
    [SerializeField] int launchPower;

    // To tell if the counter should start going
    bool hasExploded;

    // The time for which the fire should exist
    [SerializeField] float moloDuration;

    // The particles for the fire
    [SerializeField] ParticleSystem fireParticles;

    // The collider for the physical object
    BoxCollider boxCollider;

    // The collider for the fire
    SphereCollider sphereCollider;

    // The rigidbody for freezing the object after explosion
    Rigidbody moloRB;

    // The list of all players ignited so they can be extinguished when this object is destroyed
    List<Damage> ignitedPlayers = new List<Damage>();

    // Start is called before the first frame update
    void Start()
    {
        // Gets the rigidbody
        moloRB = GetComponent<Rigidbody>();

        // Get colliders
        boxCollider = GetComponent<BoxCollider>();
        sphereCollider = GetComponent<SphereCollider>();

        // Disables sphere collider as it has not exploded yet
        sphereCollider.enabled = false;

        // Sets the bomb to the correct rotation
        transform.Rotate(-90, 0, 0);

        // Adds a force in the supplied direction
        moloRB.AddForce(((Vector3.up * 2f) + direction) * launchPower, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the bomb has explode
        if (hasExploded)
        {
            // Checks if there is still time left on the fire duration, destorys the bomb if not
            if (moloDuration > 0)
            {
                moloDuration = moloDuration - Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Checks if the molo is below a certain height (the player threw it off the map) and destroys if so
        if (transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Checks if the collided is the ground
        if (collision.gameObject.layer == 3)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Disable the bottle mesh
        GetComponent<MeshRenderer>().enabled = false;

        // Plays the visual
        fireParticles.Play();

        // Freeze the physics of the molotov
        moloRB.constraints = RigidbodyConstraints.FreezeAll;

        // Disable the physical collider
        boxCollider.enabled = false;

        // Enable the fire collider
        sphereCollider.enabled = true;

        // Tells update that the molotov has collided
        hasExploded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collider is a player, is not a trigger, and is not the current players hitbox
        if (other.tag == "Player" && !other.isTrigger)
        {
            //Debug.Log("Collision Entry Detected");

            // Sets the player to the ignited state
            other.GetComponent<Damage>().isIgnited = true;

            // Adds the player to list of players ignited
            ignitedPlayers.Add(other.GetComponent<Damage>());

            //for (int i = 0; i < attackTargets.Count; i++)
            //{
            //    Debug.Log(attackTargets[i].name);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if the exiting player is in the list
        if (other.tag == "Player" && !other.isTrigger)
        {
            //Debug.Log(other.name + " Has Exited Collision");

            // Extinguishes the player
            other.GetComponent<Damage>().isIgnited = false;

            // Adds the player to list of players ignited
            ignitedPlayers.Remove(other.GetComponent<Damage>());
        }
    }

    private void OnDestroy()
    {
        // Goes through and extinguishes all players still in the flames
        for (int i = 0; i < ignitedPlayers.Count; i++)
        {
            ignitedPlayers[i].isIgnited = false;
        }
    }

}
