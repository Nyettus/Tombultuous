using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("im dead");
        }
        else
        {
            Debug.Log("YOUCH");
        }
    }

}
