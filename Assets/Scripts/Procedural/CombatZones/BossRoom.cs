using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private bool special;
    [SerializeField] private GameObject specialSpawn;

    public virtual void OnBossKill()
    {
        if (special)
        {
            specialSpawn.SetActive(true);
        }
        else
        {
            nextLevel.SetActive(true);
        }
    }
}
