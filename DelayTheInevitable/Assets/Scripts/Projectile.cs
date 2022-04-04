using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _damage = 5.0f;
    private bool _isFromPlayer;

    public void IsProjectileFromPlayer(bool value)
    {
        _isFromPlayer = value;
    }

    private void Awake()
    {
        Destroy(gameObject,5.0f);
    }

    void Update()
    {
        if (_isFromPlayer)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,
                PlayerGameController.Instance.gameObject.transform.position, step);
            //transform.LookAt(PlayerGameController.Instance.gameObject.transform.position,Vector3.up);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && _isFromPlayer)
        {
            collision.gameObject.SendMessage("ApplyDamage",_damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("ApplyDamage",_damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "EnemyProjectile" && _isFromPlayer)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
