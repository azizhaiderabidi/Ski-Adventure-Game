using Dreamteck.Forever;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimePengun : MonoBehaviour
{
    public static RuntimePengun inActive;
    public Runner runner;


    [SerializeField] Transform player;
    [SerializeField] Transform playerdummy;
    [SerializeField] float speed;
    [SerializeField] LayerMask layer;


    bool isActive = false;

    public Timer timer;

    private void Start()
    {
       // if (!LevelGenerator.instance.ready) gameObject.SetActive(false);

        player = FindObjectOfType<ProjectedPlayer>(true).transform;

        if (Physics.Raycast(transform.position + (Vector3.up * 10), Vector3.down, out RaycastHit hit, 100, layer))
        {
            transform.position = hit.point + (Vector3.up * 4);
            transform.up = hit.normal;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive) return;

        if (other.CompareTag("Player") || other.CompareTag("Head"))
        {
            isActive = true;

            if (inActive == null)
                inActive = this;
            else if (inActive != this)
            {
                inActive.gameObject.SetActive(false);

                inActive.timer.Kill();
                inActive.timer = null;

                inActive = this;
            }

            SetupPengun();
        }
    }

    void SetupPengun()
    {
        // setup powerup
        // SoundManager.Instance.PlaySFX(data.collectSound, .025f, .85f, 1.15f);
        // setup player
        player.gameObject.SetActive(false);
        playerdummy.gameObject.SetActive(true);        // setup pengunp;
        if(!LevelTracker.Current)
        {
            playerdummy.GetComponentInChildren<ModelLoadler>().SetPlayer();
            Debug.Log("Endless Dummy ");

        }
        else 
        {
            playerdummy.GetComponentInChildren<ModelLoadler>().LoadModel(LevelTracker.Current.levelIndex);
            Debug.Log("Level Dummy");
        }

        player.parent = playerdummy;

        runner.follow = true;
        runner.isPlayer = true;
        runner.followSpeed = speed;
        Runner.playerInstance = runner;

        FindObjectOfType<CameraFollow>().target = playerdummy;
        GetComponent<Collider>().isTrigger = false;

        timer = new Timer(5, () =>
        {
            // setup player
            GetComponent<Collider>().isTrigger = true;

            player.position = playerdummy.position;
            player.rotation = playerdummy.rotation;

            player.parent = null;
            playerdummy.gameObject.SetActive(false);
            player.gameObject.SetActive(true);
            // setup pengun;

            runner.follow = false;
            runner.isPlayer = false;
            runner.followSpeed = 0;
            Runner.playerInstance = null;

            FindObjectOfType<CameraFollow>().target = player;

            inActive = null;
        });
    }
}
