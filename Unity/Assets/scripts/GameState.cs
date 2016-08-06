using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameState
{
    public static GameObject PlayerAsset { get; set; }
    public static Dictionary<GameMain.InputConcept, bool> PlayerInput { get; set; }

}
