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

    private GameState _gameState = GameState.Menu;

    private PlayingManager playingManager;

    public float Difficulty
    {
        get => _difficulty;
        set => _difficulty = Mathf.Clamp(value, 0, 1);
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        playingManager = GetComponent<PlayingManager>();

        SetGameState(GameState.Menu);
        BeginTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // Debugging:
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetGameState(GameState.Menu);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetGameState(GameState.Playing);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetGameState(GameState.GameOver);

        if (Input.GetKeyDown(KeyCode.T)) GameObject.Find("PC").GetComponentInChildren<DesktopGameManager>().StopWritingAndGiveTask("This is msg");
        if (Input.GetKeyDown(KeyCode.Z)) GameObject.Find("PC").GetComponentInChildren<DesktopGameManager>().ContinueWriting();
    }


    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:

                break;
            case GameState.Playing:
                playingManager.StartPlayState();
                break;
            case GameState.GameOver:

                break;
        }

        _gameState = state;
    }
    public GameState GetGameState() { return _gameState; }


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
