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
            10,
            randomLerp);
        FireProjectile("BK_Proj", targetLocation, spawnLocation.position);

        attackCharging = false;
        newSize = 0;
    }
}
