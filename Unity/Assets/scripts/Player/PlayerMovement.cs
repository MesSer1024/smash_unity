using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
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
        var input = GameState.PlayerInput;

        Vector3 inputDirection = Vector3.zero;
        if (input[GameMain.InputConcept.MoveUp])
        {
            inputDirection.z += 1;
        }
        if (input[GameMain.InputConcept.MoveDown])
        {
            inputDirection.z -= 1;

        }
        if (input[GameMain.InputConcept.MoveLeft])
        {
            inputDirection.x -= 1;

        }
        if (input[GameMain.InputConcept.MoveRight])
        {
            inputDirection.x += 1;
        }
        var speedFactor = 1.0f;

        if (input[GameMain.InputConcept.Run])
        {
            speedFactor = MovementRunMultiplier;
        }
        rigid.velocity = inputDirection.normalized * MovementSpeed * speedFactor;
    }
}
