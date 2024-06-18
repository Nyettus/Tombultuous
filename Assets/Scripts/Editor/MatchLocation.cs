using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectBind))]
public class MatchLocation : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObjectBind script = (ObjectBind)target;
        if (GUILayout.Button("Demo Location"))
        {
            script.MatchLocation();
        }
    }
}
