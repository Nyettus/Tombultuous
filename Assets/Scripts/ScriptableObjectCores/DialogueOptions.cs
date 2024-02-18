using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string sentence;

}


[CreateAssetMenu(fileName = "new Dialogue",menuName = "NPC/Dialogue")]
public class DialogueOptions : ScriptableObject
{
    public Dialogue[] lines;

}
