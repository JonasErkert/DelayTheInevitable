using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerGameController : MonoBehaviour
{
    [SerializeField] private float _rotSpeed = 200.0f;
    [SerializeField] private float _shootingCooldown = 1.0f;
   
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPosition;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private Image _shieldBarUI;
    [SerializeField] private Image _lifeBarUI;
    [SerializeField] private float _lifes = 100.0f;

    public float Lifes => _lifes;

    [SerializeField]
    private TextMeshProUGUI _killCounterUI;

    [Header("Shield Settings")] 
    [SerializeField] private float _shieldUpTime = 2.0f;
    [SerializeField] private float _shieldFadeOutTime = 5.0f;
    [SerializeField] private float _shieldCooldown = 10.0f;
    
    [Header("Audio")] [SerializeField] private AudioClip[] _audioClips;

    private AudioSource _audioSource;
    
    private int _enemyKills = 0;
    

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

        _audioSource = GetComponent<AudioSource>();
        _shieldPrefab.transform.localScale = Vector3.zero;
        _startLifes = _lifes;
    }

    void Update()
    {
        if(GameManager.Instance.gameScreenOpen){
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.RotateAround(transform.position,Vector3.forward,_rotSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.RotateAround(transform.position,Vector3.back,_rotSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UseShield();
            }
        }

        if (!_isShieldActive)
        {
            float buffer =  _nextTimeToShield - Time.time;
            _shieldBarUI.fillAmount = 1 - buffer;
        }
    }

    private void Shoot()
    {
        if (Time.time > _nextTimeToShoot)
        {
            _audioSource.PlayOneShot(_audioClips[Random.Range(0,3)]);
            _nextTimeToShoot = Time.time + _shootingCooldown;
            GameObject tmpProjectile = Instantiate(_projectilePrefab,_projectileSpawnPosition.position,_projectileSpawnPosition.rotation);
            tmpProjectile.SendMessage("IsProjectileFromPlayer",true);
        }
    }

    private void UseShield()
    {
        if (Time.time > _nextTimeToShield && !_isShieldActive)
        {
            StartCoroutine(CreateShield(0.5f));
            _isShieldActive = true;
            _nextTimeToShield = Time.time + _shieldCooldown;
        }
    }
    
    
    IEnumerator CreateShield( float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            _shieldPrefab.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.6f, elapsed/duration);
            //_shieldBarUI.fillAmount = Mathf.Lerp(0, 1, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("done");
        StartCoroutine(ActiveShield(_shieldUpTime, _shieldFadeOutTime));
    }
    
    IEnumerator ActiveShield(float waitDuration, float fadeDuration)
    {
        float elapsed = 0.0f;

        yield return new WaitForSeconds(waitDuration);
        Debug.Log("done_Wait");
        while (elapsed < fadeDuration)
        {
            _shieldPrefab.transform.localScale = Vector3.Lerp(Vector3.one * 0.6f,Vector3.zero, elapsed/fadeDuration);
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
                GameManager.Instance.SetGameOverReason(GameOverReason.GameLost);
            }
        }
    }

    public void AddKillToCounter()
    {
        _enemyKills++;
        _killCounterUI.text = _enemyKills.ToString();
    }
}
