using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBase : WeaponCore
{
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
    private List<EnemyHealth> enemiesHit = new List<EnemyHealth>();

    protected override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (shooting) Swing();
        if (hitboxTime < Time.time && quickRef.enabled)
        {
            quickRef.enabled = false;
            enemiesHit.Clear();
            if (!swingHit) GameManager._.Master.itemMaster.onMissEffectHandler.OnMissEffect(transform.position + transform.forward * hitboxLength);
            swingHit = false;
        }
    }

    protected override void Establish()
    {
        base.Establish();
        var meleeCard = (MeleeWeapon)card;
        damage = meleeCard.damage;
        swingSpeed = meleeCard.swingSpeed;

        hitboxWidth = meleeCard.hitboxWidth;
        hitboxLength = meleeCard.hitBoxLength;

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

    public override void OnMeleeHit(IEnemyDamageable HealthScript, float additive = 0)
    {
        if (!enemiesHit.Contains(HealthScript.GetEnemyHealthScript()))
        {
            var dmg = new DamageInstance(damage + additive)
            {
                multipliers = GameManager._.Master.weaponMaster.damageMult,
                damageType = DamageType.Melee
            };
            HealthScript.TakeDamage(dmg);
            enemiesHit.Add(HealthScript.GetEnemyHealthScript());
        }

        var casted = (MonoBehaviour)HealthScript;
        GameManager._.Master.itemMaster.onHitEffectHandler.OnHitEffect(casted.transform.position);
        swingHit = true;
    }
    public override void OnProjDeflect()
    {
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
    }

    public override void OnSwitch()
    {
        base.OnSwitch();
        quickRef.center = new Vector3(0, 0, hitboxLength / 2f);
        quickRef.size = new Vector3(hitboxWidth, hitboxWidth, hitboxLength);

    }
    protected override void OnDisable()
    {
        //the if statement fixes an editor issue
        if (quickRef != null) quickRef.enabled = false;
        enemiesHit.Clear();
        swingHit = false;
        base.OnDisable();

    }


}
