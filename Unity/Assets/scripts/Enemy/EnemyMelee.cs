using UnityEngine;
using System.Collections;

public class EnemyMelee : IEnemy
{
    public float MovementSpeed = 0.5f;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        var dir = (Player.transform.position - transform.position).normalized;
        _rigidbody.velocity = dir * MovementSpeed;
    }
}
