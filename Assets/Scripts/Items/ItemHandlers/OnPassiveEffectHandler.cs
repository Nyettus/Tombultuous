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
        Debug.LogWarning("Explostion went off");
        Collider[] colliderArray = Physics.OverlapSphere(location, LRodCard.radius * LRodDamage);
        LRDebug(location);
        
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out EnemyHealth health))
            {
                UIManager._.WriteToNotification("Explosion hit", 5);
                health.takeDamage(LRodCard.explosionDamage * master.M_DamageMult);
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
