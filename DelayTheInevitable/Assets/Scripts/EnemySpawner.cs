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
            Vector3 randomPosition = Random.insideUnitCircle.normalized * 3.5f;
            randomPosition += gameObject.transform.position;
            GameObject tmp = Instantiate(_enemyPrefab, randomPosition, quaternion.identity);
            Vector3 dir = PlayerGameController.Instance.gameObject.transform.position - tmp.transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            tmp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //tmp.transform.LookAt(PlayerGameController.Instance.gameObject.transform);
        }
    }
}
