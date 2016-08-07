using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Grenade : MonoBehaviour, IShot
{
    public float TravelTime = 3f;
    public AnimationCurve HeightCurve = AnimationCurve.Linear(0, 0, 1, 0);
    public float Height = 10;
    public float ExplosionRadius = 3f;
    public float ExplosionTime = 0.5f;
    public LayerMask HitMask;

    public ParticleSystem ImpactParticleSystemPrefab;

    private float _spawnTime;
    private float _explosionTime;
    private bool _exploded;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    
    public void Spawn(Vector3 startPos, Vector3 endPos)
    {
        _spawnTime = Time.time;
        _startPosition = startPos;
        _endPosition = endPos;
    }

    void Update()
    {
        float lifeTime = Time.time - _spawnTime;
        float percent = lifeTime / TravelTime;
        Vector3 position = Vector3.Lerp(_startPosition, _endPosition, percent);
        position.y = HeightCurve.Evaluate(percent) * Height;
        transform.position = position;
    }

    void FixedUpdate()
    {
        if (_exploded == false)
        {
            if (Time.time >= _spawnTime + TravelTime)
            {
                Explode();
            }
        }

        if (_exploded)
        {
            bool finishedExploding = Time.time >= _explosionTime + ExplosionTime;
            if (finishedExploding)
            {
                DestroyGrenade();
            }
            else
            {
                var colliders = Physics.OverlapSphere(_endPosition, ExplosionRadius, HitMask);
                for (int i = 0; i < colliders.Length; i++)
                {
                    var lifeComponent = colliders[i].GetComponent<Life>();
                    if (lifeComponent != null)
                        lifeComponent.DoDamage(200);
                }
            }
        }
    }
    
    void Explode()
    {
        Instantiate(ImpactParticleSystemPrefab, transform.position, Quaternion.identity);
        _exploded = true;
        _explosionTime = Time.time;

        // disable the grenade renderer
        var renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }
    }

    void DestroyGrenade()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
