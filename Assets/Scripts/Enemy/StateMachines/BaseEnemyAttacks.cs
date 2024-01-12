using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAttacks : MonoBehaviour
{
    public BoxCollider[] Hitboxes;

    public bool canHit = true;

    public void SetHitbox(AnimationEvent eventer)
    {
        bool theBool = eventer.intParameter != 0;
        int index = (int)eventer.floatParameter;
        if (Hitboxes[index] == null)
        {
            Debug.LogError("HitboxIndex null");
            return;
        }
        Hitboxes[index].enabled = theBool;
        canHit = theBool;


    }

    public void DamagePlayer(int damage,Vector3 direction,float magnitude)
    {
        if (!canHit) return;
        GameManager._.Master.healthMaster.takeDamage(damage);
        GameManager._.Master.movementMaster.KnockBack(direction, magnitude);
        canHit = false;
        Debug.Log("Dealt: " + damage + " to the player");
    }





}
