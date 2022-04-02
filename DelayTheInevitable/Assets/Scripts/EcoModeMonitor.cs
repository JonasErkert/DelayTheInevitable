using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoModeMonitor : MonoBehaviour
{
    [SerializeField] private Material monitorScreen;
    [SerializeField] private Light indicatorLight;
    [SerializeField] private float fadeDuration = 2.0f;
    private bool isInEcoMode = false;

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

    private void ActivateEcoMode()
    {
        isInEcoMode = true;
        StopAllCoroutines();
        StartCoroutine(FadeMonitor(false, fadeDuration));
    }

    private void DeactivateEcoMode()
    {
        isInEcoMode = false;
        StopAllCoroutines();
        StartCoroutine(FadeMonitor(true, fadeDuration));
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
                monitorScreen.color = Color.Lerp(startColorMonitor, Color.white, elapsed);
                indicatorLight.color = Color.Lerp(startColorIndicator, Color.green, elapsed);
            }
            else
            {
                monitorScreen.color = Color.Lerp(startColorMonitor, Color.black, elapsed);
                indicatorLight.color = Color.Lerp(startColorIndicator, Color.yellow, elapsed);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    
    
    
    
    
}
