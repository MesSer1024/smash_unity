using UnityEngine;
using System.Collections;
using System;

public class PistolShot : MonoBehaviour, IShot
{
    public float Speed = 10f;
    public float LifeTime = 1f;
    public float Radius = 1;
    public LayerMask HitMask;
    public ParticleSystem ImpactParticleSystemPrefab;

    private Rigidbody _rigidbody;
    private float _lifetime;
    private Vector3 _lastPosition;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Spawn(Vector3 startPos, Vector3 endPos)
    {
        endPos.y = startPos.y;
        Vector3 aimDirection = (endPos - startPos).normalized;
        _rigidbody.position = startPos;
        transform.rotation = Quaternion.LookRotation(aimDirection);
        _lifetime = LifeTime;
        _lastPosition = startPos;

        var colliders = Physics.OverlapSphere(startPos, Radius, HitMask);
        foreach (var collider in colliders)
        {
            Life lifeComponent = collider.GetComponent<Life>();
            if (lifeComponent != null)
            {
                lifeComponent.DoDamage(50);
            }
            
            DestroyProjectile(startPos);
        }
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * Speed;

        Vector3 rayVector = (_rigidbody.position - _lastPosition);
        Ray ray = new Ray(_lastPosition, rayVector.normalized);
        var rayCastHits = Physics.SphereCastAll(ray, Radius, rayVector.magnitude, HitMask);
        foreach (var hit in rayCastHits)
        {
            Life lifeComponent = hit.collider.GetComponent<Life>();
            if (lifeComponent != null)
            {
                lifeComponent.DoDamage(50);
            }

            Vector3 impactPos = ray.origin + ray.direction * hit.distance;
            DestroyProjectile(impactPos);
            return;
        }

        _lifetime -= Time.fixedDeltaTime;
        if (_lifetime <= 0)
            DestroyProjectile(transform.position);

        _lastPosition = _rigidbody.position;
    }
    
    void DestroyProjectile(Vector3 impactPos)
    {
        Instantiate(ImpactParticleSystemPrefab, impactPos, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
