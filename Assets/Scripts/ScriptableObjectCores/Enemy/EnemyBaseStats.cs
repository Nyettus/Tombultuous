using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy Stats", menuName = "Enemies/EnemyStats")]

public class EnemyBaseStats : ScriptableObject
{
    public string enemyName;
    public string desc;
    public float health;
    public float moveSpeed;
    public int goldAmount;

}
