using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(EnemyHealth))]
public class AssignHitboxes : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnemyHealth zone = (EnemyHealth)target;
        if (GUILayout.Button("Assign Hitboxes"))
        {
            zone.FindHitboxes();
        }


    }
}
