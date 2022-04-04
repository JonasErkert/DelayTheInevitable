using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopGameTutorial : MonoBehaviour
{
    private bool _triggerdTutorial;
    void Update()
    {
        if (GameManager.Instance.GetGameState() == GameState.Playing && GameManager.Instance.gameScreenOpen && !_triggerdTutorial)
        {
            _triggerdTutorial = true;
            Destroy(gameObject,4.0f);
        }
    }
    

}
