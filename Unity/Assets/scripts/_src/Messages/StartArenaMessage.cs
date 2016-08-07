using UnityEngine;
using System.Collections;

public class StartArenaMessage : IMessage
{
    public int Level { get; private set; }
    public bool RestartRound { get; private set; }

    public StartArenaMessage(int level, bool restart = false)
    {
        Level = level;
        RestartRound = restart;
    }
}
