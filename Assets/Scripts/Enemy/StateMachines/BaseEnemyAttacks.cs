using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

[System.Serializable]
public struct DamagePairs
{
    public string name;
    public int damage;
    public int magnitude;
    public DamagePairs(string Tname,int Tdamage, int Tmagnitude)
    {
        name = Tname;
        damage = Tdamage;
        magnitude = Tmagnitude;
    }
}

public class BaseEnemyAttacks : MonoBehaviour
{
    public EnemyDamage[] Hitboxes;

    public bool canHit = true;

    //public void SetHitbox(AnimationEvent eventer)
    //{
    //    bool theBool = eventer.intParameter != 0;
    //    int index = (int)eventer.floatParameter;
    //    if (Hitboxes[index] == null)
    //    {
    //        Debug.LogError("HitboxIndex null");
    //        return;
    //    }
    //    Hitboxes[index].hitbox.enabled = theBool;
    //    canHit = theBool;


    //}

    public void DamagePlayer(int damage,Vector3 direction,float magnitude,EnemyComponentMaster CM = null)
    {
        if (!canHit) return;
        GameManager._.Master.healthMaster.takeDamage(damage, direction, magnitude,CM);
        canHit = false;
        Debug.Log("Dealt: " + damage + " to the player");
    }


    protected void FireProjectile(string tag, Vector3 target, Vector3 source)
    {
        Vector3 dir = target - source;
        Quaternion aimDirection = Quaternion.LookRotation(dir, Vector3.up);

        GameObject spawnedProjectile = ObjectPooler._.SpawnFromPool(tag, source, aimDirection);
        spawnedProjectile.GetComponent<ProjectileManager>().Initialise(source, aimDirection);
    }





}
