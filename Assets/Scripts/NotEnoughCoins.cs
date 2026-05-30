using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughCoins : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke(nameof(OnDisappear), 1f);
    }

    private void OnDisappear()
    {
       gameObject.SetActive(false);
    }
}
