using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OnKillItemHandler : MonoBehaviour
{

    public float bigBootsBuffTime = 10f;

    public float bigBootsTimer = 0f;
    public bool bigBootsEnabled = false;
    

    public ItemMaster itemMaster;


    public float bootTimeIncrease = 2f;
    public float bootSpeedIncrease = 10f;
    public AnimationCurve bootTimingCurve;
    public float BigBootsAdd() 
    {
        var timeEval = bootTimingCurve.Evaluate(bigBootsTimer / bigBootsBuffTime);
        return timeEval * bootSpeedIncrease;
    }

    void FixedUpdate()
    {
        HandleBoots();
    }


    #region Boots
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
        var boots = itemMaster.itemList.FirstOrDefault(il => il.name == "Big_Boots");
        if (boots == null) return;
        bigBootsTimer = 0;
        bigBootsBuffTime = boots.stacks * bootTimeIncrease;
        bigBootsEnabled = true;
    }

    #endregion
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            OnKill();
        }
    }

    private void EnableSecondItem()
    {
        var item = itemMaster.itemList.FirstOrDefault(il => il.name == "Second_Itme");


    }

    public void OnKill()
    {
        EnableBoots();
        EnableSecondItem();

    }

}
