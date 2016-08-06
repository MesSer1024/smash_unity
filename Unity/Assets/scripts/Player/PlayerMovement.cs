using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 100.0f;
    public float MovementRunMultiplier = 3.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        var rigid = GetComponent<Rigidbody>();
        Vector3 inputDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDirection.z -= 1;

        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDirection.x -= 1;

        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDirection.x += 1;
        }
        var speedFactor = 1.0f;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speedFactor = MovementRunMultiplier;
        }
        rigid.velocity = inputDirection.normalized * MovementSpeed * speedFactor;
    }
}
