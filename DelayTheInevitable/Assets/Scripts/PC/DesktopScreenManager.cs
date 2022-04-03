using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject GameScreenParent;
    [SerializeField]
    private GameObject DesktopScreenParent;


    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.gameScreenOpen)
        {
            OpenGameScreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OpenGameScreen()
    {
        DesktopScreenParent.SetActive(false);
        GameScreenParent.SetActive(true);
        GameManager.Instance.gameScreenOpen = true;
    }

    private void OpenDesktopScreen()
    {
        GameScreenParent.SetActive(false);
        DesktopScreenParent.SetActive(true);
        GameManager.Instance.gameScreenOpen = false;
    }


    public void ClickedMinimize()
    {
        OpenDesktopScreen();
    }

    public void ClickedTaskbarGame()
    {
        if (!GameManager.Instance.gameScreenOpen) OpenGameScreen();
    }

    public void ClickedTaskbarDesktop()
    {
        if (GameManager.Instance.gameScreenOpen) OpenDesktopScreen();
    }
}
