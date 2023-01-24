using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideMenu : MonoBehaviour
{
    private Canvas itself;
    
    // Start is called before the first frame update
    void Start()
    {
        itself = GetComponent<Canvas>();
        itself.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            itself.enabled = true;
        }
        else
        {
            itself.enabled = false;
        }
    }
}
