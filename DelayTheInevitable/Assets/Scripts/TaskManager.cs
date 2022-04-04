using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private CalculatorTask calculatorTask;
    [SerializeField]
    private float initialTaskSpawnTime = 30f;
    
    private int tasksAmount = 0;


    private bool taskTimerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameState.Playing && !GameManager.Instance.isWorkingOnTask && !taskTimerStarted)
        {
            StartCoroutine(StartRandomTasks());
        }
    }

    IEnumerator StartRandomTasks()
    {
        taskTimerStarted = true;
        // Initiale Spawntime minus Schwierigkeit mal Initiale Spawntime/2. Also Wenn Difficulty auf 1 ist ist die Spawntime bei der Hälfte der initialen Spawntime. Plus Minus 5 Sekunden
        float timeToWait = (initialTaskSpawnTime - (GameManager.Instance.Difficulty * (initialTaskSpawnTime / 2))) + Random.Range(-5f, 5f);
        Debug.Log(timeToWait);
        yield return new WaitForSeconds(timeToWait);
        int randomTask = Random.Range(0, 1);

        switch (randomTask)
        {
            case 0:
                Debug.Log("TASK STARTED");
                calculatorTask.StartCalculatorTask();
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
        GameManager.Instance.isWorkingOnTask = true;
        taskTimerStarted = false;
    }
}
