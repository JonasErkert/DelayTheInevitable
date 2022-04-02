using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    

    // Update is called once per frame
    void Update()
    {
        //For Debugging Key Spawn
        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector3 randomPosition = Random.insideUnitCircle.normalized * 4;

            GameObject tmp = Instantiate(_enemyPrefab, randomPosition, quaternion.identity);
            
        }
    }
}
