using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSet", menuName = "Tile Sets")]
public class TileSet : ScriptableObject
{
    public GameObject[] _1x1;
    public GameObject[] _1x2;
    public GameObject[] _2x2;
}
