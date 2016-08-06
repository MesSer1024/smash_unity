﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelController : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public LevelData LevelAsset;
    private List<SpawnTracker> _spawns;

    private struct ArenaState
    {
        public int SecondsInArena;
        public int EnemiesAlive;
        public int EnemiesKilled;
    }

    private class SpawnTracker
    {
        private LevelData.WaveSpawn _spawnData;
        public bool Completed { get; set; }
        public bool Active { get; set; }
        private int _criteriaCount;

        public SpawnTracker(LevelData.WaveSpawn waveData)
        {
            _spawnData = waveData;
            _criteriaCount = 0;
            if (waveData.MaxEnemiesAlive > 0)
                _criteriaCount++;
            if (waveData.MinSecondsInArena > 0)
                _criteriaCount++;
            if (waveData.MinTotalEnemiesKilled > 0)
                _criteriaCount++;
        }

        public void update(ArenaState data)
        {
            //check for activation
            if(Active == false)
            {
                int completedCriterias = 0;
                if (_spawnData.MinSecondsInArena > 0 && data.SecondsInArena >= _spawnData.MinSecondsInArena)
                {
                    completedCriterias++;
                }
                if (_spawnData.MaxEnemiesAlive > 0 && data.EnemiesAlive < _spawnData.MaxEnemiesAlive)
                {
                    completedCriterias++;
                }
                if (_spawnData.MinTotalEnemiesKilled > 0 && data.EnemiesKilled >= _spawnData.MinTotalEnemiesKilled)
                {
                    completedCriterias++;
                }
                if (completedCriterias >= _criteriaCount)
                    Active = true;
            }
        }

        public int GetEnemyTypeIndex()
        {
            return _spawnData.EnemyType;
        }

        public int GetEnemySpawnCount()
        {
            return _spawnData.SpawnCount;
        }
    }

    void Start()
    {
        _spawns = new List<SpawnTracker>();
        foreach(var item in LevelAsset.Waves)
        {
            _spawns.Add(new SpawnTracker(item));
        }
    }

    void Update()
    {
        var state = new ArenaState();
        state.SecondsInArena = (int)Time.time;
        foreach(var spawn in _spawns)
        {
            if (spawn.Completed)
                continue;

            spawn.update(state);

            if(spawn.Active)
            {
                var prefab = LevelAsset.EnemyTypes[spawn.GetEnemyTypeIndex()];
                for(int i=0; i < spawn.GetEnemySpawnCount(); ++i)
                {
                    var enemy = Instantiate<IEnemy>(prefab);
                    enemy.Player = GameState.PlayerAsset;

                    enemy.transform.position = GetRandomSpawnPosition();
                }
                spawn.Completed = true;
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomSpawn = UnityEngine.Random.Range(0, SpawnPoints.Count);
        var spawn = SpawnPoints[randomSpawn];
        var scale = spawn.transform.localScale;

        Vector3 pos = spawn.transform.position;
        pos.x += UnityEngine.Random.Range(0, scale.x) - (scale.x / 2);
        pos.z += UnityEngine.Random.Range(0, scale.z) - (scale.z / 2);

        return pos;
    }
}
