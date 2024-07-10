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
    private void HandleSteelFeather(int damage, EnemyComponentMaster attacker)
    {
        int steelFeatherCount = itemMaster.GetItemCount(steelFeatherCard);
        if (steelFeatherCount == 0 || attacker == null) return;
        float reflectedMultipliers = steelFeatherCard.reflectionPercent * steelFeatherCount * itemMaster.Master.weaponMaster.damageMult;
        var dmg = new DamageInstance(damage) { multipliers = reflectedMultipliers, damageType = DamageType.Item };
        attacker.enemyHealth.TakeDamage(dmg);
    }

    #endregion

    #region Amoral Compass
    public AmoralCompass amoralCard;
    private void HandleAmoral(int damage)
    {
        int amoralCount = itemMaster.GetItemCount(amoralCard);
        if (amoralCount == 0) return;
        float damageMultiplier = (amoralCard.baseDamage + amoralCard.damageIncrement * (amoralCount - 1))*GameManager._.Master.weaponMaster.damageMult;
        float explosionDamage = damage * damageMultiplier; 
        Collider[] colliderArray = Physics.OverlapSphere(GameManager._.Master.transform.position, amoralCard.radius);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out EnemyHealth health))
            {
                health.TakeDamage(explosionDamage);
            }
        }
    }

    #endregion
    #region Moral Compass
    public MoralCompass moralCard;
    private void HandleMoral(int damage)
    {
        int moralCount = itemMaster.GetItemCount(moralCard);
        if (moralCount == 0) return;
        float randomChance = Random.value;
        if (randomChance > moralCard.procChance) return;
        float healPercent = moralCard.healPercent + moralCard.incrementIncrease * (1 - moralCount);
        int healAmount = Mathf.RoundToInt(damage * healPercent);
        GameManager._.Master.healthMaster.HealFlesh(healAmount);
        Debug.Log("Healed: " + healAmount);
    }

    #endregion

    public void OnHurtEffect(int damageTaken,EnemyComponentMaster CM = null)
    {
        EnableLaudunum();
        HandleSteelFeather(damageTaken, CM);
        HandleAmoral(damageTaken);
        HandleMoral(damageTaken);
    }
}
