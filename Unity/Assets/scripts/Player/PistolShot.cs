using UnityEngine;
using System.Collections;
using System;

public class PistolShot : MonoBehaviour, IShot
{
    public float Speed = 10f;
    public float LifeTime = 1f;
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
        transform.position = startPos;
        transform.rotation = Quaternion.LookRotation(aimDirection);
        _lifetime = LifeTime;
        _lastPosition = startPos;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * Speed;

        Vector3 rayVector = (_rigidbody.position - _lastPosition);
        Ray ray = new Ray(_rigidbody.position, rayVector.normalized);
        var rayCastHits = Physics.RaycastAll(ray, rayVector.magnitude, HitMask);
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

    //void OnTriggerEnter(Collider collider)
    //{
    //    Life lifeComponent = collider.GetComponent<Life>();
    //    if (lifeComponent != null)
    //    {
    //        lifeComponent.DoDamage(50);
    //    }

    //    DestroyProjectile();
    //}

    void DestroyProjectile(Vector3 impactPos)
    {
        Instantiate(ImpactParticleSystemPrefab, impactPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
