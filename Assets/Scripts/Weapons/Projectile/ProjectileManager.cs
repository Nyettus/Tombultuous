using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public ProjectileType card;

    void OnEnable()
    {
        
    }


    public void Initialise(Vector3 position, Quaternion direction)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.tag != "Player" || other.tag != "Enemy")) gameObject.SetActive(false);


        if(other.TryGetComponent(out EnemyHealth enemyScript))
        {
            card.ProjDamage(enemyScript);
        }
        if(other.TryGetComponent(out PlayerHealth playerScript))
        {
            card.ProjDamage(this.transform, playerScript);
        }
        if (!card.piercing) gameObject.SetActive(false);



    }

}
