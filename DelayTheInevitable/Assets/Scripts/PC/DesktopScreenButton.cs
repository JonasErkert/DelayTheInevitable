using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DesktopScreenButton : MonoBehaviour
{
    public UnityEvent buttonPressed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*
     *     public UnityEvent flipped;
 
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) Flip();
    }
 
    void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        flipped.Invoke();
    }

     */

    private void OnMouseDown()
    {
        buttonPressed.Invoke();
        Debug.Log("MOUSE DOWN");
    }
}
