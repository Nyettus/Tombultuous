using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatHubTrigger : DialogueTrigger
{
    private bool inConstruction = false;
    public override void StartConvo()
    {
        CheckInit();
        base.StartConvo();
        Debug.Log(timesSpoken);
        if (timesSpoken < 2)
        {
            timesSpoken++;
            timesSpoken = Mathf.Clamp(timesSpoken, 0, convo.Length - 2);
        }

    }


    private void CheckInit()
    {
        int hatState = PlayerPrefs.GetInt("NPC_Hat", 0);
        if (hatState == 2)
        {
            timesSpoken = 4;
        }
        if (inConstruction)
        {
            timesSpoken = 3;
            Debug.Log("Is still in progress");
        }

        int scrapState = PlayerPrefs.GetInt("Hat_Shop_ItemScrap",0);
        if (scrapState >= 2)
            convoInit = 1;
    }

    public void EnableShop()
    {
        if (PlayerPrefs.GetInt("NPC_Hat", 0) == 2)
            return;
        int totalScrap = PlayerPrefs.GetInt("Hat_Shop_ItemScrap", 0);
        int newScrap = totalScrap - 2;
        PlayerPrefs.SetInt("Hat_Shop_ItemScrap", newScrap);
        PlayerPrefs.SetInt("NPC_Hat", 2);
        inConstruction = true;
        GameManager._.goldManager.OnGoldChangeEvent();
    }


}
