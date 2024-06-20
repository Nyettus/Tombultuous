using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;


public class BarrelKin_Attacks : BaseEnemyAttacks
{
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private ParticleSystem projectileParticles;
    [SerializeField] private GameObject falseProj;
    private float falseProjSize;
    private bool attackCharging = false;

    private float newSize = 0;
    private float rate = 1.5f;
    private void Start()
    {
        falseProjSize = falseProj.transform.localScale.x;
    }



    private void Update()
    {
        if (attackCharging)
        {
            newSize = Mathf.Lerp(newSize, falseProjSize, rate * Time.deltaTime);
            falseProj.transform.localScale = new Vector3(newSize, newSize, newSize);
        }
    }
    #region Projectile Attack
    public void BK_ProjectileStart()
    {

        falseProj.transform.localScale = Vector3.zero;
        projectileParticles.Play();
        falseProj.SetActive(true);
        attackCharging = true;
    }


    public void BK_ProjectileAttack()
    {
        falseProj.SetActive(false);
        var quickref = GameManager._.Master;
        float randomLerp = Random.value;
        var targetLocation = MurderBag.RoughPredictLocation(
            quickref.transform.position + Vector3.up * (quickref.movementMaster.height / 4),
            quickref.movementMaster.rb.velocity,
            transform.position,
            20,
            randomLerp);
        FireProjectile("BK_Proj", targetLocation, spawnLocation.position);

        attackCharging = false;
        newSize = 0;
    }
    #endregion

    #region Kick attack
    public void BK_KickAttack_ON()
    {
        GenericAttack_ON(0, 0);
    }


    public void BK_KickAttack_OFF()
    {
        GenericAttack_OFF(0);

    }
    #endregion

    #region head based attacks
    public void BK_HeadbuttAttack_ON()
    {
        GenericAttack_ON(1, 1);
    }

    public void BK_Charge_ON()
    {
        GenericAttack_ON(1, 2);
    }

    public void BK_Headhitbox_OFF()
    {
        GenericAttack_OFF(1);

    }
    #endregion





}
