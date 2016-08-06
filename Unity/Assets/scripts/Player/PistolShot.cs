using UnityEngine;
using System.Collections;
using System;

public class PistolShot : MonoBehaviour, IShot
{
    public float Speed = 10f;
    public float LifeTime = 1f;
    public ParticleSystem ImpactParticleSystemPrefab;

    private Rigidbody _rigidbody;
    private float _lifetime;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Spawn(Vector3 startPos, Vector3 endPos)
    {
        Vector3 aimDirection = (endPos - startPos).normalized;
        transform.position = startPos;
        transform.rotation = Quaternion.LookRotation(aimDirection);
    }

    void Start()
    {
        _lifetime = LifeTime;
    }
    
    void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * Speed;

        _lifetime -= Time.fixedDeltaTime;
        if (_lifetime <= 0)
            DestroyProjectile();
    }

    void OnTriggerEnter(Collider collider)
    {
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Instantiate(ImpactParticleSystemPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
