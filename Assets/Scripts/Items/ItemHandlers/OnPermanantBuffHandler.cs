using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    OverHealth,
    DecayHealth,
    DamageMult,
    Haste,
    Pockets,
    MovementSpeed,
    AirAcceleration,
    JumpPower,
    JumpCount,
    DashCooldown,
    DashSpeed,
    GoldRetention,
    GoldMultiplier
}

public class OnPermanantBuffHandler : MonoBehaviour
{
    public ItemMaster itemMaster;
    public PlayerMaster origin;

    public void PermanentBuff(int stacks, PermanentBuffItem card)
    {
        StatBuff[] statBuffs = card.buffs;


        for (int i = 0; i < statBuffs.Length; i++)
        {
            StatBuff stat = statBuffs[i];
            switch (stat.type)
            {
                case StatType.Health:
                    itemMaster.M_Health += (int)stat.change * stacks;
                    break;
                
                case StatType.DamageMult:
                    itemMaster.Perm_DamageMult += (origin.damage * stat.change) * stacks;
                    break;

                case StatType.Haste:
                    itemMaster.Perm_Haste += (origin.haste * stat.change) * stacks;
                    break;

                case StatType.Pockets:
                    itemMaster.M_Pockets += (int)stat.change * stacks;
                    GameManager._.Master.weaponMaster.RefreshPockets();
                    break;

                case StatType.MovementSpeed:
                    itemMaster.Perm_MoveSpeed += (origin.moveSpeed * stat.change) * stacks;
                    break;

                case StatType.AirAcceleration:
                    itemMaster.M_AirAcceleration += (origin.airAccel * stat.change) * stacks;
                    break;

                case StatType.JumpCount:
                    itemMaster.M_JumpPower += (origin.jumpPower * stat.change) * stacks;
                    break;

                case StatType.JumpPower:
                    itemMaster.M_JumpCount += (int)stat.change * stacks;
                    break;

                case StatType.DashCooldown:
                    itemMaster.M_DashCooldown += origin.dashCooldown - (origin.dashCooldown * (1 - stat.change * stacks));
                    break;

                case StatType.DashSpeed:
                    itemMaster.M_DashSpeed += (origin.dashSpeed * stat.change) * stacks;
                    break;

                case StatType.GoldRetention:
                    itemMaster.M_GoldRetention += stat.change * stacks;
                    break;

                case StatType.GoldMultiplier:
                    itemMaster.M_GoldMultiplier += stat.change * stacks;
                    break;

                default:
                    Debug.LogError("Stat reference out of bounds");
                    break;

            }
        }
    }

   
}
