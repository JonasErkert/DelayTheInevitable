using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WorkCountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int startTimeSeconds;

    private TimeSpan _startTimeSpan;
    private TimeSpan _timeRemaining;
    private bool _timerRunning = false;
    private float _elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _startTimeSpan = new TimeSpan(0, 0, startTimeSeconds);
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
        _elapsedTime = 0f;
        timerText.text = _startTimeSpan.ToString("mm':'ss");
    }

    private IEnumerator UpdateTimer()
    {
        while (_timerRunning)
        {
            _elapsedTime += Time.deltaTime;

            _timeRemaining = _startTimeSpan - TimeSpan.FromSeconds(_elapsedTime);
            
            string timePlayingString = _timeRemaining.ToString("mm':'ss");
            timerText.text = timePlayingString;

            yield return null;
        }
    }
}
