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
        int bootsCount = itemMaster.GetItemCount(bootsCard);
        return timeEval * bootsCount * bootsCard.bootSpeedIncrease;
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
        bigBootsBuffTime = bootsCard.bootTimeIncrease;
        bigBootsEnabled = true;
    }

    #endregion

    #region Adrenaline
    public Adrenaline adrenalineCard;
    private void AdrenalineActivate()
    {
        int adrenalineCount = itemMaster.GetItemCount(adrenalineCard);
        if (adrenalineCount == 0) return;
        if(Random.value < adrenalineCard.healChance)
        {
            GameManager._.Master.healthMaster.HealFlesh(adrenalineCard.healAmount);
        }
    }
    #endregion

    #region Second Wings
    public SecondWings secondWingsCard;
    private void SecondWingsActivate()
    {
        int wingsCount = itemMaster.GetItemCount(secondWingsCard);
        if (wingsCount == 0 || GameManager._.Master.grounded) return;
        var movementMaster = GameManager._.Master.movementMaster;
        movementMaster.jumpCount = movementMaster.b_jumpCount;
    }

    #endregion

    public void OnKill()
    {
        EnableBoots();
        SecondWingsActivate();
        AdrenalineActivate();
    }

}
