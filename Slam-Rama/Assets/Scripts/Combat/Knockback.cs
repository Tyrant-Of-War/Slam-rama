using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    public void RunKnockback(Vector3 direction, int force)
    {
        Debug.Log("Force Applied");

        playerRB.AddForce(direction * force, ForceMode.Impulse);
    }
}
