using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingRopes : MonoBehaviour
{
    // Timer used to tell if the ropes should go down
    [SerializeField] float timer;

    // The collider for the ropes
    [SerializeField] MeshCollider ropeCollider;

    // Used to tell if the ropes should be lowered
    bool lowerRopes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }
        else 
        {
            lowerRopes = true;

            ropeCollider.enabled = false;
        }

        if (lowerRopes && transform.position.y > -20)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * 3), transform.position.z);
        }
    }

    
}
