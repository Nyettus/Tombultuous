using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBase : WeaponCore
{
    [Header("Card")]
    public MeleeWeapon card;

    [Header("Weapon Traits")]
    protected float damage;
    protected float swingSpeed;

    [Header("Out of Card logic")]
    protected float swingTime;


    protected override void Start()
    {
        base.Start();
        Establish();
    }

    public override void Update()
    {
        base.Update();
        if (shooting) Swing();
    }

    protected virtual void Establish()
    {
        weaponName = card.weaponName;
        atkDesc = card.normalDescription;
        spkDesc = card.specialDescription;
        description = card.lore;

        specialCooldown = card.cooldown;
        damage = card.damage;
        swingSpeed = card.swingSpeed;
    }

    protected void Swing()
    {
        if (GameManager._.paused) return;
        if (shooting && swingTime < Time.time)
        {
            Shoot();
            swingTime = Time.time + swingSpeed * GameManager._.Master.weaponMaster.hasteMult;
        }
    }

    public override void Special()
    {

        if (!specialUsed)
        {
            Debug.Log("Special used");
            base.Special();
        }

    }

    public override void Shoot()
    {
        Debug.Log("Swing");
    }


}
