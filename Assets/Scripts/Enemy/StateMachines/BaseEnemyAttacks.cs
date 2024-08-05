using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

[System.Serializable]
public struct DamagePairs
{
    public string name;
    public int damage;
    public float magnitude;
    public DamagePairs(string Tname,int Tdamage, float Tmagnitude)
    {
        name = Tname;
        damage = Tdamage;
        magnitude = Tmagnitude;
    }
}

public class BaseEnemyAttacks : MonoBehaviour
{
    public EnemyComponentMaster CM;
    public EnemyDamage[] Hitboxes;
    public string hasHitName;
    public EnemyDamageNumbers damageValues;


    public virtual void CheckHealthPercent()
    {

    }

    public void DamagePlayer(int damage,Vector3 direction,float magnitude,EnemyComponentMaster CM = null)
    {
        GameManager._.Master.healthMaster.takeDamage(damage, direction, magnitude,CM);
        Debug.Log("Dealt: " + damage + " to the player");
        if (hasHitName == "" || CM == null) return;
        CM.enemyAnimator.SetTrigger(hasHitName);
        
    }


    protected void FireProjectile(string tag, Vector3 target, Vector3 source)
    {
        Vector3 dir = target - source;
        Quaternion aimDirection = Quaternion.LookRotation(dir, Vector3.up);

        GameObject spawnedProjectile = ObjectPooler._.SpawnFromPool(tag, source, aimDirection);
        spawnedProjectile.GetComponent<ProjectileManager>().Initialise(source, aimDirection,CM);
    }


    protected EnemyDamage GenericAttack_ON(int hitboxIndex, int damageIndex)
    {
        EnemyDamage hitbox = Hitboxes[hitboxIndex];
        hitbox.AssignValues(damageValues.damageArray[damageIndex]);
        hitbox.canHit = true;
        hitbox.hitbox.enabled = true;
        return hitbox;
    }

    protected EnemyDamage GenericAttack_OFF(int hitboxIndex)
    {
        EnemyDamage hitbox = Hitboxes[hitboxIndex];
        hitbox.hitbox.enabled = false;
        return hitbox;
    }


}
