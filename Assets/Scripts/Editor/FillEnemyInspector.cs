using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(CombatZone))]
public class FillEnemyInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CombatZone zone = (CombatZone)target;
        if (GUILayout.Button("Assign Enemies"))
        {
            zone.AssignEnemies();
            //PrefabStageUtility.GetCurrentPrefabStage();
        }


    }
}
