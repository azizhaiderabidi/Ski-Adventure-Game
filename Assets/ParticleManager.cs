using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ParticleSystem WoodExplodion;
    [SerializeField] ParticleSystem CloudExplodion;
    [SerializeField] ParticleSystem StoneExplodion;


    public void Play(Vector3 pos , ParticleEffect effect)
    {
        switch (effect)
        {
            case ParticleEffect.WoodExplodion:

                WoodExplodion.transform.position = pos;
                WoodExplodion.Play();

                break;
            case ParticleEffect.CloudExplodion:

                CloudExplodion.transform.position = pos;
                CloudExplodion.Play();

                break;
            case ParticleEffect.StoneExplodion:

                StoneExplodion.transform.position = pos;
                StoneExplodion.Play();

                break;
        }
    }
}

public enum ParticleEffect
{
    WoodExplodion,
    CloudExplodion,
    StoneExplodion
}
