using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public PlayerData playerData;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RunKnockback(Vector3 direction, float multiplier)
    {
        Debug.Log("Force Applied");

        rb.AddForce(playerData.damage * multiplier * direction, ForceMode.Impulse);
    }
}
