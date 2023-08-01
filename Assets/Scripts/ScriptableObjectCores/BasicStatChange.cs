using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "BasicStatChange",menuName = "Items/movementItems") ]
public class BasicStatChange : ScriptableObject
{
    public string itemName;
    public string itemDesc;

    public int[] statRef;
    public float[] statChange;
}
