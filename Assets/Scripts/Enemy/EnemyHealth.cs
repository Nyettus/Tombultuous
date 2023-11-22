using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    public EnemyCountHandler countHandler;

    private void Start()
    {
        countHandler = GetComponent<EnemyCountHandler>();
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        Debug.Log(string.Format("Took {0} damage ", damage));
        if (health <= 0)
        {
            Debug.Log("im dead");
            countHandler.RemoveFromMaster();
        }
        else
        {
            Debug.Log("YOUCH");
        }
    }

}
