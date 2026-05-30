using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public static ParticlePlayer Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }


    public List<ParticleSystem> explodions = new List<ParticleSystem>();
    List<ParticleSystem> _explodions = new List<ParticleSystem>();


    private void Start()
    {
        foreach (ParticleSystem p in explodions)
        {
            ParticleSystem particleSystem = Instantiate(p, transform);
            _explodions.Add(particleSystem);
        }
    }

    public void PlayExplodion(Vector3 pos)
    {
        ParticleSystem particleSystem = _explodions[0];

        particleSystem.transform.position = pos;

        particleSystem.Play();
    }
}
