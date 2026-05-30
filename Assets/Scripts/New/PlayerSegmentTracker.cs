using UnityEngine;
using Dreamteck.Forever;
using System.Collections.Generic;

public class PlayerSegmentTracker : MonoBehaviour
{
    public Transform pos;
    public LevelSegment segment;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerPosition();
            GameManager.Instance.currentSegment = segment;
           // Debug.Log("New Segment Position Set");
        }
    }


    public void SetPlayerPosition()
    {
        GameManager.Instance.lastPlayerPos = pos;
    }



}
