using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class LG_Attacks : BaseEnemyAttacks
{
    private bool isSpinning = false;
    private EnemyDamage[] spinHitboxes = new EnemyDamage[2];

    public string phaseName;
    public float phaseTransitionPercent;
    private bool once = true;


    private void Start()
    {
        falseProjMaxSize = tripProjFalse[0].transform.localScale.x;
    }
    private void LateUpdate()
    {
        ResetSpin();
        TripProjLerpFalse();
    }

    public override void CheckHealthPercent()
    {
        if (!once) return;
        float totalHealth = CM.card.health;
        float currentHealth = CM.enemyHealth.health;
        float healthPercent = currentHealth / totalHealth;
        Debug.Log("Health percentage: " + healthPercent);
        if (phaseTransitionPercent > healthPercent)
        {
            CM.enemyAnimator.SetBool(phaseName, true);
            once = false;
        }

    }


    #region Neutral kick chain
    public void LG_NeutralKick_ON()
    {
        GenericAttack_ON(0, 0);

    }

    public void LG_NeutralKick_OFF()
    {
        GenericAttack_OFF(0);
    }

    public void LG_ShoulderBash_ON()
    {
        GenericAttack_ON(1, 1);
    }

    public void LG_ShoulderBash_OFF()
    {
        GenericAttack_OFF(1);
    }

    #endregion

    #region Clap chain
    public void LG_Clap_ON()
    {
        EnemyDamage[] hitboxes = new EnemyDamage[2];
        hitboxes[0] = GenericAttack_ON(2, 2);
        hitboxes[1] = GenericAttack_ON(3, 2);
        //Reassign knockback duration to be directly up
        var damagePair = damageValues.damageArray[2];
        Vector3 direction = (GameManager._.Master.transform.position - transform.position + Vector3.up * 10).normalized;
        foreach (EnemyDamage hitbox in hitboxes)
        {
            hitbox.AssignValues(damagePair, direction);
        }
    }

    public void LG_Clap_OFF()
    {
        GenericAttack_OFF(2);
        GenericAttack_OFF(3);
    }

    public void LG_ClapFollowup_ON()
    {
        GenericAttack_ON(2, 3);
    }

    public void LG_ClapFollowup_OFF()
    {
        GenericAttack_OFF(2);
    }

    #endregion

    #region drop kick
    public void LG_DropKick_ON()
    {
        var hitbox = GenericAttack_ON(0, 4);
        var damagePair = damageValues.damageArray[4];
        Vector3 direction = (GameManager._.Master.transform.position - transform.position + Vector3.up * 2).normalized;
        hitbox.AssignValues(damagePair, direction);

    }

    public void LG_DropKickPart2_ON()
    {
        var hitbox = GenericAttack_ON(0, 5);
        var damagePair = damageValues.damageArray[5];
        hitbox.AssignValues(damagePair, Vector3.up);
    }

    public void LG_DropKick_OFF()
    {
        GenericAttack_OFF(0);
    }





    #endregion

    #region shpin

    public void LG_Spin_ON()
    {
        spinHitboxes = new EnemyDamage[2];
        spinHitboxes[0] = GenericAttack_ON(2, 6);
        spinHitboxes[1] = GenericAttack_ON(3, 6);
        isSpinning = true;
    }

    public void LG_Spin_OFF()
    {
        LG_Clap_OFF();
        isSpinning = false;
    }

    private void ResetSpin()
    {
        if (!isSpinning) return;
        foreach (EnemyDamage hitbox in spinHitboxes)
        {
            if (hitbox.canHit) continue;
            hitbox.canHit = true;
        }
    }
    #endregion

    #region Triple Projectile Attack
    //Arrays fill from Right to Left (Attacker Perspective)
    [SerializeField] private ProjectileType chosenProj;
    [SerializeField] private Transform[] tripProjLocation = new Transform[3];
    [SerializeField] private GameObject[] tripProjFalse = new GameObject[3];
    [SerializeField] private ParticleSystem[] tripProjEffect = new ParticleSystem[3];
    private bool[] tripProjCharging = new bool[3];

    //False proj aesthetic rates
    private float falseProjMaxSize;
    private float[] falseProjSize = new float[3];
    private float rate = 1.5f;

    public void GenericStartTripProj(int index)
    {
        GameObject thisProj = tripProjFalse[index];
        thisProj.transform.localScale = Vector3.zero;
        tripProjEffect[index].Play();
        thisProj.SetActive(true);
        tripProjCharging[index] = true;

    }
    public void GenericActivateTripProjAttack(int index, float accuracy)
    {
        GameObject thisProj = tripProjFalse[index];
        Transform thisSpawn = tripProjLocation[index];
        thisProj.SetActive(false);

        Vector3 targetLocal = GameManager._.Master.transform.position + Vector3.up * (GameManager._.Master.movementMaster.height / 4);
        Vector3 targetVel = GameManager._.Master.movementMaster.rb.velocity;
        var targetLocation = MurderBag.RoughPredictLocation(
            targetLocal,
            targetVel,
            tripProjLocation[index].position
            ,chosenProj.speed,accuracy);
        FireProjectile("LG_Proj", targetLocation, thisSpawn.position);

        tripProjCharging[index] = false;
        falseProjSize[index] = 0;
    }

    private void TripProjLerpFalse()
    {
        for(int i = 0; i < falseProjSize.Length; i++)
        {
            if (!tripProjCharging[i]) continue;
            falseProjSize[i] = Mathf.Lerp(falseProjSize[i], falseProjMaxSize, rate * Time.deltaTime);
            tripProjFalse[i].transform.localScale = new Vector3(falseProjSize[i], falseProjSize[i], falseProjSize[i]);
        }
    }

    #region Fire Right
    public void LG_StartTripProjR()
    {
        GenericStartTripProj(0);
    }

    public void LG_FireTripProjR()
    {
        GenericActivateTripProjAttack(0, 0f);
    }
    #endregion

    #region Fire Center
    public void LG_StartTripProjC()
    {
        GenericStartTripProj(1);
    }

    public void LG_FireTripProjC()
    {
        GenericActivateTripProjAttack(1, 1f);
    }
    #endregion

    #region Fire Left
    public void LG_StartTripProjL()
    {
        GenericStartTripProj(2);
    }

    public void LG_FireTripProjL()
    {
        GenericActivateTripProjAttack(2, 0.5f);
    }
    #endregion


    #endregion

    #region Ground Pound
    [SerializeField] private ParticleSystem GroundPoundParticles;
    public void LG_GroundPound_ON()
    {
        var hitbox = GenericAttack_ON(4, 7);
        var damagePair = damageValues.damageArray[7];
        hitbox.AssignValues(damagePair, Vector3.up);
        GroundPoundParticles.Play();
    }

    public void LG_GroundPound_OFF()
    {
        GenericAttack_OFF(4);
    }
    #endregion

    #region Phase 2 Swipe
    public void LG_Phase2SwipeP1_ON()
    {
        GenericAttack_ON(5, 8);
    }
    public void LG_Phase2SwipeP1_OFF()
    {
        GenericAttack_OFF(5);
    }

    public void LG_Phase2SwipeP2_ON()
    {
        GenericAttack_ON(5, 9);
    }
    public void LG_Phsae2SwipeP2_OFF()
    {
        GenericAttack_OFF(5);
    }

    #endregion
}
