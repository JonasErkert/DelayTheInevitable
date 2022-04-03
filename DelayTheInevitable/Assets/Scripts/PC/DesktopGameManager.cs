using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DesktopGameManager : MonoBehaviour
{
    [Header("Word")]
    private int keysPressed = 0;
    [SerializeField]
    private Slider keyProgressSlider;
    [SerializeField]
    private int initialTaskKeyCount = 100;
    [SerializeField]
    private Image progressbarFillImage;
    
    [SerializeField]
    private TextMeshProUGUI textField;
    [SerializeField]
    private GameObject caret;
    private Coroutine caretCoroutine;

    [Header("Mail")]
    [SerializeField]
    private float baseWorkTimespan;
    private float currentWorkTimespan;
    [SerializeField]
    private Animator mailPopup;

    [HideInInspector]
    public bool isWorkFinished = false;
    private bool hasTask = false;
    private TaskMessage taskMessageScript;


    private void Awake()
    {
        keyProgressSlider.minValue = 0;
        keyProgressSlider.value = 0;
        keyProgressSlider.maxValue = initialTaskKeyCount;

        currentWorkTimespan = baseWorkTimespan;
        taskMessageScript = GetComponent<TaskMessage>();
    }

    private void OnEnable()
    {
        if(caret != null) caretCoroutine = StartCoroutine(CaretBlinking());
        keyProgressSlider.maxValue = initialTaskKeyCount;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    #region WritingGame

    IEnumerator CaretBlinking()
    {
        while (true)
        {
            caret.SetActive(!caret.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void CheckForWriting()
    {
        // TODO && GameState == Playing
        if (Input.anyKeyDown && !GameManager.Instance.gameScreenOpen && GameManager.Instance.GetGameState() == GameState.Playing && !hasTask)
        {
            if (caret != null)
            {
                StopCoroutine(caretCoroutine);
                Destroy(caret);
            }

            if (textField.isTextTruncated) textField.text = textField.text.Remove(0, 1);
            int letterIndex = Random.Range(0, 5);
            string letter = "";
            switch (letterIndex)
            {
                case 0: letter = "A"; break;
                case 1: letter = "B"; break;
                case 2: letter = "C"; break;
                case 3: letter = "D"; break;
                case 4: letter = "E"; break;
            }
            textField.text += letter;
            textField.ForceMeshUpdate();

            keysPressed++;
            keysPressed = Mathf.Clamp(keysPressed, 0, (int)keyProgressSlider.maxValue);
            keyProgressSlider.value = keysPressed;

            if (keyProgressSlider.value >= keyProgressSlider.maxValue)
            {
                isWorkFinished = true;
                progressbarFillImage.color = Color.green;
            }
            else
            {
                progressbarFillImage.color = Color.red;
            }
        }
    }

    public void ResetProgress()
    {
        isWorkFinished = false;
        keyProgressSlider.value = 0;
        progressbarFillImage.color = Color.red;
    }

    public void StopWritingAndGiveTask(string message)
    {
        taskMessageScript.StartTask(message);

        hasTask = true;
    }

    public void ContinueWriting()
    {
        taskMessageScript.StopTask();
        hasTask = false;
    }

    #endregion


    // Update is called once per frame
    void Update()
    {
        CheckForWriting();
    }
}
