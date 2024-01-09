using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBind : MonoBehaviour
{
    public Transform toFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = toFollow.position;
        transform.rotation = toFollow.rotation;
    }
}
