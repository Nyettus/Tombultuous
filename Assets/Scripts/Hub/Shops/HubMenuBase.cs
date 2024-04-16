using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubMenuBase : MonoBehaviour
{
    public Canvas thisCanvas;
    protected virtual void Start()
    {
        thisCanvas = GetComponent<Canvas>();
    }

    public void FixedUpdate()
    {
        if (thisCanvas.enabled) GameManager._.ShowMouse(true);
    }
    public virtual void SetMenu(bool state)
    {

        GameManager._.ShowMouse(true);

        thisCanvas.enabled = state;
        GameManager._.inMenu = state;
        GameManager._.whichMenu = thisCanvas;

    }
}
