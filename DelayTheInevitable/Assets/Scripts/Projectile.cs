using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private bool _isFromPlayer;

    public bool IsFromPlayer
    {
        get => _isFromPlayer;
        set => _isFromPlayer = value;
    }

    private void Awake()
    {
        Destroy(gameObject,5.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("ApplyDamage",5.0);
            Destroy(gameObject);
        }
    }
}
