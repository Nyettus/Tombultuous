using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OnKillItemHandler : MonoBehaviour
{
    public ItemMaster itemMaster;
    void FixedUpdate()
    {
        HandleBoots();
    }


    #region Boots

    public float bigBootsBuffTime = 10f;
    public float bigBootsTimer = 0f;
    public bool bigBootsEnabled = false;

    public BigBoots bootsCard;
    public float BigBootsAdd() 
    {
        var timeEval = bootsCard.bootTimingCurve.Evaluate(bigBootsTimer / bigBootsBuffTime);
        return timeEval * bootsCard.bootSpeedIncrease;
    }

    private void HandleBoots()
    {
        if (bigBootsEnabled == true)
        {
            if (bigBootsTimer <= bigBootsBuffTime)
            {
                bigBootsTimer += Time.fixedDeltaTime;
            }
            else
            {
                bigBootsEnabled = false;
            }

        }

    }
    private void EnableBoots()
    {
        int bootsCount = itemMaster.GetItemCount(bootsCard);
        if (bootsCount == 0) return;
        bigBootsTimer = 0;
        bigBootsBuffTime = bootsCount * bootsCard.bootTimeIncrease;
        bigBootsEnabled = true;
    }

    #endregion

    public void OnKill()
    {
        EnableBoots();
    }

}
