using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct NavMeshAgentValues
{
    public float angularSpeed;
    public float acceleration;
    public NavMeshAgentValues(float aSpeed, float accel)
    {
        angularSpeed = aSpeed;
        acceleration = accel;
    }
}

public class EnemyComponentMaster : MonoBehaviour
{
    public EnemyBaseStats card;

    public EnemyHealth enemyHealth;
    public NavMeshAgent enemyNavMesh;
    public Animator enemyAnimator;
    public BaseEnemyAttacks enemyAttacks;
    public Rigidbody enemyRB;
    public Collider enemyCollider;
    public BossHandler enemyBoss;

    [SerializeField]
    private GameObject model;
    [SerializeField]
    private Collider[] ragdollCollider;
    [SerializeField]
    private Rigidbody[] ragdollRB;

    public float defaultWalkSpeed = -1;
    private Vector3 navmeshVelocity = Vector3.zero;
    private NavMeshAgentValues storedValues;

    [Header("Airborne")]
    public bool canBeAirborne = false;
    private float airborneLerp = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (TryGetComponent<EnemyHealth>(out EnemyHealth healthCompono))
        {
            enemyHealth = healthCompono;
            enemyHealth.health = card.health;
        }
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent navCompono))
        {
            enemyNavMesh = navCompono;
            enemyNavMesh.speed = card.moveSpeed;
        }
        if (TryGetComponent<Animator>(out Animator animCompono))
        {
            enemyAnimator = animCompono;
            enemyAnimator.applyRootMotion = false;
        }
        if (TryGetComponent<BaseEnemyAttacks>(out BaseEnemyAttacks damCompono)) enemyAttacks = damCompono;
        if (TryGetComponent<Rigidbody>(out Rigidbody RBCompono)) enemyRB = RBCompono;
        if (TryGetComponent<Collider>(out Collider ColCompono)) enemyCollider = ColCompono;
        if (TryGetComponent<BossHandler>(out BossHandler BossCompono))
        {
            BossCompono.master = this;
            enemyBoss = BossCompono;
        }
        if (model != null)
        {
            ragdollRB = model.GetComponentsInChildren<Rigidbody>();
            ragdollCollider = model.GetComponentsInChildren<Collider>();
            ActivateRagdoll(false);
        }


    }

    private void OnAnimatorMove()
    {
        if (enemyAnimator.applyRootMotion)
        {
            Debug.Log("Is using special");
            enemyNavMesh.updatePosition = false;
            Vector3 rootPos = enemyAnimator.rootPosition;
            rootPos.y = enemyNavMesh.nextPosition.y;
            transform.position = rootPos;
            enemyNavMesh.nextPosition = rootPos;
        }
        else
        {
            enemyNavMesh.updatePosition = true;
        }


    }


    public void SetRootMotion(bool state)
    {
        if (storedValues.acceleration == 0 || storedValues.angularSpeed ==0)
        {
            storedValues = new NavMeshAgentValues(enemyNavMesh.angularSpeed, enemyNavMesh.acceleration);
        }
        if (state)
        {

            enemyNavMesh.angularSpeed = 0;
            enemyNavMesh.acceleration = 0;
        }
        else
        {
            enemyNavMesh.angularSpeed = storedValues.angularSpeed;
            enemyNavMesh.acceleration = storedValues.acceleration;
        }
        enemyAnimator.applyRootMotion = state;

    }

    public void SetAnimBool(string name, bool set)
    {
        enemyAnimator.SetBool(name, set);
    }

    public void SetAnimFloat(string name, float value = -1)
    {
        if (value == -1)
        {
            enemyAnimator.SetFloat(name, Random.Range(0f, 1f));
        }
        else
        {
            enemyAnimator.SetFloat(name, value);
        }
    }




    public void FallOver(Vector3 direction)
    {
        enemyRB.isKinematic = false;
        enemyNavMesh.enabled = false;
        enemyRB.AddForce(direction, ForceMode.Impulse);

    }

    public void FixedUpdate()
    {
        LerpingBlendTree(10f);
        if (canBeAirborne) MakeAirborne(10f);
    }


    private void LerpingBlendTree(float rate)
    {
        var holding = defaultWalkSpeed;
        if (holding == 0) holding = 1;

        var adjustedVel = transform.InverseTransformDirection(enemyNavMesh.desiredVelocity) / holding;
        navmeshVelocity = Vector3.Lerp(navmeshVelocity, adjustedVel, rate * Time.deltaTime);

        enemyAnimator.SetFloat("VelocityX", navmeshVelocity.x);
        enemyAnimator.SetFloat("VelocityZ", navmeshVelocity.z);
    }

    private void MakeAirborne(float rate)
    {
        int state = enemyNavMesh.isOnOffMeshLink ? 1 : 0;
        airborneLerp = Mathf.Lerp(airborneLerp, state, rate * Time.deltaTime);
        enemyAnimator.SetLayerWeight(1, airborneLerp);
    }

    public void ActivateRagdoll(bool state)
    {
        if (model == null) return;
        foreach (var collider in ragdollCollider)
        {
            if (collider.tag == "WeaponHitbox") continue;
            collider.enabled = state;
            collider.excludeLayers = 1 << 6 | 1 << 12;
        }
        foreach (var rb in ragdollRB)
        {
            rb.detectCollisions = state;
            rb.isKinematic = !state;
        }


        enemyAnimator.enabled = !state;
        enemyRB.detectCollisions = !state;
        if (enemyCollider != null)
            enemyCollider.enabled = !state;

    }


}
