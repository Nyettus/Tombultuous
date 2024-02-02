using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPassiveEffectHandler : MonoBehaviour
{
    public ItemMaster master;


    #region LightningRod
    public LightningRod LRodCard;
    public float LRodDamage = 0;
    public void LRodHit(Vector3 location)
    {
        LRodExplosion(location);
    }

    private void LRodExplosion(Vector3 location)
    {
        if (LRodDamage == 0) return;
        float explosion = LRodDamage * LRodCard.explosionDamage;
        if (explosion > LRodCard.threshold)
        {
            Debug.LogWarning("Explostion went off");
            Collider[] colliderArray = Physics.OverlapSphere(location, LRodCard.radius);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out EnemyHealth health))
                {
                    health.takeDamage(LRodCard.explosionDamage*master.M_DamageMult);
                }
            }



        }
        Debug.Log("LRod Damage reset at: " + LRodDamage);
        LRodDamage = 0;
    }

    private void LRodCalculate()
    {
        int LRodCount = master.GetItemCount(LRodCard);
        if (LRodCount == 0) return;
        if (!GameManager._.Master.grounded)
        {
            LRodDamage += Time.fixedDeltaTime * LRodCard.rate * LRodCount;
        }
        else
        {
            LRodExplosion(transform.position);
        }


    }

    #endregion


    // Update is called once per frame
    void FixedUpdate()
    {
        LRodCalculate();
    }
}
