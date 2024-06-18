using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;


public class BarrelKin_Attacks : BaseEnemyAttacks
{
    [SerializeField] private EnemyDamageNumbers damageValues;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private ParticleSystem projectileParticles;
    [SerializeField] private GameObject falseProj;
    //[SerializeField] private 
    private float falseProjSize;
    private bool attackCharging = false;

    private float newSize = 0;
    private float rate = 1.5f;
    private void Start()
    {
        falseProjSize = falseProj.transform.localScale.x;
    }

    public void BK_ProjectileStart()
    {

        falseProj.transform.localScale = Vector3.zero;
        projectileParticles.Play();
        falseProj.SetActive(true);
        attackCharging = true;
    }


    private void Update()
    {
        if (attackCharging)
        {
            newSize = Mathf.Lerp(newSize, falseProjSize, rate * Time.deltaTime);
            falseProj.transform.localScale = new Vector3(newSize, newSize, newSize);
        }
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
    public void BK_KickAttack_ON()
    {
        EnemyDamage hitbox = base.Hitboxes[0];
        hitbox.AssignValues(damageValues.damageArray[0]);
        hitbox.hitbox.enabled = true;
    }


    public void BK_KickAttack_OFF()
    {
        EnemyDamage hitbox = base.Hitboxes[0];
        hitbox.hitbox.enabled = false;
        canHit = true;
    }

    public void BK_HeadbuttAttack_ON()
    {
        EnemyDamage hitbox = base.Hitboxes[1];
        hitbox.AssignValues(damageValues.damageArray[1]);
        hitbox.hitbox.enabled = true;
    }

    public void BK_Charge_ON()
    {
        EnemyDamage hitbox = base.Hitboxes[1];
        hitbox.AssignValues(damageValues.damageArray[2]);
        hitbox.hitbox.enabled = true;
    }

    public void BK_Headhitbox_OFF()
    {
        EnemyDamage hitbox = base.Hitboxes[1];
        hitbox.hitbox.enabled = false;
        canHit = true;
    }






}
