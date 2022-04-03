using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DesktopGameManager : MonoBehaviour
{
    private int keysPressed = 0;
    [SerializeField]
    private Slider keyProgressSlider;
    [SerializeField]
    private int initialTaskKeyCount = 100;
    private int maximalKeyCount = 100;
    [SerializeField]
    private Image progressbarFillImage;


    [SerializeField]
    private TextMeshProUGUI textField;
    [SerializeField]
    private GameObject caret;
    private Coroutine caretCoroutine;

    private void Awake()
    {
        // TODO GameState == Playing
        caretCoroutine = StartCoroutine(CaretBlinking());

        keyProgressSlider.minValue = 0;
        keyProgressSlider.value = 0;
        keyProgressSlider.maxValue = initialTaskKeyCount;
        maximalKeyCount = initialTaskKeyCount;
    }

    private void OnEnable()
    {
        keyProgressSlider.maxValue = maximalKeyCount;
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
        if (Input.anyKeyDown && !GameManager.Instance.gameScreenOpen)
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

            if (keyProgressSlider.value >= keyProgressSlider.maxValue) progressbarFillImage.color = Color.green;
            else progressbarFillImage.color = Color.red;
        }
    }

    public void AddKeysAsWork(int keysToAdd)
    {
        maximalKeyCount += keysToAdd;
        keyProgressSlider.maxValue = maximalKeyCount;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        CheckForWriting();
    }
}
