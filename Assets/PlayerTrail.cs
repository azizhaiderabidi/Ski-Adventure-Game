using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField] bool CheckGround;
    [SerializeField] float radius;
    [SerializeField] LayerMask groundMask;
    [SerializeField] ParticleSystem trail;

    private void LateUpdate()
    {
        if (CheckGround)
        {
            if (Physics.CheckSphere(transform.position , radius, groundMask))
            {
                PlayTrail();
            }
            else
            {
                trail.Stop();
            }
        }
        else
        {
            PlayTrail();
        }
    }

    Vector3 PrePos = Vector3.zero;
    
    void PlayTrail()
    {
        if (PrePos != transform.position)
        {
            PrePos = transform.position;
            trail.Play();
        }
        else
        {
            trail.Stop();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (CheckGround)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
