using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    ItemMaster quick => GameManager._.Master.itemMaster;
    public int gold;
    public float goldMultiplier => 1+ Mathf.Clamp(quick.M_GoldMultiplier,quick.MIN_GoldMultiplier,float.MaxValue);
    public float goldRetention => Mathf.Clamp( quick.M_GoldRetention,quick.MIN_GoldRetention,float.MaxValue);

    public void GetGold(int amount)
    {
        gold += (int)(amount * goldMultiplier);
    }

    public int FinalGold()
    {
        int final = (int)(gold * goldRetention);
        gold = 0;
        return final;
    }

}
