using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    #region singleton stuff

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }


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
    }

    #endregion

    public bool gameScreenOpen = true;

    [SerializeField] private float secondsToMaxDifficulty = 180f;
    [SerializeField] private TextMeshProUGUI timerText;
    private TimeSpan _timePlaying;
    private bool _timerRunning = false;
    private float _elapsedTime = 0f;
    private float _difficulty = 0;

    public float Difficulty
    {
        get => _difficulty;
        set => _difficulty = Mathf.Clamp(value, 0, 1);
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        
        BeginTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginTimer()
    {
        _timerRunning = true;
        _elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        _timerRunning = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (_timerRunning)
        {
            _elapsedTime += Time.deltaTime;
            Difficulty = _elapsedTime / secondsToMaxDifficulty;
            
            _timePlaying = TimeSpan.FromSeconds(_elapsedTime);
            string timePlayingString = _timePlaying.ToString("mm':'ss':'ff");
            timerText.text = timePlayingString;

            yield return null;
        }
    }
}
