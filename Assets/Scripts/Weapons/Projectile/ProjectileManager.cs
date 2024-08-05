using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public ProjectileType card;
    private Rigidbody RB;
    [SerializeField] private Collider bounceCollider;
    private List<EnemyHealth> enemiesHit = new List<EnemyHealth>();
    public EnemyComponentMaster ownerCM;

    [SerializeField] private bool isAlly;
    [SerializeField] private bool hasHit;
    private int pierceCount;
    private int bounceCount;

    void OnEnable()
    {
        RB = GetComponent<Rigidbody>();
        enemiesHit.Clear();
    }


    public void Initialise(Vector3 position, Quaternion rotation, EnemyComponentMaster CM = null)
    {
        ownerCM = CM;
        RB.velocity = Vector3.zero;
        pierceCount = card.pierceCount;
        bounceCount = card.bounceCount;
        isAlly = card.ally;
        RB.position = position;
        RB.rotation = rotation;
        RB.useGravity = card.useGravity;
        Vector3 velocity = transform.forward * card.speed;
        RB.AddForce(velocity, ForceMode.Impulse);
        UpdateLayer();

    }
    private void Update()
    {
        RotateToVelocity();
    }

    private void RotateToVelocity()
    {
        if (RB.velocity != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(RB.velocity, Vector3.up);
            RB.rotation = rotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 2)
        {
            OnTargetHit(other);
            OnGroundHit(other);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (bounceCollider != null) bounceCollider.enabled = false;
    }

    private void OnGroundHit(Collider other)
    {
        if (!(other.TryGetComponent(out EnemyHealth enemyScript) || other.TryGetComponent(out PlayerHealth playerScript) || other.TryGetComponent(out MeleeHitboxHandling meleescript)))
        {
            if (other.gameObject.layer == 3)
            {

                bounceCount--;

            }
            if (bounceCount <= 0)
            {
                Debug.Log("Bounce disable");
                DisableEffect();
            }
            else
            {
                if (bounceCollider != null) bounceCollider.enabled = true;
            }
        }


    }
    private void OnTargetHit(Collider other)
    {
        if (other.TryGetComponent(out IEnemyDamageable hitboxHealth) && isAlly)
        {
            if (!enemiesHit.Contains(hitboxHealth.GetEnemyHealthScript()))
            {
                card.ProjDamage(hitboxHealth);
                enemiesHit.Add(hitboxHealth.GetEnemyHealthScript());
            }


            GameManager._.Master.itemMaster.onHitEffectHandler.OnHitEffect(transform.position);
            hasHit = true;
        }


        if (other.TryGetComponent(out PlayerHealth playerScript) && !isAlly)
        {
            card.ProjDamage(this.transform, playerScript,ownerCM);
            hasHit = true;
        }
        if (hasHit)
        {
            if (pierceCount <= 0)
            {
                Debug.Log("Pierce disable");
                DisableEffect();
            }
            pierceCount--;

        }

    }

    public void MeleeDeflection()
    {
        if (isAlly) return;
        isAlly = true;
        RB.velocity = -RB.velocity;
        hasHit = true;
        UpdateLayer();

    }
    protected virtual void DisableEffect()
    {
        if (!hasHit && isAlly)
        {
            GameManager._.Master.itemMaster.onMissEffectHandler.OnMissEffect(transform.position);
        }
        hasHit = false;
        gameObject.SetActive(false);
    }

    private void UpdateLayer()
    {
        if (isAlly)
        {
            RB.excludeLayers = 6;
        }
        else
        {
            RB.excludeLayers = 13;
        }

    }

    private void OnDisable()
    {
        RB.velocity = Vector3.zero;
    }

}
