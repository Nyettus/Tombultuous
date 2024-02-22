using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MeleeWeaponBase
{
    private InvulnManager IM => GameManager._.Master.invulnManager;
    private float blockDuration = 1f;
    [SerializeField] private Animator anim;
    private int damageAculm { get => IM.shovelAcculm; set => IM.shovelAcculm = value; }

    public override void OnMeleeHit(EnemyHealth HealthScript, float additive)
    {

        base.OnMeleeHit(HealthScript, damageAculm);
        damageAculm = 0;

    }
    public override void Special()
    {
        if (!specialUsed)
        {
            damageAculm = 0;
            IM.SetShovel(true, blockDuration);
            anim.SetTrigger("Special");

            base.Special();
        }

    }

    protected override void Swing()
    {
        if (IM.shovelInvuln) return;
        base.Swing();
    }

    public override void Shoot()
    {
        base.Shoot();
        anim.SetTrigger("Swing");
    }

    public void OnDisable()
    {
        IM.SetShovel(false, blockDuration);
    }
}
