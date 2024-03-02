using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    ItemMaster quick => GameManager._.Master.itemMaster;
    public int gold;
    public float goldMultiplier => 1 + Mathf.Clamp(quick.M_GoldMultiplier, quick.MIN_GoldMultiplier, float.MaxValue);
    public float goldRetention => Mathf.Clamp(quick.M_GoldRetention, quick.MIN_GoldRetention, float.MaxValue);


    public delegate void UpdateGold();
    public static event UpdateGold OnUpdateGold;

    public void OnGoldChangeEvent()
    {

        if (OnUpdateGold != null)
            OnUpdateGold();
    }

    
    public void Start()
    {
        GameManager._.goldManager = this;
        OnGoldChangeEvent();
    }

    public void GetGold(int amount)
    {
        Debug.Log(string.Format("{0} gold gained with {1} multipler", (int)(amount * goldMultiplier), goldMultiplier));
        gold += Mathf.RoundToInt(amount * goldMultiplier);
        OnGoldChangeEvent();
    }

    public int FinalGold(bool win, bool reset = true)
    {
        int final = 0;
        if (win)
        {
            final = gold + Mathf.RoundToInt(gold * goldRetention);
        }
        else
        {
            final = Mathf.RoundToInt(gold * goldRetention);

        }
        if(reset)gold = 0;
        return final;
    }

}
