using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : ScriptableObject
{
    [Header("Prefab")]
    public GameObject prefab;
    [Header("Descriptions")]
    public string weaponName;
    public string normalDescription;
    public string specialDescription;
    public string lore;

    [Header("Special Move")]
    public float cooldown;
}
