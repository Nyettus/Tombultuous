using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWalls : MonoBehaviour
{
    public GameObject solidWall;
    public GameObject doorWall;
    public bool walls = true;
    // Start is called before the first frame update
    void Start()
    {
        doorWall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            toggle();
        }
    }


    public void toggle()
    {
        if (walls)
        {
            solidWall.SetActive(false);
            doorWall.SetActive(true);
            walls = false;
        }
        else
        {
            solidWall.SetActive(true);
            doorWall.SetActive(false);
            walls = true;
        }
        
    }

}
