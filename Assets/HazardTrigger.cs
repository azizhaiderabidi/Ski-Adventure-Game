using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTrigger : MonoBehaviour
{
    public float speedMin;
    public float speedMax;
    public Transform target;
    public Runner runner;

    public bool CanChase { get; set; }

    private void Start()
    {
        runner = GetComponent<Runner>();

    }

    private void LateUpdate()
    {
        float speed = Vector3.Distance(transform.position, target.position) > 200 && CanChase ? speedMax : speedMin;

        runner.followSpeed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.gameObject.name);
        if (other.GetComponentInParent<ProjectedPlayer>(true) != null || other.gameObject.name == "Head")
        {
            runner.isPlayer = true;
            Runner.playerInstance = runner;
            // GameManager.Instance.lastPlayerPos = target.position;
            GameManager.Instance.GameOver();
        }

       
    }
}
