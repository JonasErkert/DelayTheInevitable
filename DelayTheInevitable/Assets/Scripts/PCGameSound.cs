using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGameSound : MonoBehaviour
{
    private AudioSource audioSrc;

    private DesktopGameManager gameManager;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        gameManager = GetComponent<DesktopGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetGameState() == GameState.Playing || GameManager.Instance.GetGameState() == GameState.GameOver)
        {
            if (GameManager.Instance.gameScreenOpen)
            {
                if (audioSrc.mute) audioSrc.mute = false;
            }
            else
            {
                if (!audioSrc.mute) audioSrc.mute = true;
            }
        }
    }
}
