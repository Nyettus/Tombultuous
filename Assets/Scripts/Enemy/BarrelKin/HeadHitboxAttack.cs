using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitboxAttack : EnemyDamage
{
    [SerializeField] private int HeadbuttDamage = 50;
    [SerializeField] private int ChargeDamage = 10;



    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        if (CM.enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack b"))
            damage = HeadbuttDamage;
        else
            damage = ChargeDamage;

        base.OnTriggerEnter(other);
        damage = ChargeDamage;
    }
}
