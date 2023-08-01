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
        Debug.Log(BigBootsAdd());
    }
    private void EnableBoots()
    {
        var boots = itemMaster.itemList.FirstOrDefault(il => il.item == bootsCard);
        if (boots == null) return;
        bigBootsTimer = 0;
        bigBootsBuffTime = boots.stacks * bootsCard.bootTimeIncrease;
        bigBootsEnabled = true;
    }

    #endregion


    //debuging
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            OnKill();
        }
    }


    public void OnKill()
    {
        EnableBoots();
    }

}
