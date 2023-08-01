using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/testBoots")]
public class BigBoots : ItemBase
{
    public float bootTimeIncrease = 2f;
    public float bootSpeedIncrease = 10f;
    public AnimationCurve bootTimingCurve;
}
