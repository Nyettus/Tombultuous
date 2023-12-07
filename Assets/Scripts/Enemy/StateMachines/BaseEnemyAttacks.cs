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


        if (Hitboxes[index] == null) return;
        Hitboxes[index].enabled = theBool;
        canHit = theBool;


    }

    public void DamagePlayer(int damage)
    {
        if (!canHit) return;
        GameManager._.Master.healthMaster.takeDamage(damage);
        canHit = false;
    }





}
