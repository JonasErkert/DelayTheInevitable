using System.Collections;
using System.Collections.Generic;
using ElRaccoone.Tweens;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void MouseEnterButton(RectTransform button)
    {
        button.TweenLocalScale(Vector3.one * 1.3f, 0.2f).SetFrom(Vector3.one).SetPingPong();
        button.TweenLocalRotation(new Vector3(0, 0, 5), 0.2f).SetFrom(Vector3.zero).SetEaseBounceInOut().SetPingPong();
    }
}
