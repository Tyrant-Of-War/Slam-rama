using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackRangeBuff : MonoBehaviour
{
    private BoxCollider attackCollider;
    private void Start()
    {
        attackCollider = GetComponent<BoxCollider>(); //get the attack collider
    }

    public void IncreaseAttackRange()
    {

        attackCollider.size = new Vector3(attackCollider.size.x, attackCollider.size.y, attackCollider.size.z * 2); // Double the size 
        attackCollider.center = new Vector3(attackCollider.center.x, attackCollider.center.y, attackCollider.center.z * 2); // Adjust the center 
    }
}
