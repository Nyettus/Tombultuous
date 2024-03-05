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
            SetTiers(zone);            
        }


    }


    private void SetTiers(ItemPools itemPools)
    {
        foreach (ItemBase item in itemPools.tier1)
        {
            item.tier = (int)PoolTiers.Tier1 + 1;
            EditorUtility.SetDirty(item);
        }
        foreach (ItemBase item in itemPools.tier2)
        {
            item.tier = (int)PoolTiers.Tier2 + 1;
            EditorUtility.SetDirty(item);
        }
        foreach (ItemBase item in itemPools.tier3)
        {
            item.tier = (int)PoolTiers.Tier3 + 1;
            EditorUtility.SetDirty(item);
        }
        foreach (ItemBase item in itemPools.tier4)
        {
            item.tier = (int)PoolTiers.Tier4 + 1;
            EditorUtility.SetDirty(item);
        }
    }
}
