using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public LevelData LevelAsset;
    private List<SpawnTracker> _spawns;

    public float _secondsInArena;

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
        public bool CriteriasFulfilled { get; set; }
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

            CriteriasFulfilled = completedCriterias >= _criteriaCount;
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
        _secondsInArena += Time.deltaTime;

        state.SecondsInArena = (int)_secondsInArena;
        GameState.SecondsInArena = state.SecondsInArena;
        if(state.SecondsInArena > LevelAsset.Time)
        {
            MessageManager.QueueMessage(new ArenaFinishedMessage());
        }

        state.EnemiesAlive = GameState.Enemies.FindAll(a => a.IsAlive).Count; //check if they are alive


        foreach (var spawn in _spawns)
        {
            if (spawn.Completed)
                continue;

            spawn.update(state);

            if(spawn.CriteriasFulfilled)
            {
                var prefab = LevelAsset.EnemyTypes[spawn.GetEnemyTypeIndex()];
                for(int i=0; i < spawn.GetEnemySpawnCount(); ++i)
                {
                    var enemy = Instantiate<IEnemy>(prefab);
                    GameState.Enemies.Add(enemy);
                    enemy.Player = GameState.PlayerAsset;
                    enemy.transform.position = GetRandomSpawnPosition();
                }
                state.EnemiesAlive += spawn.GetEnemySpawnCount(); //update alive enemies so we dont trigger lots of new enemies in one frame
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

    public void StartArena()
    {
        _secondsInArena = 0.0f;
    }
}
