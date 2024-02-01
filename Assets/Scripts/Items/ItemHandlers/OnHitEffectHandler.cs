using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitEffectHandler : MonoBehaviour
{
    public ItemMaster itemMaster;

    #region Marksman's Coin
    public MarksmansCoin MCoinCard;
    public float MCoinDamage = 0;
    public void MCoinIncrement()
    {
        int MCoinCount = itemMaster.GetItemCount(MCoinCard);
        if (MCoinCount == 0 || MCoinDamage >= MCoinCard.MaxiumumDamage) return;
        MCoinDamage += MCoinCard.IncrementalDamage * MCoinCount;
        if (MCoinDamage <= MCoinCard.MaxiumumDamage) UIManager._.WriteToNotification("Marksman's Coin increased damage by " + MCoinCard.IncrementalDamage * MCoinCount * 100 + "%",5f);
    }
    #endregion


    #region Marksman's Lament
    public MarksmansLament MLamentCard;
    public void MLamentReset()
    {
        int MLamentCount = itemMaster.GetItemCount(MLamentCard);
        if (MLamentCount == 0 || itemMaster.onMissEffectHandler.MLamentDamage == 0) return;
        itemMaster.onMissEffectHandler.MLamentDamage = 0;
        UIManager._.WriteToNotification("Marksman's Lament relieved");
    }

    #endregion

    public void OnHitEffect()
    {
        MCoinIncrement();
        MLamentReset();
    }
}
