using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _lifes = 5.0f;
    [SerializeField] private float _fireRate = 1.5f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPosition;

    private void Start()
    {
        StartCoroutine(ShootAtPlayer());
    }

    public void ApplyDamage(float damage)
    {
        _lifes -= damage;
        if (_lifes <= 0.0f)
        {
            PlayerGameController.Instance.AddKillToCounter();
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        Instantiate(_projectilePrefab,_projectileSpawnPosition.position,transform.rotation);
    }

    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_fireRate);
        }
    }
}
