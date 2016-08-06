using UnityEngine;
using System.Collections;

public class EnemyMelee : MonoBehaviour {
    public GameObject Player;
    // Use this for initialization
    void Start ()
    {
    
    }
    
    // Update is called once per frame
    void Update ()
    {

    }

    void FixedUpdate()
    {
        var dir = (Player.transform.position - transform.position).normalized;
        var rigid = GetComponent<Rigidbody>();
        rigid.velocity = dir * 48;
    }
}
