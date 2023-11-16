using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewmodelCamera : MonoBehaviour
{
    Camera thisCam;
    // Start is called before the first frame update
    void Start()
    {
        thisCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        matchFOV();
    }


    private void matchFOV()
    {
        if(thisCam.fieldOfView != Camera.main.fieldOfView)
        {
            thisCam.fieldOfView = Camera.main.fieldOfView;
        }
    }
}
