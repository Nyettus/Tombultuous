using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager._.TransitionScene((int)Scenes.TS1Spawn);
    }
}
