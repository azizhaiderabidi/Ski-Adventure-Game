using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
           // Invoke(nameof(GameOver), .7f);
           // Debug.Log("GameOver");
        }
    }

    void GameOver()
    {
        GameManager.Instance.GameOver();
    }
}
