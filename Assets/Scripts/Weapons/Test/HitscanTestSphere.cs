using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanTestSphere : RangedWeaponBase
{
    public override void Shoot()
    {
        Debug.Log("sphere shot");
    }
    public override void Special()
    {
        if (specialTime < Time.time)
        {
            base.Special();
            StartCoroutine(startSpecialCooldown());
        }

    }
}
