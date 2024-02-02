using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/LightningRod")]
public class LightningRod : ItemBase
{
    public float rate = 1;
    public float explosionDamage = 50;
    public float threshold = 100;
    public float radius = 3;

}
