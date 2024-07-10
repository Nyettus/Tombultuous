using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemMaster : MonoBehaviour
{
    public PlayerMaster Master;

    public Dictionary<ItemBase, ItemStack> itemList => GameManager._.itemList;

    public OnKillItemHandler onKillItemHandler;
    public OnPermanantBuffHandler onPBuffHandler;
    public OnRoomClearHandler onRoomClearHandler;
    public OnHitEffectHandler onHitEffectHandler;
    public OnMissEffectHandler onMissEffectHandler;
    public OnPassiveEffectHandler onPassiveEffectHandler;
    public OnHurtEffectHandler onHurtEffectHandler;



    public int Perm_Health;
    public int M_Health => Perm_Health + onKillItemHandler.copperHeartHealthIncrease;

    public int MIN_Health = 1;
    public int M_OverHealth;
    public int M_DecayHealth;
    public float Perm_DamageMult;
    public float M_DamageMult => Perm_DamageMult + onRoomClearHandler.madnessModifier + onHitEffectHandler.MCoinDamage + onMissEffectHandler.MLamentDamage + onPassiveEffectHandler.LRodDamage+onPassiveEffectHandler.PSDamage;
    public float MIN_DamageMult = 0.1f;
    public float Perm_Haste;
    public float M_Haste => hasteEquation(Perm_Haste);
    public int M_Pockets;
    public int MIN_Pockets = 1;
    public int MAX_Pockets = 10;
    public float Perm_MoveSpeed;
    public float M_MoveSpeed => Perm_MoveSpeed + onKillItemHandler.BigBootsAdd()+onHurtEffectHandler.LaudAdd();

    public float MIN_MoveSpeed = 0.1f;
    public float M_AirAcceleration;
    public float MIN_AirAcceleration = 0.1f;
    public float Perm_JumpPower;
    public float M_JumpPower => Perm_JumpPower;

    public float MIN_JumpPower = 0.1f;
    public int M_JumpCount;
    public int MIN_JumpCount = 1;
    public float Perm_DashCooldown;
    public float M_DashCooldown => Perm_DashCooldown;

    public float MIN_DashCooldown = 0.2f;
    public float M_DashSpeed;
    public float M_GoldRetention;
    public float MIN_GoldRetention = 0f;
    public float M_GoldMultiplier;
    public float MIN_GoldMultiplier = 0f;


    #region Haste Equations
    public float hasteEquation(float addition)
    {
        float a = 5.2f;
        float b = 1f;
        float d = 0.001f;
        float additionCombine = (1 - Master.haste + addition);
        float c = -(Mathf.Log((a/(1-d))-1)-b);
        float output = (a/(1+Mathf.Exp(b*(1+ additionCombine - c))))+d;
        return output;
    }
    #endregion

    /// <summary>
    /// Adds a item to the master with side effects (PBuffItem)
    /// </summary>
    /// <returns>ItemStack with stacks</returns>
    public ItemStack GetItem(ItemBase newItem)
    {
        if (!itemList.TryGetValue(newItem, out var itemStack))
        {
            itemStack = new ItemStack(newItem, 0);
            itemList.Add(newItem, itemStack);
        }

        itemStack.stacks++;

        if (newItem is PermanentBuffItem pBuffItem)
        {
            RefreshEffects();
        }

        return itemStack;
    }

    public int GetItemCount(ItemBase item)
    {
        if (itemList.TryGetValue(item, out var itemStack))
        {
            return itemStack.stacks;
        }
        return 0;
    }


    public void ResetStats()
    {
        Perm_Health = 0;
        Perm_DamageMult = 0;
        Perm_Haste = 0;
        M_Pockets = 0;
        Perm_MoveSpeed = 0;
        M_AirAcceleration = 0;
        Perm_JumpPower = 0;
        M_JumpCount = 0;
        Perm_DashCooldown = 0;
        M_DashSpeed = 0;
        M_GoldRetention = 0;
        M_GoldMultiplier = 0;
    }

    public void CleanseItems()
    {
        GameManager._.itemList.Clear();
    }

    public void RefreshEffects()
    {
        ResetStats();
        var allBuffItems = itemList.Where(il => il.Key is PermanentBuffItem);
        foreach (var i in allBuffItems)
        {
            onPBuffHandler.PermanentBuff(i.Value.stacks, i.Key as PermanentBuffItem);
        }
        GameManager._.Master.healthMaster.OnHealthChangeEvent();
    }

}
