using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WorkCountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int startTimeSeconds;

    public bool hasTimerFinished = false;

    private TimeSpan _startTimeSpan;
    private TimeSpan _timeRemaining;
    private bool _timerRunning = false;
    private float _elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _startTimeSpan = new TimeSpan(0, 0, startTimeSeconds);
        timerText.text = _startTimeSpan.ToString("mm':'ss");
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

    public void ResetTimer()
    {
        StopAllCoroutines();
        timerText.text = _startTimeSpan.ToString("mm':'ss");
        hasTimerFinished = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (_timerRunning)
        {
            _elapsedTime += Time.deltaTime;

            _timeRemaining = _startTimeSpan - TimeSpan.FromSeconds(_elapsedTime);

            if (_timeRemaining.Seconds <= 0)
            {
                hasTimerFinished = true;
                timerText.text = "00:00";
            }
            else
            {
                string timePlayingString = _timeRemaining.ToString("mm':'ss");
                timerText.text = timePlayingString;
            }

            yield return null;
        }
    }
}
