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
        Debug.Log(string.Format("{0} gold gained with {1} multipler", (int)(amount * goldMultiplier),goldMultiplier));
        gold += Mathf.RoundToInt(amount * goldMultiplier);
    }

    public int FinalGold()
    {
        int final = Mathf.RoundToInt(gold * goldRetention);
        gold = 0;
        return final;
    }

}
