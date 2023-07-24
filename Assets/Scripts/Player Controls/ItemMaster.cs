using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
    public PlayerMaster Master;
    
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

}
