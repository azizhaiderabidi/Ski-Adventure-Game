using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject target;
    IEnumerator Start()
    {
        target.SetActive(false);
        yield return new WaitForSeconds(2);
        OnReady();
    }

    void OnReady()
    {
        if (Physics.Raycast(target.transform.position , Vector3.down , out RaycastHit hit , 100 ,layer))
        {
            target.transform.position = hit.point + Vector3.up;
            target.transform.up = hit.normal;
        }
        target.SetActive(true);

        target.transform.parent = null;
        GameManager.Instance.isReady = true;
       
    }
}
