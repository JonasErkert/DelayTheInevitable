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

public enum GameOverReason
{
    GameLost,
    DeadlineReached,
    CaughtByBoss
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
    public bool isWorkingOnTask = false;

    [Header("Events")]
    [SerializeField] private Animator workMailAnimation;

    [Header("Timer")]
    [SerializeField] private float secondsToMaxDifficulty = 180f;
    private TimeSpan _timePlaying;
    private bool _timerRunning = false;
    private float _elapsedTime = 0f;
    private float _difficulty = 0;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameOverReasonText;
    [SerializeField] private GameObject timerUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject descriptionUI;
    private bool _isDescriptionOpen = false;
    
    [Header("Misc")]
    [SerializeField] private GameObject officeLights;
    private GameState _gameState = GameState.Menu;
    private GameOverReason _gameOverReason = GameOverReason.DeadlineReached;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Debugging:
        //if (Input.GetKeyDown(KeyCode.Alpha1)) SetGameState(GameState.Menu);
        //if (Input.GetKeyDown(KeyCode.Alpha2)) SetGameState(GameState.Playing);
        //if (Input.GetKeyDown(KeyCode.Alpha3)) SetGameState(GameState.GameOver);

        if (_isDescriptionOpen)
        {
            if (Input.anyKeyDown)
            {
                ToggleDescriptionUI(false);
                SetGameState(GameState.Playing);
            }
        }

        if (desktopManagerScript.isWorkFinished && countdownScript.hasTimerFinished)
        {
            countdownScript.ResetTimer();
            countdownScript.BeginTimer();
            workMailAnimation.SetTrigger("RollInMail");
            desktopManagerScript.ResetProgress();
        }
        else
        {
            if (countdownScript.hasTimerFinished)
            {
                SetGameOverReason(GameOverReason.DeadlineReached);
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

    public void SetGameOverReason(GameOverReason reason)
    {
        _gameOverReason = reason;
        SetGameState(GameState.GameOver);
    }
    public GameOverReason GetGameOverReason() { return _gameOverReason; }

    public void StartMenuState()
    {
        // TODO: Is something in here?
    }
    
    public void StartPlayState()
    {
        BeginTimer();
        bossScript.StartBossDoor();
        countdownScript.BeginTimer();
        ToggleOfficeLights(true);
        ToggleTimerUI(true);
    }

    public void ResetPlayState()
    {
        Difficulty = 0;
        countdownScript.ResetTimer();
        bossScript.ResetBossDoor();
        ToggleTimerUI(false);

        string reasonText = "Game Over";
        switch (_gameOverReason)
        {
            case GameOverReason.GameLost:
                reasonText = "GAME Lost!";
                break;
            case GameOverReason.DeadlineReached:
                reasonText = "DEADLINE reached!";
                break;
            case GameOverReason.CaughtByBoss:
                reasonText = "CAUGHT by Boss!";
                break;
        }

        gameOverReasonText.text = reasonText;
        ToggleGameOverUI(true);
        
        _timePlaying = TimeSpan.FromSeconds(_elapsedTime);
        string timePlayingString = "delayed work by " + _timePlaying.ToString("mm':'ss");
        gameOverScoreText.text = timePlayingString;
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

    public void ToggleGameOverUI(bool isGameOver)
    {
        gameOverUI.SetActive(isGameOver);
        ToggleOfficeLights(!isGameOver);
    }

    public void ToggleOfficeLights(bool isLightEnabled)
    {
        officeLights.SetActive(isLightEnabled);
    }

    public void StartGame()
    {
        ToggleStartMenuUI(false);
        ToggleDescriptionUI(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleStartMenuUI(bool isShowingStart)
    {
        startMenuUI.SetActive(isShowingStart);
    }

    public void ToggleDescriptionUI(bool isShowingDescription)
    {
        descriptionUI.SetActive(isShowingDescription);
        _isDescriptionOpen = isShowingDescription;
    }

    public void ToggleTimerUI(bool isShowingTimer)
    {
        timerUI.SetActive(isShowingTimer);
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
