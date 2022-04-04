using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private Transform _spawnAreaMin;
    [SerializeField] private Transform _spawnAreaMax;
    [SerializeField] private float _distanceToPlayer = 2.0f;
    

    // Update is called once per frame
    void Update()
    {
        //For Debugging Key Spawn
        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            GameObject tmp = Instantiate(_enemyPrefab, randomPosition, quaternion.identity);
            Vector3 dir = PlayerGameController.Instance.gameObject.transform.position - tmp.transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            tmp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //tmp.transform.LookAt(PlayerGameController.Instance.gameObject.transform);
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(_spawnAreaMin.position,_spawnAreaMax.position);
        Gizmos.DrawWireSphere(transform.position,_distanceToPlayer);
    }
}
