using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{
    public bool ShowHealthBar;
    public float StartHealth = 100f;

    public float Health { get { return _health; } }

    private float _health;

    void Awake()
    {
        _health = StartHealth;
    }

    public void DoDamage(float damage)
    {
        _health -= damage;
    }

    void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
