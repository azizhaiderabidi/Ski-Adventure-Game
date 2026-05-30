using DG.Tweening;
using UnityEngine;

public class MoveOnZAxis : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 10f;

    void Start()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.DOMoveZ(distance, speed).SetEase(Ease.Linear).OnComplete(MoveBackward);
    }

    void MoveBackward()
    {
        transform.DOMoveZ(-distance, speed).SetEase(Ease.Linear).OnComplete(MoveForward);
    }
}
