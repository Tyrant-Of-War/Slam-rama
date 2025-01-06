using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemMagnet : MonoBehaviour
{
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] float dragSpeed;
    Vector3 direction;

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (GetComponentInParent<PlayerMovement>().playerData.loserCardID == 1)
        {
            sphereCollider.enabled = true;
        }
        else
        {
            sphereCollider.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item") && GetComponentInParent<PlayerMovement>().playerData.itemID == 0)
        {
            direction = (transform.position - other.transform.position).normalized;
            other.transform.position += direction * Mathf.Pow(dragSpeed * Time.deltaTime, Vector3.Distance(transform.position, other.transform.position));  
        }
    }
}