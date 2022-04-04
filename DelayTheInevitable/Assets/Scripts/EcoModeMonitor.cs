using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcoModeMonitor : MonoBehaviour
{
    [SerializeField] private Image monitorScreen;
    [SerializeField] private SpriteRenderer indicatorLight;
    [SerializeField] private GameObject ecoModeHint;
    [SerializeField] private float fadeDuration = 2.0f;
    private bool isInEcoMode = false;

    [SerializeField] private DesktopGameManager desktopManager;

    private void Update()
    {
        //TESTING ONLY
        /*if (Input.GetKeyDown(KeyCode.L))
        {
            ActivateEcoMode();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            DeactivateEcoMode();
        }*/
        //######
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isInEcoMode)
            {
                DeactivateEcoMode();
            }
            else
            {
                ActivateEcoMode();
            }
        }
    }

    public void ActivateEcoMode()
    {
        isInEcoMode = true;
        StopAllCoroutines();
        StartCoroutine(FadeMonitor(false, fadeDuration));
        ecoModeHint.SetActive(true);
    }

    private void DeactivateEcoMode()
    {
        isInEcoMode = false;
        StopAllCoroutines();
        StartCoroutine(FadeMonitor(true, fadeDuration));
        desktopManager.ContinueWriting();
        GameManager.Instance.isWorkingOnTask = false;
        ecoModeHint.SetActive(false);
    }


    IEnumerator FadeMonitor(bool fadeIn, float duration)
    {
        float elapsed = 0.0f;
        Color startColorMonitor = monitorScreen.color;
        Color startColorIndicator = indicatorLight.color;
        while (elapsed < duration)
        {
            if (fadeIn)
            {
                monitorScreen.color = Color.Lerp(startColorMonitor, new Color(startColorMonitor.r, startColorMonitor.g, startColorMonitor.b, 0), elapsed);
                indicatorLight.color = Color.Lerp(startColorIndicator, Color.green, elapsed);
            }
            else
            {
                monitorScreen.color = Color.Lerp(startColorMonitor, new Color(startColorMonitor.r, startColorMonitor.g, startColorMonitor.b, 1), elapsed);
                indicatorLight.color = Color.Lerp(startColorIndicator, Color.yellow, elapsed);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    
    
    
    
    
}
