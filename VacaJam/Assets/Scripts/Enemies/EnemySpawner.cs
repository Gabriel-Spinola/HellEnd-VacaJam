using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private float _nextWaveCooldown;

    private int _waveCount = 0;
    private int _enemiesSpawned = 0;

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
        if (_waveCount >= _waves.Length) {
            Destroy(this.gameObject);

            return;
        }

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        Wave wave = _waves[_waveCount];

        if (Time.time >= _nextSpawn && _enemiesSpawned <= wave.EnemiesAmount - 1) {
            Instantiate(wave.Enemies[Random.Range(0, wave.Enemies.Length - 1)], transform.position, Quaternion.identity);

            _enemiesSpawned++;
            _nextSpawn = Time.time + 1f / wave.SpawnRate;
        }
        else if (_enemiesSpawned > wave.EnemiesAmount - 1) {
            StartCoroutine(NextWave(_nextWaveCooldown));
        }
    }

    private IEnumerator NextWave(float time)
    {
        yield return new WaitForSeconds(time);

        _waveCount++;
    }
}
