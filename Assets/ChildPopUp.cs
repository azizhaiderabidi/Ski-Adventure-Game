using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChildPopUp : MonoBehaviour
{
    [SerializeField] float delayBtwChild;
    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            float target = transform.GetChild(i).localPosition.y;

            transform.GetChild(i).localPosition = new Vector3(transform.GetChild(i).localPosition.x, target - 500, transform.GetChild(i).localPosition.z);

            transform.GetChild(i).DOLocalMoveY(target,1f).SetDelay(delayBtwChild * (i + 1));
        }
    }
}
