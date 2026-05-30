using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEvents;

public class Hazard : MonoBehaviour
{
    public float minWarningDistance = 250;
    public HazardTrigger hazard;

    // public  OnHazardRequest;

    GameEvents.HazardState state = new HazardState(false);

    private void OnEnable()
    {
        EventManager.Register<GameEvents.StartHazard>(StartHazard);
        EventManager.Register<GameEvents.StopHazard>(StopHazard);
        EventManager.Register<GameEvents.GameOver>(GameOver);
    }
    private void OnDisable()
    {
        EventManager.Unregister<GameEvents.StartHazard>(StartHazard);
        EventManager.Unregister<GameEvents.StopHazard>(StopHazard);
        EventManager.Unregister<GameEvents.GameOver>(GameOver);

    }
    void Start()
    {
        hazard.gameObject.SetActive(false);
        
        
    }

    private void Update()
    {
        state.IsClose = hazard.CanChase && Vector3.Distance(hazard.transform.position , hazard.target.position) < minWarningDistance;
        EventManager.Raise(state);
    }

    public void StartHazard(GameEvents.StartHazard _hazard)
    {
        Debug.Log("Start Hazard Called");
        
        new Timer(5, () => {
            Runner.playerInstance = hazard.runner;
            hazard.runner.follow = true;
           // hazard.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            hazard.gameObject.SetActive(true);
           // hazard.GetComponent<SphereCollider>().enabled = true;

            Debug.Log("Hazard Start"); 
        
        } , true);
        hazard.CanChase = true;

       // SoundManager.Instance.UpdateMusicVolume(0.15f);

    }
    public void StopHazard(GameEvents.StopHazard _hazard)
    {
        Debug.Log("Hazard Stopped");
        //hazard.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        //Runner.playerInstance = null;
        //hazard.runner.follow = false;
        //hazard.runner.isPlayer = false;
        //hazard.GetComponent<SphereCollider>().enabled = false;
        //if (LevelGenerator.instance != null && LevelGenerator.instance.ready)
        //{
        //    int segmentIndex = 0;
        //    double localPercent = LevelGenerator.instance.GlobalToLocalPercent(hazard.runner.startPercent, out segmentIndex);

        //    LevelSegment currentSegment = GameManager.Instance.currentSegment;
        //    if (currentSegment != null)
        //    {

        //        hazard.runner.Init(currentSegment, localPercent);
        //        hazard.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("LevelGenerator not ready. Can't reset hazard position.");
        //}
       
        hazard.gameObject.SetActive(false);
    }
    public void GameOver(GameEvents.GameOver _gameOver)
    {
        hazard.CanChase = false;
        
        SoundManager.Instance.ResetMusic();

    }

}
