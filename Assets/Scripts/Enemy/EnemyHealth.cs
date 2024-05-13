using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public int goldAmount;

    public EnemyCountHandler countHandler;
    private bool once = true;
    private EnemyComponentMaster CM;


    private void Start()
    {
        countHandler = GetComponent<EnemyCountHandler>();
        if (TryGetComponent<EnemyComponentMaster>(out EnemyComponentMaster C)) CM = C;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (CM.enemyBoss != null) CM.enemyBoss.OnBossHealthChangeEvent();
        if (health <= 0 && once)
        {
            Debug.Log("im dead");
            if (GameManager._.goldManager != null) GameManager._.goldManager.GetGold(goldAmount);
            GameManager._.Master.itemMaster.onKillItemHandler.OnKill();
            countHandler.RemoveFromMaster();
            if (CM != null)
            {
                CM.enemyAnimator.SetTrigger("IsDead");

            }
            once = false;
        }

    }

    public void FindHitboxes()
    {
        var allHitbox = GetComponentsInChildren<EnemyHitbox>();
        if (allHitbox == null)
        {
            Debug.LogWarning("No Hitboxes found");
            return;
        }

        Debug.Log("" + allHitbox.Length + " Hitboxes Found");
        foreach(EnemyHitbox hitbox in allHitbox)
        {
            hitbox.host = this;
        }

    }
}
