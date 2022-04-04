using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public struct SpawnDifficulties
{
    public Vector2 difficultyRange;
    public float spawnFrequenzy;
    public GameObject enemyPrefab;
}
public class EnemySpawner : MonoBehaviour
{
    private GameObject _enemyPrefab;

    [SerializeField] private Transform _spawnAreaMin;
    [SerializeField] private Transform _spawnAreaMax;
    [SerializeField] private float _distanceToPlayer = 2.0f;
    [SerializeField] private SpawnDifficulties[] _spawnDifficulties;

    private SpawnDifficulties _currentSpawnDifficulty;
    private int _indexDifficulty = -1; //start at -1 to init in Start to 0
    private bool _startSpawning;

    // Update is called once per frame
    void Update()
    {
        //Init to start spawning 
        if (GameManager.Instance.GetGameState() == GameState.Playing && !_startSpawning)
        {
            _startSpawning = true;
            UpdateCurrentDiffitultyRange();
        }
        if(_startSpawning){
            if (_currentSpawnDifficulty.difficultyRange.y < GameManager.Instance.Difficulty)
            {
                StopAllCoroutines();
                UpdateCurrentDiffitultyRange();
            }
        }
        
        //For Debugging Key Spawn
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    SpawnEnemy();
        //}
    }

    private void UpdateCurrentDiffitultyRange()
    {
        _indexDifficulty++;
        _currentSpawnDifficulty = _spawnDifficulties[_indexDifficulty];
        _enemyPrefab = _currentSpawnDifficulty.enemyPrefab;
        StartCoroutine(SpawnEnemysTimed(_currentSpawnDifficulty.spawnFrequenzy));
    }

    private void SpawnEnemy()
    {
        Vector3 randomPosition = GetRandomSpawnPosition();
        GameObject tmp = Instantiate(_enemyPrefab, randomPosition, quaternion.identity);
        Vector3 dir = PlayerGameController.Instance.gameObject.transform.position - tmp.transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        tmp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition;
        do{
            randomPosition = new Vector3(Random.Range(_spawnAreaMin.position.x, _spawnAreaMax.position.x),
                Random.Range(_spawnAreaMin.position.y, _spawnAreaMax.position.y),
                Random.Range(_spawnAreaMax.position.z, _spawnAreaMax.position.z));
        }while (Vector3.Distance(randomPosition, transform.position) <= _distanceToPlayer);
        return randomPosition;
    }


    private IEnumerator SpawnEnemysTimed(float spawnFrequenzy)
    {
        if(_indexDifficulty == 0)
            yield return new WaitForSeconds(3.0f); //wait for the tutorial screen
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnFrequenzy);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(_spawnAreaMin.position,_spawnAreaMax.position);
        Gizmos.DrawWireSphere(transform.position,_distanceToPlayer);
    }
}
