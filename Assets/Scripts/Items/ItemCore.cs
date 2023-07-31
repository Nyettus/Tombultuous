using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCore : MonoBehaviour
{
    [SerializeField]
    private BasicStatChange card;

    //  0 : permanent buff
    //  1 : once off buff
    //  2 : on hit effect
    //  3 : on kill effect
    //  4 : in player hurt effect
    public int[] itemType;
    // Start is called before the first frame update

    public virtual string GiveName()
    {
        return card.itemName;
    }
    public virtual void PickupItem(ItemMaster player)
    {
        foreach(ItemList i in player.itemList)
        {
            if(i.name == GiveName())
            {
                Debug.Log("I ADDED A STACK");
                i.stacks++;
                player.RefreshEffects();
                return;
            }

        }
        Debug.Log("I ADDED AN ITEM");
        player.itemList.Add(new ItemList(this, GiveName()));
        player.RefreshEffects();
    }

    public bool HasAssociatedBuff(int buffType)
    {
        if (itemType.Length == 1)
        {
            if (itemType[0] == buffType)
                return true;
            else
                return false;
        }
        else if (itemType.Length > 1)
        {
            if (itemType[0] == buffType)
                return true;
            else
                return false;
        }
        else
        {
            Debug.LogError("Buff Array Empty");
            return false;
        }

    }
    public virtual void PermanentBuff(ItemMaster master, PlayerMaster origin, int stacks)
    {
        int[] reference = card.statRef;
        float[] value = card.statChange;
        if(reference != null)
        {
            
        for(int i = 0; i<reference.Length; i++)
            {
                int number = reference[i];
                switch (number)
                {
                    case 0:
                        master.M_Health += (int)value[i] * stacks;
                        break;
                    case 1:
                        master.M_DamageMult += (origin.damage * value[i]) * stacks;
                        break;

                    case 2:
                        Debug.Log("Stat not implemented");
                        break;

                    case 3:
                        master.M_Pockets += (int)value[i] * stacks;
                        break;

                    case 4:
                        master.M_MoveSpeed += (origin.moveSpeed * value[i]) * stacks;
                        break;

                    case 5:
                        master.M_AirAcceleration += (origin.airAccel * value[i]) * stacks;
                        break;

                    case 6:
                        master.M_JumpPower += (origin.jumpPower * value[i]) * stacks;
                        break;

                    case 7:
                        master.M_JumpCount += (int)value[i] * stacks;
                        break;

                    case 8:
                        master.M_DashCooldown += origin.dashCooldown - (origin.dashCooldown * (1 - value[i] * stacks));
                        break;

                    case 9:
                        master.M_DashSpeed += (origin.dashSpeed * value[i]) * stacks;
                        break;

                    case 10:
                        master.M_GoldRetention += value[i] * stacks;
                        break;

                    case 11:
                        master.M_GoldMultiplier += value[i] * stacks;
                        break;

                    default:
                        Debug.LogError("Stat reference out of bounds");
                        break;

                }
            }
        }
    }

    public virtual void OnceBuff()
    {

    }

    public virtual void OnHitEffect()
    {

    }

    public virtual void OnKillEffect()
    {

    }

    public virtual void OnHurtEffect()
    {

    }

    public virtual void OnRoomClear()
    {

    }



}
