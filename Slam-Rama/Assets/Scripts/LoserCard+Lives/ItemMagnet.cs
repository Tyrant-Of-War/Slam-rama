using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] float dragSpeed;
    Vector3 direction;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item") && GetComponentInParent<PlayerMovement>().playerData.itemID == 0)
        {
            direction = (transform.position - other.transform.position).normalized;
            other.transform.position += direction * Mathf.Pow(dragSpeed * Time.deltaTime, Vector3.Distance(transform.position, other.transform.position));  
        }
    }
}