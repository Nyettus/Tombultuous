using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Vector3 offset;
    void Update()
    {
        if (GameManager._.CheckMasterError()) return;
        transform.position = GameManager._.Master.transform.position + offset;
    }
}
