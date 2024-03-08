using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/Laudanum")]
public class Laudanum : ItemBase
{
    public float laudTimeIncrease = 5f;
    public float laudSpeedIncrease = 5f;
    public AnimationCurve laudSpeedControl;
}
