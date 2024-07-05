using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class LG_Attacks : BaseEnemyAttacks
{
    private bool isSpinning = false;
    private EnemyDamage[] spinHitboxes = new EnemyDamage[2];

    [SerializeField] private string phaseName;
    [SerializeField] private float phaseTransitionPercent;
    [SerializeField] private bool isPhase2;
    [SerializeField] private bool once = true;


    private void Start()
    {

    }
    private void LateUpdate()
    {
        ResetSpin();
        LG_Phase2LerpFalse();
    }

    public override void CheckHealthPercent()
    {
        if (!once) return;
        float totalHealth = CM.card.health;
        float currentHealth = CM.enemyHealth.health;
        float healthPercent = currentHealth / totalHealth;
        if (phaseTransitionPercent > healthPercent)
        {
            CM.enemyAnimator.SetBool(phaseName, true);
            CM.enemyAnimator.SetTrigger(phaseName + "Trigger");
            isPhase2 = true;
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
    [Header("Triple Projectile Attack")]
    [SerializeField] private ProjectileType chosenProj;
    [SerializeField] private Transform[] tripProjLocation = new Transform[3];
    [SerializeField] private ParticleSystem[] tripProjEffect = new ParticleSystem[3];

    public void GenericStartTripProj(int index)
    {
        tripProjEffect[index].Play();
    }


    public void GenericActivateTripProjAttack(int index, float accuracy)
    {
        Transform thisSpawn = tripProjLocation[index];

        Vector3 targetLocal = GameManager._.Master.transform.position + Vector3.up * (GameManager._.Master.movementMaster.height / 4);
        Vector3 targetVel = GameManager._.Master.movementMaster.rb.velocity;
        var targetLocation = MurderBag.RoughPredictLocation(
            targetLocal,
            targetVel,
            tripProjLocation[index].position
            , chosenProj.speed, accuracy);
        tripProjEffect[index].Clear();
        FireProjectile("LG_Proj", targetLocation, thisSpawn.position);

    }



    #region Fire Right
    public void LG_StartTripProjR()
    {
        GenericStartTripProj(0);
        if (isPhase2) GenericStartTripProj(3);
    }

    public void LG_FireTripProjR()
    {
        GenericActivateTripProjAttack(0, 0f);
        if (isPhase2) GenericActivateTripProjAttack(3, 0.25f);
    }
    #endregion

    #region Fire Center
    public void LG_StartTripProjC()
    {
        GenericStartTripProj(1);
        if (isPhase2) GenericStartTripProj(4);
    }

    public void LG_FireTripProjC()
    {
        GenericActivateTripProjAttack(1, 1f);
        if (isPhase2) GenericActivateTripProjAttack(4, 0.85f);
    }
    #endregion

    #region Fire Left
    public void LG_StartTripProjL()
    {
        GenericStartTripProj(2);
        if (isPhase2) GenericStartTripProj(5);
    }

    public void LG_FireTripProjL()
    {
        GenericActivateTripProjAttack(2, 0.5f);
        if (isPhase2) GenericActivateTripProjAttack(5, 0.75f);
    }
    #endregion


    #endregion

    #region Ground Pound
    [Header("Ground Pound")]
    [SerializeField] private ParticleSystem groundPoundParticles;
    [SerializeField] private int groundPoundShotNo;
    [SerializeField] private Vector2 groundPoundXVariation;
    [SerializeField] private Vector2 groundPoundZVariation;
    [SerializeField] private Vector2 groundPoundYVariation;
    private GameObject[] groundPoundShotSpawn;
    [SerializeField] private float groundPoundInaccuracyAmount;
    public void LG_GroundPound_ON()
    {
        var hitbox = GenericAttack_ON(4, 7);
        var damagePair = damageValues.damageArray[7];
        hitbox.AssignValues(damagePair, Vector3.up);
        groundPoundParticles.Play();

        LG_GroundPoundPrimePhase2();

    }

    public void LG_GroundPound_OFF()
    {
        GenericAttack_OFF(4);
    }

    #region Phase 2 Adjustments
    private void LG_GroundPoundPrimePhase2()
    {
        if (!isPhase2) return;
        Vector3 rootPos = CM.enemyAnimator.rootPosition;
        groundPoundShotSpawn = new GameObject[groundPoundShotNo];
        for (int i = 0; i < groundPoundShotSpawn.Length; i++)
        {
            float xOffset = Random.Range(groundPoundXVariation[0], groundPoundXVariation[1]);
            float zOffset = Random.Range(groundPoundZVariation[0], groundPoundZVariation[1]);
            float yOffset = Random.Range(groundPoundYVariation[0], groundPoundYVariation[1]);
            var position = rootPos + new Vector3(xOffset, yOffset, zOffset);
            groundPoundShotSpawn[i] = ObjectPooler._.SpawnFromPool("RedProjParticles", position, Quaternion.identity);
            groundPoundShotSpawn[i].GetComponent<ParticleSystem>().Play();

        }
        //theres not enough time in the animation to do another event
        Invoke("LG_GroundPoundFirePhase2", 1f);
    }

    public void LG_GroundPoundFirePhase2()
    {
        Vector3[] targetLocation = new Vector3[groundPoundShotNo];
        //1 perfectly accurate rest randomish

        Vector3 trueTarget = GameManager._.Master.transform.position + Vector3.up * (GameManager._.Master.movementMaster.height / 4);
        for (int i = 0; i < targetLocation.Length - 1; i++)
        {
            float lerpAmount = Random.value;
            Vector3 randomAmount = new Vector3(
                Random.Range(-groundPoundInaccuracyAmount, groundPoundInaccuracyAmount),
                Random.Range(0, groundPoundInaccuracyAmount / 2),
                Random.Range(-groundPoundInaccuracyAmount, groundPoundInaccuracyAmount));
            Vector3 target = trueTarget + randomAmount;
            Vector3 predictedTarget = MurderBag.RoughPredictLocation(target, GameManager._.Master.movementMaster.rb.velocity, groundPoundShotSpawn[i].transform.position, chosenProj.speed, lerpAmount);
            FireProjectile("LG_Proj", predictedTarget, groundPoundShotSpawn[i].transform.position);
            groundPoundShotSpawn[i].GetComponent<ParticleSystem>().Clear();
        }
        Vector3 perfectShot = MurderBag.RoughPredictLocation(trueTarget, GameManager._.Master.movementMaster.rb.velocity, groundPoundShotSpawn[groundPoundShotSpawn.Length - 1].transform.position, chosenProj.speed, 1);
        FireProjectile("LG_Proj", perfectShot, groundPoundShotSpawn[groundPoundShotSpawn.Length - 1].transform.position);
        groundPoundShotSpawn[groundPoundShotSpawn.Length - 1].GetComponent<ParticleSystem>().Clear();
    }
    #endregion
    #endregion

    #region Phase 2 Transition
    [Header("Phase 2 Transition")]
    [SerializeField] private ParticleSystem P2StartParticles;
    [SerializeField] private GameObject P2FalseOrb;
    [SerializeField] private float P2StartProjMaxSize;
    [SerializeField] private float P2StartProjSize;
    [SerializeField] private bool P2StartProjCharging = false;
    [SerializeField] private float P2StartProjRate = 1.5f;
    public void LG_Phase2FalseOrb_ON()
    {
        P2FalseOrb.transform.localScale = Vector3.zero;
        P2StartParticles.Play();
        P2FalseOrb.SetActive(true);
        P2StartProjCharging = true;
    }

    public void LG_Phase2LerpFalse()
    {
        if (!P2StartProjCharging) return;
        P2StartProjSize = Mathf.Lerp(P2StartProjSize, P2StartProjMaxSize, P2StartProjRate * Time.deltaTime);
        P2FalseOrb.transform.localScale = new Vector3(P2StartProjSize, P2StartProjSize, P2StartProjSize);
    }

    [SerializeField] private GameObject P2TrueOrb;
    [SerializeField] private GameObject P2Eyes;
    [SerializeField] private ParticleSystem[] P2Particles;
    public void LP_Phase2SwapOrb()
    {
        P2FalseOrb.SetActive(false);
        P2TrueOrb.SetActive(true);
        P2Eyes.SetActive(true);
        foreach (ParticleSystem particle in P2Particles)
        {
            particle.Play();
        }
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




    #region Death
    [Header("Death Variables")]
    [SerializeField] private ParticleSystem kneeParticles;
    [SerializeField] private ParticleSystem lyingDownParticles;
    [SerializeField] private ParticleSystem descentParticles;
    public void LG_DeathGeneralDisable()
    {
        foreach (ParticleSystem particle in P2Particles)
        {
            particle.Stop();
        }
        P2StartParticles.Stop();
        P2TrueOrb.SetActive(false);
        P2Eyes.SetActive(false);

    }
    public void LG_DeathKneeDown()
    {
        kneeParticles.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
        kneeParticles.Play();
    }
    public void LG_DeathLyingDown()
    {
        lyingDownParticles.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        lyingDownParticles.Play();
    }

    public void LG_DeathDescent()
    {
        descentParticles.Play();
        Destroy(this.gameObject, 5);
    }
    #endregion
}
