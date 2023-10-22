using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int damage;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * maxDistance, Color.red);
    }


    private void Shoot()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out raycastHit, maxDistance, layerMask))
        {
            Debug.Log("Hit: " + raycastHit.collider.gameObject);
            raycastHit.collider.gameObject.GetComponent<Enemy>().ReduceHealth(damage);
        }
    }
}
