using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AttackRangeBuff : MonoBehaviour
{
    private BoxCollider attackCollider;
    private void Start()
    {
        attackCollider = GetComponent<BoxCollider>(); //get the attack collider

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        Debug.Log("Scene change read by attack range buff");

        if (GetComponent<UseItem>().playerData.loserCardID == 2 && attackCollider.size.z == 3)
        {
            IncreaseAttackRange();
        }
        else if (GetComponent<UseItem>().playerData.loserCardID != 2 && attackCollider.size.z == 6)
        {
            DecreaseAttackRange();
        }
    }

    public void IncreaseAttackRange()
    {
        attackCollider.size = new Vector3(attackCollider.size.x, attackCollider.size.y, attackCollider.size.z * 2); // Double the size 
        attackCollider.center = new Vector3(attackCollider.center.x, attackCollider.center.y, attackCollider.center.z * 2); // Adjust the center 

        Debug.Log("Range has been multiplied");
    }

    public void DecreaseAttackRange()
    {
        attackCollider.size = new Vector3(attackCollider.size.x, attackCollider.size.y, attackCollider.size.z / 2); // half the size 
        attackCollider.center = new Vector3(attackCollider.center.x, attackCollider.center.y, attackCollider.center.z / 2); // Adjust the center 

        Debug.Log("Range has been unmultiplied");
    }
}
