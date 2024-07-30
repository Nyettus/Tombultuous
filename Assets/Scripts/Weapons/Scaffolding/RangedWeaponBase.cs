using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponBase : WeaponCore
{
    [Header("Basic Weapon Traits")]

    protected int bullets;
    protected bool fullAuto;
    protected float fireRate;
    protected float spread;



    [Header("Reloadable Traits")]
    public bool requireReload;
    public int magSize;
    public float reloadTime;
    public float reloadPercentage;
    private float reloadTicker;


    [Header("Out of card logic times")]
    protected float shootSetTime;
    public int curMag;
    protected float reloadSetTime;
    public bool reloading = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }



    protected override void Establish()
    {
        base.Establish();
        var rangedCard = (RangedGunBase)card;
        bullets = rangedCard.bullets;
        fireRate = rangedCard.fireRate;
        spread = rangedCard.spread;
        requireReload = rangedCard.requireReload;
        fullAuto = rangedCard.fullAuto;
        if (requireReload)
        {
            magSize = rangedCard.magSize;
            reloadTime = rangedCard.reloadTime;
            curMag = magSize;
        }
    }

    public override void Update()
    {
        base.Update();
        if (requireReload)
        {
            ReloadFire();
            if (reloading)
            {

                reloadPercentage = Mathf.Clamp((reloadTicker += (Time.deltaTime * Time.timeScale)) / (reloadTime * (GameManager._.Master.weaponMaster.hasteMult)), 0, 1);

                GameManager._.Master.weaponMaster.OnReloadChangeEvent();
            }
            else if (reloadTicker != 0)
            {
                reloadPercentage = 1;
                GameManager._.Master.weaponMaster.OnReloadChangeEvent();
                reloadTicker = 0;

            }
        }

        else
            FreeFire();
    }

    public virtual void Reload()
    {
        curMag = magSize;
        reloading = false;
        GameManager._.Master.weaponMaster.OnAmmoChangeEvent();

    }

    protected void FreeFire()
    {
        if (GameManager._.ToggleInputs()) return;
        if (shooting && shootSetTime < Time.time)
        {
            if (fullAuto)
                Shoot();
            else
            {
                Shoot();
                shooting = false;
            }
            shootSetTime = Time.time + fireRate * (GameManager._.Master.weaponMaster.hasteMult);
        }

    }
    protected void ReloadFire()
    {
        if (GameManager._.ToggleInputs()) return;
        if (shooting && shootSetTime < Time.time && curMag > 0 && !reloading)
        {
            if (fullAuto)
            {
                curMag -= 1;
                Shoot();
            }
            else
            {
                curMag -= 1;
                Shoot();
                shooting = false;
            }
            shootSetTime = Time.time + fireRate * (GameManager._.Master.weaponMaster.hasteMult);
        }
        if (curMag <= 0 && !reloading)
        {
            StartReload();

        }
    }
    public virtual void StartReload()
    {
        if (reloading) return;

        Invoke("Reload", reloadTime * (GameManager._.Master.weaponMaster.hasteMult));
        reloading = true;
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



}
