using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("On Start");
        gameObject.transform.position = new Vector3(50, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
