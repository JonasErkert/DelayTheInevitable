using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _lifes = 10.0f;

    public void ApplyDamage(float damage)
    {
        _lifes -= damage;
        if (_lifes <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
