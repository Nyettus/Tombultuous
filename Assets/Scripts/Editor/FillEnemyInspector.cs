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
        PrefabStage prefabScene = PrefabStageUtility.GetCurrentPrefabStage();
        if (GUILayout.Button("Assign Enemies"))
        {
            zone.AssignEnemies();
            if (prefabScene != null)
            {
                
                EditorSceneManager.MarkSceneDirty(prefabScene.scene);
            }
            else
            {
                PrefabUtility.ApplyPrefabInstance(zone.gameObject, InteractionMode.UserAction);
            }
        }


    }
}
