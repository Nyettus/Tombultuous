using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundCard), true)]
public class SoundCardEditor : Editor
{
    SoundCard card;
    public static AudioSource sound;
    Texture2D lol;
    private void OnEnable()
    {
        sound = KongrooUtils.FindObjectsWithTagAndType<AudioSource>("EditorUtils").FirstOrDefault();
        //Debug.Log($"Enabling Soundcard {card}");
        var thing = AssetDatabase.LoadAssetAtPath($"Assets/Resources/Secret.png", typeof(Texture2D));
        //lol = (Texture2D)Resources.Load("Stickbugged.png");
        lol = (Texture2D)thing;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        card = (SoundCard)target;
        GUILayout.Space(20);

        //if (GUILayout.Button("Preview", new GUILayoutOption[] { GUILayout.Height(40) }))
        var content = new GUIContent("     Preview", lol);
        if (GUILayout.Button(content, GUILayout.MaxHeight(69)))
        {
            card.PlayRandomOneShot(sound);
        }
    }
}
