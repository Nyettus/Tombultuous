using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyComponentMaster : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public NavMeshAgent enemyNavMesh;
    public Animator enemyAnimator;
    public BaseEnemyAttacks enemyAttacks;
    public Rigidbody enemyRB;
    public Collider enemyCollider;

    [SerializeField]
    private GameObject model;
    [SerializeField]
    private Collider[] ragdollCollider;
    [SerializeField]
    private Rigidbody[] ragdollRB;

    public float defaultWalkSpeed = -1;
    private Vector3 navmeshVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        if (TryGetComponent<EnemyHealth>(out EnemyHealth healthCompono)) enemyHealth = healthCompono;
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent navCompono)) enemyNavMesh = navCompono;
        if (TryGetComponent<Animator>(out Animator animCompono)) enemyAnimator = animCompono;
        if (TryGetComponent<BaseEnemyAttacks>(out BaseEnemyAttacks damCompono)) enemyAttacks = damCompono;
        if (TryGetComponent<Rigidbody>(out Rigidbody RBCompono)) enemyRB = RBCompono;
        if (TryGetComponent<Collider>(out Collider ColCompono)) enemyCollider = ColCompono;
        if (model != null)
        {
            ragdollRB = model.GetComponentsInChildren<Rigidbody>();
            ragdollCollider = model.GetComponentsInChildren<Collider>();
            ActivateRagdoll(false);
        }


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
    }


    private void LerpingBlendTree(float rate)
    {
        var holding = enemyNavMesh.speed;
        if (holding == 0) holding = 1;
        var adjustedVel = transform.InverseTransformDirection(enemyNavMesh.desiredVelocity) / holding;
        navmeshVelocity = Vector3.Lerp(navmeshVelocity, adjustedVel, rate * Time.deltaTime);

        enemyAnimator.SetFloat("VelocityX", navmeshVelocity.x);
        enemyAnimator.SetFloat("VelocityZ", navmeshVelocity.z);
    }

    public void ActivateRagdoll(bool state)
    {
        if (model == null) return;
        foreach (var collider in ragdollCollider)
        {
            if (collider.tag == "WeaponHitbox") continue;
            collider.enabled = state;
            collider.excludeLayers = 1 << 6;
        }
        foreach (var rb in ragdollRB)
        {
            rb.detectCollisions = state;
            rb.isKinematic = !state;
        }


        enemyAnimator.enabled = !state;
        enemyRB.detectCollisions = !state;
        enemyCollider.enabled = !state;

    }


}
