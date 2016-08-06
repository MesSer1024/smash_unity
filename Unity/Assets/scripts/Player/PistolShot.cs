using UnityEngine;
using System.Collections;

public class PistolShot : MonoBehaviour
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
