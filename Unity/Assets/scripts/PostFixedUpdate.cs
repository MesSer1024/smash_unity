using UnityEngine;
using System.Collections;

public class PostFixedUpdate : MonoBehaviour
{

    void Start()
    {

    }

    void FixedUpdate()
    {
        GameMain.Instance.PostFixedUpdate();
    }
}
