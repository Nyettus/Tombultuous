using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RoomManager))]
public class RestartLevelGen : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RoomManager manager = (RoomManager)target;
        if (GUILayout.Button("Restart Generation"))
        {
            manager.ReloadScene();

        }



    }
}
