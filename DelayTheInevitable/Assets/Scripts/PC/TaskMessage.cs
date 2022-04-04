using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskMessage : MonoBehaviour
{
    [SerializeField]
    private Animator taskMessageAnim;

    [SerializeField]
    private TextMeshProUGUI taskMessageText;

    [SerializeField]
    private Image transparentImage;
    public void StartTask(string message)
    {
        transparentImage.color = new Color(transparentImage.color.r, transparentImage.color.g, transparentImage.color.b, 0.5f);
        taskMessageText.text = message;
        taskMessageAnim.SetBool("isOpen", true);
    }
    public void StopTask()
    {
        transparentImage.color = new Color(transparentImage.color.r, transparentImage.color.g, transparentImage.color.b, 0);
        taskMessageText.text = "";
        taskMessageAnim.SetBool("isOpen", false);
    }
}
