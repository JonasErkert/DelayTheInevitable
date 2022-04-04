using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Timer")]
    [SerializeField] private float secondsToMaxDifficulty = 180f;
    [SerializeField] private TextMeshProUGUI timerText;
    private TimeSpan _timePlaying;
    private bool _timerRunning = false;
    private float _elapsedTime = 0f;
    private float _difficulty = 0;
    
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject officeLights;
    private bool temp = false;

    private GameState _gameState = GameState.Menu;

    [Header("Script Connections")]
    [SerializeField] private Boss bossScript;
    [SerializeField] private WorkCountdown countdownScript;
    [SerializeField] private DesktopGameManager desktopManagerScript;

    public float Difficulty
    {
        get => _difficulty;
        set => _difficulty = Mathf.Clamp(value, 0, 1);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);

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
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            temp = !temp;
            ToggleGameOver(temp);
        }

        if (desktopManagerScript.isWorkFinished && countdownScript.hasTimerFinished)
        {
            countdownScript.ResetTimer();
            countdownScript.BeginTimer();

            desktopManagerScript.ResetProgress();
        }
        else
        {
            if (countdownScript.hasTimerFinished)
            {
                SetGameState(GameState.GameOver);
            }
            else
            {
                if (desktopManagerScript.isWorkFinished)
                {
                }
            }
        }
    }

    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                StartMenuState();
                break;
            case GameState.Playing:
                StartPlayState();
                break;
            case GameState.GameOver:
                ResetPlayState();
                break;
        }

        _gameState = state;
    }
    public GameState GetGameState() { return _gameState; }

    public void StartMenuState()
    {
        // TODO
    }
    
    public void StartPlayState()
    {
        bossScript.StartBossDoor();
        countdownScript.BeginTimer();
    }

    public void ResetPlayState()
    {
        Difficulty = 0;
        countdownScript.ResetTimer();
        bossScript.ResetBossDoor();
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

    public void ToggleGameOver(bool isGameOver)
    {
        gameOverUI.SetActive(isGameOver);
        officeLights.SetActive(!isGameOver);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
