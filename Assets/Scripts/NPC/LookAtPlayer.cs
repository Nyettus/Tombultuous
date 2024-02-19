using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookAtPlayer : MonoBehaviour
{
    public Rig rig;
    private float desiredWeight = 0;
    [SerializeField]
    private float rate = 50;

    private void Update()
    {
        lerpTowardTarget();
    }

    private void lerpTowardTarget()
    {
        if (rig.weight == desiredWeight) return;
        rig.weight = Mathf.Lerp(rig.weight, desiredWeight, rate * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        desiredWeight = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        desiredWeight = 0;
    }


}
