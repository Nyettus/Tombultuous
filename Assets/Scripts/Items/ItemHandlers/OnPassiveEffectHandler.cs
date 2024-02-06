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
        Collider[] colliderArray = Physics.OverlapSphere(location, LRodCard.radius * LRodDamage);
        LRDebug(location);
        
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out EnemyHealth health))
            {
                health.takeDamage(LRodCard.explosionDamage * master.M_DamageMult);
            }
        }
        LRodDamage = 0;
    }

    private void LRodCalculate()
    {
        int LRodCount = master.GetItemCount(LRodCard);
        if (LRodCount == 0) return;
        float stackedRate = LRodCard.rate + (LRodCount - 1) * 0.5f;
        if (!GameManager._.Master.grounded)
        {
            LRodDamage += Time.fixedDeltaTime * stackedRate;
        }
        else
        {
            LRodExplosion(transform.position-((GameManager._.Master.movementMaster.height/2)*Vector3.up));
            
        }



    }

    [SerializeField]
    private GameObject LRDebugShape;
    private void LRDebug(Vector3 location)
    {
        var item = Instantiate(LRDebugShape, location,Quaternion.identity);
        var size = LRodDamage * LRodCard.radius*2;
        item.transform.localScale = new Vector3(size, size, size);
    }

    #endregion


    // Update is called once per frame
    void FixedUpdate()
    {
        LRodCalculate();
    }
}
