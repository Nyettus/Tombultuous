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

    [Header("Hitbox Traits")]
    protected float hitboxLength;
    protected float hitboxWidth;

    [Header("Out of Card logic")]
    protected float swingTime;
    protected BoxCollider quickRef => GameManager._.Master.weaponMaster.meleeHitbox;
    protected float hitboxTime;
    protected float hitboxDuration = 0.3f;

    [SerializeField] private bool swingHit = false;


    protected override void Start()
    {
        base.Start();
        Establish();
    }

    public override void Update()
    {
        base.Update();
        if (shooting) Swing();
        if (hitboxTime < Time.time && quickRef.enabled)
        {
            quickRef.enabled = false;
            if (!swingHit) GameManager._.Master.itemMaster.onMissEffectHandler.OnMissEffect(transform.position + transform.forward * hitboxLength);
            swingHit = false;
        }
    }

    protected virtual void Establish()
    {
        prefab = card.prefab;
        weaponName = card.weaponName;
        atkDesc = card.normalDescription;
        spkDesc = card.specialDescription;
        description = card.lore;

        specialCooldown = card.cooldown;
        damage = card.damage;
        swingSpeed = card.swingSpeed;

        hitboxWidth = card.hitboxWidth;
        hitboxLength = card.hitBoxLength;

    }

    protected virtual void Swing()
    {

        if (GameManager._.paused) return;
        if (shooting && swingTime < Time.time)
        {

            Shoot();
            swingTime = Time.time + swingSpeed * GameManager._.Master.weaponMaster.hasteMult;
        }
    }

    public override void OnMeleeHit(EnemyHealth HealthScript, float additive = 0)
    {
        float damage = (this.damage + additive) * GameManager._.Master.weaponMaster.damageMult;
        HealthScript.takeDamage(damage);
        GameManager._.Master.itemMaster.onHitEffectHandler.OnHitEffect(HealthScript.transform.position);
        swingHit = true;
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
        quickRef.enabled = false;
        quickRef.enabled = true;
        hitboxTime = Time.time + hitboxDuration;
        Debug.Log("Swing");
    }

    public override void OnSwitch()
    {
        base.OnSwitch();
        quickRef.center = new Vector3(0, 0, hitboxLength / 2f);
        quickRef.size = new Vector3(hitboxWidth, hitboxWidth, hitboxLength);

    }
    private void OnDisable()
    {
        GameManager._.Master.weaponMaster.meleeHitbox.enabled = false;
        swingHit = false;
    }


}
