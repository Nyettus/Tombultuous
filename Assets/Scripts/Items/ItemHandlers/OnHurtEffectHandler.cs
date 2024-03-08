using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHurtEffectHandler : MonoBehaviour
{
    public ItemMaster itemMaster;
    private void FixedUpdate()
    {
        HandleLaud();
    }

    #region Laudanum
    public Laudanum laudCard;
    private bool laudEnabled = false;
    private float laudEndTime => laudCard.laudTimeIncrease;
    private float laudTimer;

    public float LaudAdd()
    {
        var timeEval = laudCard.laudSpeedControl.Evaluate(laudTimer / laudEndTime);
        int laudCount = itemMaster.GetItemCount(laudCard);
        return timeEval * laudCount * laudCard.laudSpeedIncrease;
    }
    private void HandleLaud()
    {
        if (laudEnabled)
        {
            if (laudTimer <= laudEndTime)
            {
                laudTimer += Time.deltaTime;
            }
            else
            {
                laudEnabled = false;
            }
        }
    }
    private void EnableLaudunum()
    {
        int laudCount = itemMaster.GetItemCount(laudCard);
        if (laudCount == 0) return;
        laudTimer = 0;
        laudEnabled = true;

    }

    #endregion


    public void OnHurtEffect()
    {
        EnableLaudunum();
    }
}
