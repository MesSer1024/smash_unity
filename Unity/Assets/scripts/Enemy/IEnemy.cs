﻿using UnityEngine;
using System.Collections;

public class IEnemy : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject Player;

    public bool IsAlive { get; protected set; }
}
