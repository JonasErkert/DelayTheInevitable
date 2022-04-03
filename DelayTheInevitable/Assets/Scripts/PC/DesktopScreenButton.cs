using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DesktopScreenButton : MonoBehaviour
{
    public UnityEvent buttonPressed;

    [SerializeField]
    private SpriteRenderer highlightSprite;


    private void OnEnable()
    {
        OnMouseExit();
    }


    private void OnMouseEnter()
    {
        highlightSprite.color = new Color(highlightSprite.color.r, highlightSprite.color.g, highlightSprite.color.b, 0.5f);
    }

    private void OnMouseExit()
    {
        highlightSprite.color = new Color(highlightSprite.color.r, highlightSprite.color.g, highlightSprite.color.b, 1);
    }

    private void OnMouseDown()
    {
        buttonPressed.Invoke();
    }
}
