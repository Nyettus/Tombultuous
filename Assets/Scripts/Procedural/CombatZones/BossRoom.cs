using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private bool special;
    [SerializeField] private GameObject specialSpawn;
    [SerializeField] private string prefName;

    public virtual void OnBossKill()
    {
        special = !ReturnSpecial(prefName);
        if (special)
        {
            specialSpawn.SetActive(true);
        }
        else
        {
            nextLevel.SetActive(true);
        }
    }

    public bool ReturnSpecial(string prefName)
    {
        return PlayerPrefs.GetInt(prefName, 0) >= 1;


    }

}
