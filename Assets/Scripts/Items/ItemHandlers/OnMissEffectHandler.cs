using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMissEffectHandler : MonoBehaviour
{
    public ItemMaster itemMaster;
    #region Marksman's Coin
    public MarksmansCoin MCoinCard;
    private void MCoinReset()
    {
        int MCoinCount = itemMaster.GetItemCount(MCoinCard);
        if (MCoinCount == 0) return;
        if (itemMaster.onHitEffectHandler.MCoinDamage != 0) UIManager._.WriteToNotification("Marksman's Coin reset damage");
        itemMaster.onHitEffectHandler.MCoinDamage = 0;

    }


    #endregion

    #region Marksman's Lament
    public MarksmansLament MLamentCard;
    public float MLamentDamage = 0f;
    private void MLamentIncrement()
    {
        int MLamentCount = itemMaster.GetItemCount(MLamentCard);
        if (MLamentCount == 0 || MLamentDamage >= MLamentCard.MaxiumumDamage) return;
        MLamentDamage += MLamentCard.IncrementalDamage * MLamentCount;
        if (MLamentDamage <= MLamentCard.MaxiumumDamage) UIManager._.WriteToNotification("Marksman's Lament Grows, +" + MLamentCard.IncrementalDamage * MLamentCount * 100 + "% damage", 5f);

    }


    #endregion

    public void OnMissEffect(Vector3 position)
    {
        itemMaster.onPassiveEffectHandler.LRodHit(position);
        MCoinReset();
        MLamentIncrement();
    }
}
