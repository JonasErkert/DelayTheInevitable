using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputText;
    private CalculatorTask task;
    private float _input1;
    private float _input2;
    private string _inputString;
    private string _operation;
    private float _result;

    private void Start()
    {
        task = GetComponent<CalculatorTask>();
    }

    public void ClickNumber(int number)
    {
        if (!string.IsNullOrEmpty(_inputString))
        {
            if(_inputString.Length < 4) //Limit input to 9999 max
                _inputString += number;
        }
        else
        {
            _inputString = number.ToString();
        }
        inputText.text = _inputString;
    }

    public void ClickOperation(string operation)
    {
        if (_input1 == 0.0f)
        {
            _operation = operation;
            _input1 = int.Parse(_inputString);
            _inputString = String.Empty;
        }
    }

    public void Calculate()
    {
        if (_input1 != 0)
        {
            _input2 = int.Parse(_inputString);
            switch (_operation)
            {
                case "+":
                    _result = _input1 + _input2;
                    break;
                case "-":
                    _result = _input1 - _input2;
                    break;
                case "/":
                    _result = _input1 / _input2;
                    break;
                case "*":
                    _result = _input1 * _input2;
                    break;
            }

            task.CheckResult((int)_input1, _operation, (int)_input2, _result);

            if(_result == (int)_result) inputText.text = _result.ToString();
            else inputText.text = _result.ToString("F2");
        }
    }
    public void Clear()
    {
        _input1 = 0.0f;
        _input2 = 0.0f;
        _result = 0.0f;
        _inputString = String.Empty;
        inputText.text = "0";
    }
}
