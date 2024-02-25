using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public ProjectileType card;
    private Rigidbody RB;
    [SerializeField] private Collider bounceCollider;

    private bool hasHit;
    private int pierceCount;
    private int bounceCount;

    void OnEnable()
    {
        RB = GetComponent<Rigidbody>();
    }


    public void Initialise(Vector3 position, Quaternion rotation)
    {
        RB.velocity = Vector3.zero;
        pierceCount = card.pierceCount;
        bounceCount = card.bounceCount;
        RB.position = position;
        RB.rotation = rotation;
        RB.useGravity = card.useGravity;
        Vector3 velocity = transform.forward * card.speed;
        RB.AddForce(velocity, ForceMode.Impulse);

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
            Debug.Log(other.name);


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
        if (card.ally)
        {
            if (!hasHit)
            {
                GameManager._.Master.itemMaster.onMissEffectHandler.OnMissEffect(transform.position);

            }
            hasHit = false;

        }
        if (!(other.TryGetComponent(out EnemyHealth enemyScript) || other.TryGetComponent(out PlayerHealth playerScript)))
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
        bool hasHit = false;
        if (other.TryGetComponent(out EnemyHealth enemyScript) && card.ally)
        {
            card.ProjDamage(enemyScript);

            GameManager._.Master.itemMaster.onHitEffectHandler.OnHitEffect(transform.position);
            hasHit = true;
        }
        if (other.TryGetComponent(out PlayerHealth playerScript) && !card.ally)
        {
            card.ProjDamage(this.transform, playerScript);
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


    protected virtual void DisableEffect()
    {
        gameObject.SetActive(false);
    }



    private void OnDisable()
    {
        RB.velocity = Vector3.zero;
    }

}
