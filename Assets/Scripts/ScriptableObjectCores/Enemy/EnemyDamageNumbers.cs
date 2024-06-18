using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyDamageNumbers", menuName = "Enemies/Enemy Damage Numbers")]
public class EnemyDamageNumbers : ScriptableObject
{
    public DamagePairs[] damageArray;
}
