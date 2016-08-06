using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour
{
    ParticleSystem _particleSystem;

    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    
    void Update()
    {
        if (_particleSystem.IsAlive() == false)
        {
            Destroy(gameObject);
        }
    }
}
