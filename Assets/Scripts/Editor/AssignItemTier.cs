using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemPools))]
public class AssignItemTier : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ItemPools zone = (ItemPools)target;
        
        if (GUILayout.Button("Assign Item Tiers"))
        {
            zone.AssignTiers();
        }


    }
}
