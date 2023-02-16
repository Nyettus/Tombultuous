using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponBase : WeaponCore
{
    [Header("Card")]
    public HitscanGun card;

    [Header("Basic Weapon Traits")]
    protected float maxDamage;
    protected float minDamage;
    protected int bullets;
    protected bool fullAuto;
    protected float fireRate;
    protected float spread;

    [Header("Weapon Falloff")]
    protected float minRange;
    protected float maxRange;

    [Header("Reloadable Traits")]
    public bool requireReload;
    public int magSize;
    public float reloadTime;


    [Header("Out of card logic times")]
    protected float shootSetTime;
    public int curMag;
    protected float reloadSetTime;
    public bool reloading;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Establish();
    }



    protected virtual void Establish()
    {
        weaponName = card.weaponName;
        atkDesc = card.normalDescription;
        spkDesc = card.specialDescription;
        description = card.lore;


        specialCooldown = card.cooldown;
        maxDamage = card.maxDamage;
        minDamage = card.minDamage;
        minRange = card.minRange;
        maxRange = card.maxRange;
        bullets = card.bullets;
        fireRate = card.fireRate;
        spread = card.spread;
        requireReload = card.requireReload;
        fullAuto = card.fullAuto;
        if (requireReload)
        {
            magSize = card.magSize;
            reloadTime = card.reloadTime;
            curMag = magSize;
        }
    }

    public override void Update()
    {
        base.Update();
        if (requireReload)
            ReloadFire();
        else
            FreeFire();
    }

    public void Reload()
    {
        curMag = magSize;
        reloading = false;

    }

    protected void FreeFire()
    {
        if (shooting && shootSetTime < Time.time)
        {
            if (fullAuto)
                Shoot();
            else
            {
                Shoot();
                shooting = false;
            }
            shootSetTime = Time.time + (1 / GameManager.instance.Master.weaponMaster.hasteMult);
        }

    }
    protected void ReloadFire()
    {
        if (shooting && shootSetTime < Time.time&&curMag>0 && !reloading)
        {
            if (fullAuto)
            {
                Shoot();
                curMag -= 1;
            }
            else
            {
                Shoot();
                shooting = false;
                curMag -= 1;
            }
            shootSetTime = Time.time + fireRate* (1/GameManager.instance.Master.weaponMaster.hasteMult);
        }
        if (curMag <= 0&&!reloading)
        {
            Invoke("Reload", reloadTime* (1 / GameManager.instance.Master.weaponMaster.hasteMult));
            reloading = true;
        }
    }


    protected Vector3 bulletSpread(float spread)
    {
        Vector3 targetPos = Camera.main.transform.position + Camera.main.transform.forward * 2;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-spread, spread),
            targetPos.y + Random.Range(-spread, spread),
            targetPos.z + Random.Range(-spread, spread));
        Vector3 direction = targetPos - Camera.main.transform.position;
        return direction.normalized;
    }

    protected float damageFalloff(float distance)
    {
        float normalised;
        normalised = Mathf.Clamp((distance - minRange) / (maxRange - minRange),0f,1f);
        return normalised * minDamage + (1 - normalised) * maxDamage;
    }

}
