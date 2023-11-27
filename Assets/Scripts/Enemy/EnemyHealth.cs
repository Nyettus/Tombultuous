using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public int goldAmount;

    public EnemyCountHandler countHandler;
    private bool once = true;
    private void Start()
    {
        countHandler = GetComponent<EnemyCountHandler>();
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        Debug.Log(string.Format("Took {0} damage ", damage));
        if (health <= 0 && once)
        {
            Debug.Log("im dead");
            GameManager._.goldManager.GetGold(goldAmount);
            countHandler.RemoveFromMaster();
            once = false;
        }
        else
        {
            Debug.Log("YOUCH");
        }
    }

}
