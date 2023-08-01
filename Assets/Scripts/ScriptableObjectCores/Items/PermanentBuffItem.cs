using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatBuff
{
    public StatType type;
    public float change;
}

[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/PermanentBuffItem") ]
public class PermanentBuffItem : ItemBase
{
    public StatBuff[] buffs;

}
