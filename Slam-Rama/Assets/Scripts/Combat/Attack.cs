using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    BoxCollider attackHitbox;

    List<Rigidbody> attackTargets = new List<Rigidbody>();

    private void Start()
    {
        attackHitbox = GetComponent<BoxCollider>();

        attackHitbox.enabled = false;
    }

    void OnAttack(InputValue inputValue)
    {
        attackHitbox.enabled = true;

        Debug.Log(attackHitbox.enabled);

        if (inputValue.Get<float>() == 1)
        {
            Invoke("LightAttack", 2f);
        }
        else if (inputValue.Get<float>() == -1)
        {
            Invoke("HeavyAttack", 2f);
        }
    }

    void LightAttack()
    {
        Debug.Log("Light Attack");

        attackTargets.Clear();

        attackHitbox.enabled = false;
    }

    void HeavyAttack()
    {
        Debug.Log("Heavy Attack");

        attackTargets.Clear();

        attackHitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            Debug.Log("Collision Entry Detected");

            attackTargets.Add(other.GetComponent<Rigidbody>());

            Debug.Log(attackTargets.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            Debug.Log("Collision Exit Detected");

            attackTargets.Remove(other.GetComponent<Rigidbody>());

            Debug.Log(attackTargets.Count);
        }
    }
}
