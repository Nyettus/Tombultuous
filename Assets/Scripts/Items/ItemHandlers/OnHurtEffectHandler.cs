using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHurtEffectHandler : MonoBehaviour
{
    public ItemMaster itemMaster;
    private void FixedUpdate()
    {
        HandleLaud();
    }

    #region Laudanum
    public Laudanum laudCard;
    private bool laudEnabled = false;
    private float laudEndTime => laudCard.laudTimeIncrease;
    private float laudTimer;

    public float LaudAdd()
    {
        var timeEval = laudCard.laudSpeedControl.Evaluate(laudTimer / laudEndTime);
        int laudCount = itemMaster.GetItemCount(laudCard);
        return timeEval * laudCount * laudCard.laudSpeedIncrease;
    }
    private void HandleLaud()
    {
        if (laudEnabled)
        {
            if (laudTimer <= laudEndTime)
            {
                laudTimer += Time.deltaTime;
            }
            else
            {
                laudEnabled = false;
            }
        }
    }
    private void EnableLaudunum()
    {
        int laudCount = itemMaster.GetItemCount(laudCard);
        if (laudCount == 0) return;
        laudTimer = 0;
        laudEnabled = true;

    }

    #endregion

    #region Steel Feather
    public SteelFeather steelFeatherCard;
    private void HandleSteelFeather(int damage,EnemyComponentMaster attacker)
    {
        int steelFeatherCount = itemMaster.GetItemCount(steelFeatherCard);
        if (steelFeatherCount == 0 || attacker==null) return;
        float reflectedDamage = damage * steelFeatherCard.reflectionPercent * steelFeatherCount * itemMaster.Master.weaponMaster.damageMult;
        attacker.enemyHealth.TakeDamage(reflectedDamage);
    }

    #endregion

    #region Amoral Compass
    public AmoralCompass amoralCard;
    public GameObject amoralExplosion;
    private void HandleAmoral(int damage)
    {
        int amoralCount = itemMaster.GetItemCount(amoralCard);
        if (amoralCount == 0) return;
        float damageMultiplier = (amoralCard.baseDamage + amoralCard.damageIncrement * (amoralCount - 1))*GameManager._.Master.itemMaster.M_DamageMult;
        float explosionDamage = damage * damageMultiplier; 
        Collider[] colliderArray = Physics.OverlapSphere(GameManager._.Master.transform.position, amoralCard.radius);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out EnemyHealth health))
            {
                health.TakeDamage(explosionDamage);
                Debug.Log("Dealt amoral damage");
            }
        }
    }

    #endregion
    public void OnHurtEffect(int damageTaken,EnemyComponentMaster CM = null)
    {
        EnableLaudunum();
        HandleSteelFeather(damageTaken, CM);
        HandleAmoral(damageTaken);
    }
}
