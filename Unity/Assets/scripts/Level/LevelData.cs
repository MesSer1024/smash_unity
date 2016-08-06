using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public struct WaveSpawn
    {
        public int EnemyType;
        public int SpawnCount;

        public int MaxEnemiesAlive;
        public int MinSecondsInArena;
        public int MinTotalEnemiesKilled;
        public bool SameSpawnpoint;
    }

    public string LevelName;
    public int Level = 0;
    public int Time = 60;
    public List<IEnemy> EnemyTypes;
    public List<WaveSpawn> Waves;
}
