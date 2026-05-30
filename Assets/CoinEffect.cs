using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    public static CoinEffect instance;

    [SerializeField] ParticleSystem coinParticle;


    Camera cam;

    
    
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        cam = Camera.main;
    }

    public void Emit(Vector3 pos, int amount = 1)
    {
        coinParticle.transform.parent.position = pos;
        coinParticle.Emit(amount);
    }


    public void EmitInWorld(Vector3 worldPos ,int amount = 1)
    {
        Vector3 pos = cam.WorldToScreenPoint(worldPos);

        coinParticle.transform.parent.position = pos;
        coinParticle.Emit(amount);
    }
}