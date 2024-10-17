using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    BoxCollider attackHitbox;

    List<GameObject> attackTargets = new List<GameObject>();

    private void Start()
    {
        attackHitbox = GetComponent<BoxCollider>();
    }

    void OnAttack(InputValue inputValue)
    {
        if (inputValue.Get<float>() == 1)
        {
            LightAttack();
        }
        else if (inputValue.Get<float>() == -1)
        {
            HeavyAttack();
        }
    }

    void LightAttack()
    {
        Debug.Log("Light Attack");

        for (int i = 0; i < attackTargets.Count; i++)
        {
            Debug.Log(attackTargets[i].name);

            attackTargets[i].GetComponent<Knockback>().RunKnockback(this.transform.forward, 10);

            attackTargets.RemoveAt(i);
        }
    }

    void HeavyAttack()
    {
        Debug.Log("Heavy Attack");

        for (int i = 0; i < attackTargets.Count; i++)
        {
            Debug.Log(attackTargets[i].name);

            attackTargets[i].GetComponent<Knockback>().RunKnockback(this.transform.forward, 30);

            attackTargets.RemoveAt(i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger && other != this.GetComponent<CapsuleCollider>())
        {
            Debug.Log("Collision Entry Detected");

            attackTargets.Add(other.gameObject);

            for (int i = 0; i < attackTargets.Count; i++)
            {
                Debug.Log(attackTargets[i].name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (attackTargets.Contains(other.gameObject))
        {
            Debug.Log(other.name + " Has Exited Collision");

            attackTargets.Remove(other.gameObject);
        }
    }
}
