using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomGridder))]
public class SearchRoomGrid : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RoomGridder gridder = (RoomGridder)target;
        if (GUILayout.Button("Search for room"))
        {
            gridder.DebugFindRoom();

        }



    }
}

