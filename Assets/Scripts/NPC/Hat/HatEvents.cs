using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatEvents : MonoBehaviour
{
    public void ReadyToGo()
    {
        PlayerPrefs.SetInt("NPC_Hat", 1);
        GameManager._.EndGame(true);
    }
}
