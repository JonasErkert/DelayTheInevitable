using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorTask : MonoBehaviour
{
    private Calculator calcScript;

    [SerializeField]
    private DesktopGameManager desktopManager;

    private bool isInTask = false;


    private float randomFirstNumber = 0;
    private string randomOperator = "";
    private float randomSecondNumber = 0;
    private float correctResult = 0;

    // Start is called before the first frame update
    void Start()
    {
        calcScript = GetComponent<Calculator>();
    }

    public void StartCalculatorTask()
    {
        randomFirstNumber = Random.Range(1, 10);
        randomOperator = "";
        randomSecondNumber = Random.Range(1, 10);
        switch (Random.Range(0, 4))
        {
            case 0:
                randomOperator = "+";
                correctResult = randomFirstNumber + randomSecondNumber;
                break;
            case 1:
                randomOperator = "-";
                correctResult = randomFirstNumber - randomSecondNumber;
                break;
            case 2:
                randomOperator = "*";
                correctResult = randomFirstNumber * randomSecondNumber;
                break;
            case 3:
                randomOperator = "/";
                correctResult = randomFirstNumber / randomSecondNumber;
                break;
        }


        desktopManager.StopWritingAndGiveTask("Calculate this: \n"+ randomFirstNumber + " "+ randomOperator + " "+ randomSecondNumber + " =");
        isInTask = true;

    }

    public void CheckResult(int firstNumber, string oper, int secondNumber, float result)
    {
        if (isInTask && firstNumber == randomFirstNumber && oper == randomOperator && secondNumber == randomSecondNumber && result.ToString("F1") == correctResult.ToString("F1"))
        {
            isInTask = false;
            desktopManager.ContinueWriting();
            GameManager.Instance.isWorkingOnTask = false;
        }
    }

}
