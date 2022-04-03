using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingManager : MonoBehaviour
{
    [SerializeField]
    private Boss bossScript;
    [SerializeField]
    private DesktopGameManager desktopManager;

    public void StartPlayState()
    {
        bossScript.StartBossDoor();
    }

}
