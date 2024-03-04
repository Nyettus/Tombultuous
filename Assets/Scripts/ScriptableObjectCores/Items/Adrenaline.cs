using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/Adrenaline")]
public class Adrenaline : ItemBase
{
    public float healChance;
    public int healAmount;
}
