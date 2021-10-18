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

    [SerializeField] private Mode _mode;

    [HideInInspector] public int EnemiesInRoom = 0;

    private enum Mode
    {
        KeepSpawning,
        WaitEnemiesDeath,
        WaitEnemiesDeathToInfinity
    }


    private int _waveCount = 0;
    private int _enemiesSpawned = 0;

    private int _canChangeWave;

    private float _nextSpawn;

    [Serializable] private class Wave
    {
        [SerializeField] private GameObject[] _enemies;
        [SerializeField] private float _spawnRate;

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

        switch (_mode) {
            case Mode.KeepSpawning:
                SpawnEnemiesMode1();
            break; 

            case Mode.WaitEnemiesDeath:
                SpawnEnemiesMode2();
            break;
            
            case Mode.WaitEnemiesDeathToInfinity:
                SpawnEnemiesMode3();
            break;
        }

    }

    private void SpawnEnemiesMode1()
    {
        Wave wave = _waves[_waveCount];

        if (Time.time >= _nextSpawn && _enemiesSpawned < wave.EnemiesAmount) {
            Vector2 randomOffset = new Vector2(Random.Range(_minSpawnOffset.x, _maxSpawnOffset.x), Random.Range(_minSpawnOffset.y, _maxSpawnOffset.y));

            Instantiate(wave.Enemies[Random.Range(0, wave.Enemies.Length)], (Vector2) transform.position + randomOffset, Quaternion.identity);

            _enemiesSpawned++;
            _nextSpawn = Time.time + 1f / wave.SpawnRate;
        }

        if (_enemiesSpawned >= wave.EnemiesAmount) {
            StartCoroutine(NextWave(_nextWaveCooldown));
            _canChangeWave = 0;
        }
    }

    private void SpawnEnemiesMode2()
    {
        Wave wave = _waves[_waveCount];

        if (Time.time >= _nextSpawn && _enemiesSpawned < wave.EnemiesAmount) {
            Vector2 randomOffset = new Vector2(Random.Range(_minSpawnOffset.x, _maxSpawnOffset.x), Random.Range(_minSpawnOffset.y, _maxSpawnOffset.y));

            Instantiate(wave.Enemies[Random.Range(0, wave.Enemies.Length)], (Vector2) transform.position + randomOffset, Quaternion.identity);

            Debug.Log(wave.Enemies.Length - 1);

            EnemiesInRoom++;
            _enemiesSpawned++;
            _nextSpawn = Time.time + 1f / wave.SpawnRate;
        }

        if (_enemiesSpawned >= wave.EnemiesAmount && EnemiesInRoom <= 0) {
            StartCoroutine(NextWave(_nextWaveCooldown));
            _canChangeWave = 0;
        }
    }
    
    private void SpawnEnemiesMode3()
    {
        Wave wave = _waves[_waveCount];

        if (Time.time >= _nextSpawn && _enemiesSpawned < wave.EnemiesAmount) {
            Vector2 randomOffset = new Vector2(Random.Range(_minSpawnOffset.x, _maxSpawnOffset.x), Random.Range(_minSpawnOffset.y, _maxSpawnOffset.y));

            Instantiate(wave.Enemies[Random.Range(0, wave.Enemies.Length)], (Vector2) transform.position + randomOffset, Quaternion.identity);

            EnemiesInRoom++;
            _enemiesSpawned++;
            _nextSpawn = Time.time + 1f / wave.SpawnRate;
        }

        if (_enemiesSpawned >= wave.EnemiesAmount && EnemiesInRoom <= 0) {
            _waveCount = 0;
            _enemiesSpawned = 0;
        }
    }

    private IEnumerator NextWave(float time)
    {
        yield return new WaitForSeconds(time);

        _canChangeWave++;

        if (_canChangeWave > 1)
            yield break;

        _waveCount++;
        _enemiesSpawned = 0;
    }
}
