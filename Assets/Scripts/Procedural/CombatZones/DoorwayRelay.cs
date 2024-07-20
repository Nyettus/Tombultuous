using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayRelay : MonoBehaviour
{
    [SerializeField] private DoorwayTrigger trigger;
    public void CD_SlamParticles()
    {
        trigger.slamParticles.Play();
    }
}
