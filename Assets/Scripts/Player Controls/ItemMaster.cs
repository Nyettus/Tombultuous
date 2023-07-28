using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
    public PlayerMaster Master;
    public List<ItemList> itemList = new List<ItemList>();
    
    public int M_Health;
    public int MIN_Health = 1;
    public int M_OverHealth;
    public int M_DecayHealth;
    public float M_DamageMult;
    public float MIN_DamageMult = 0.1f;
    public float M_Haste;
    public float MIN_Haste;
    public int M_Pockets;
    public int MIN_Pockets = 1;
    public float M_MoveSpeed;
    public float MIN_MoveSpeed = 0.1f;
    public float M_AirAcceleration;
    public float MIN_AirAcceleration = 0.1f;
    public float M_JumpPower;
    public float MIN_JumpPower = 0.1f;
    public int M_JumpCount;
    public int MIN_JumpCount = 1;
    public float M_DashCooldown;
    public float MIN_DashCooldown = 0.2f;
    public float M_DashSpeed;
    public float M_GoldRetention;
    public float MIN_GoldRetention = 0f;
    public float M_GoldMultiplier;
    public float MIN_GoldMultiplier = 0f;


    public void ResetStats()
    {
        M_Health = 0;           //1
        M_DamageMult = 0;       //2
        M_Haste = 0;            //3
        M_Pockets = 0;          //4
        M_MoveSpeed = 0;        //5
        M_AirAcceleration = 0;  //6
        M_JumpPower = 0;        //7
        M_JumpCount = 0;        //8
        M_DashCooldown = 0;     //9
        M_DashSpeed = 0;        //10
        M_GoldRetention = 0;    //11
        M_GoldMultiplier = 0;   //12
    }

    public void RefreshEffects()
    {
        ResetStats();
        foreach(ItemList i in itemList)
        {
            i.item.PermanantBuff(this, Master, i.stacks);
        }

    }


}
