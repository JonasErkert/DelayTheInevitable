using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameController : MonoBehaviour
{
    [SerializeField] private float _rotSpeed = 200.0f;
    [SerializeField] private float _shootingCooldown = 1.0f;
    [SerializeField] private float _shieldCooldown = 15.0f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPosition;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private Image _shieldBarUI;
    [SerializeField] private Image _lifeBarUI;
    [SerializeField] private float _lifes = 10.0f;

    private float _startLifes;
    private float _nextTimeToShoot;

    private bool _isShieldActive;
    private float _nextTimeToShield;

    #region singleton stuff

    private static PlayerGameController _instance;

    public static PlayerGameController Instance
    {
        get { return _instance; }
    }
    #endregion
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
        _shieldPrefab.transform.localScale = Vector3.zero;
        _startLifes = _lifes;
    }

    void Update()
    {
        if(GameManager.Instance.gameScreenOpen){
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow))
            {
                transform.RotateAround(transform.position,Vector3.forward,_rotSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.RightArrow))
            {
                transform.RotateAround(transform.position,Vector3.back,_rotSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.UpArrow))
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UseShield();
            }
        }
    }

    private void Shoot()
    {
        if (Time.time > _nextTimeToShoot)
        {
            _nextTimeToShoot = Time.time + _shootingCooldown;
            GameObject tmpProjectile = Instantiate(_projectilePrefab,_projectileSpawnPosition.position,_projectileSpawnPosition.rotation);
            tmpProjectile.SendMessage("IsProjectileFromPlayer",true);
        }
    }

    private void UseShield()
    {
        if (Time.time > _nextTimeToShield && !_isShieldActive)
        {
            StartCoroutine(CreateShield(1.0f));
            _isShieldActive = true;
            _nextTimeToShield = Time.time + _shieldCooldown;
        }
    }
    
    
    IEnumerator CreateShield( float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            _shieldPrefab.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 2, elapsed/duration);
            _shieldBarUI.fillAmount = Mathf.Lerp(0, 1, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("done");
        StartCoroutine(ActiveShield(2, 8));
    }
    
    IEnumerator ActiveShield(float waitDuration, float fadeDuration)
    {
        float elapsed = 0.0f;

        yield return new WaitForSeconds(waitDuration);
        Debug.Log("done_Wait");
        while (elapsed < fadeDuration)
        {
            _shieldPrefab.transform.localScale = Vector3.Lerp(Vector3.one * 2,Vector3.zero, elapsed/fadeDuration);
            _shieldBarUI.fillAmount = Mathf.Lerp(1, 0, elapsed/fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("done_Despawn");
        _isShieldActive = false;
    }
    
    public void ApplyDamage(float damage)
    {
        if(!_isShieldActive){
            _lifes -= damage;
            _lifeBarUI.fillAmount = _lifes / _startLifes;
            if (_lifes <= 0.0f)
            {
                //TODO: Broadcast player died in the GameGame
            }
        }
    }
}
