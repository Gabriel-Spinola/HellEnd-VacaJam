using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private float _nextWaveCooldown;
    [SerializeField] private Vector2 _maxSpawnOffset;
    [SerializeField] private Vector2 _minSpawnOffset;

    private int _waveCount = 0;
    private int _enemiesSpawned = 0;

    private int _canChangeWave;

    private float _nextSpawn;

    [Serializable] private class Wave
    {
        [SerializeField] private GameObject[] _enemies;
        [SerializeField] private float _spawnRate;

        [Tooltip("There's a bug I'm too Lazy to fix so, if the wave > 1 add 1 to the enemies amount, sorry")]
        [SerializeField] private int _enemiesAmount;

        public GameObject[] Enemies => _enemies;
        public float SpawnRate => _spawnRate;
        public int EnemiesAmount => _enemiesAmount;
    }

    private void Update()
    {
        if (_waveCount > _waves.Length - 1) {
            Destroy(this.gameObject);

            return;
        }
        
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        Wave wave = _waves[_waveCount];

        if (Time.time >= _nextSpawn && _enemiesSpawned < wave.EnemiesAmount) {
            Instantiate(
                original: wave.Enemies[Random.Range(0, wave.Enemies.Length - 1)],
                position: (Vector2) transform.position + new Vector2(
                            Random.Range(_minSpawnOffset.x, _maxSpawnOffset.x), Random.Range(_minSpawnOffset.y, _maxSpawnOffset.y)
                          ),
                rotation: Quaternion.identity
            );

            _enemiesSpawned++;
            _nextSpawn = Time.time + 1f / wave.SpawnRate;
        }

        if (_enemiesSpawned >= wave.EnemiesAmount) {
            StartCoroutine(NextWave(_nextWaveCooldown));
            _canChangeWave = 0;
        }
    }

    private IEnumerator NextWave(float time)
    {
        yield return new WaitForSeconds(time);

        _canChangeWave++;

        if (_canChangeWave > 1)
            yield break;

        _waveCount++;
    }
}
