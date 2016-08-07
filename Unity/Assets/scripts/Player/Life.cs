using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Life : MonoBehaviour
{
    public LifeDataAsset LifeData;

    public bool ShowHealthBar;
    public float StartHealth = 100f;

    public float Health { get { return _health; } }

    private float _health;

    public static List<Life> LifeComponents = new List<Life>(30);  

    void Awake()
    {
        _health = StartHealth;
    }

    void OnEnable()
    {
        LifeComponents.Add(this);
    }

    void OnDisable()
    {
        LifeComponents.Remove(this);
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
