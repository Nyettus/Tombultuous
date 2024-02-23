using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrackLocation : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float targetY;

    public bool matchRotation;

    public bool pauseTrack = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (pauseTrack) return;
        MatchLocation();
    }

    public void MatchLocation()
    {
        Vector3 pos = target.position;
        pos.y = targetY;
        transform.position = pos;
        if (matchRotation) transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0);
    }
}
