using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float followSpeed;
    [SerializeField] LayerMask mask;


    void Update()
    {
        if (!PowerUpHandler.HasMagnet) return;

        Collider[] collisions = Physics.OverlapSphere(transform.position, radius, mask, QueryTriggerInteraction.Collide);

        foreach (var item in collisions)
        {
            if (item.GetComponent<RuntimeCoin>() != null)
            {
                item.transform.position = Vector3.Lerp(item.transform.position, transform.position, followSpeed * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
