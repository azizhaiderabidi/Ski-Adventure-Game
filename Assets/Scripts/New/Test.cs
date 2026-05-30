using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Button]
   public void SetPosition()
    {
        transform.position = GameManager.Instance.lastPlayerPos.position;
    }
}
