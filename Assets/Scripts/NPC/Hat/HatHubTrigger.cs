using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatHubTrigger : DialogueTrigger
{

    public override void StartConvo()
    {
        CheckInit();
        base.StartConvo();

        if (timesSpoken <= 3)
        {
            timesSpoken++;
            timesSpoken = Mathf.Clamp(timesSpoken, 0, convo.Length - 2);
        }

    }

    public void Activate3rdDialogue()
    {
        timesSpoken = 3;
    }

    private void CheckInit()
    {
        if (PlayerPrefs.GetInt("NPC_Hat", 0) == 2)
        {
            timesSpoken = 5;
        }
        if (transform.parent.name == "--InConstruction--")
        {
            timesSpoken = 4;
            Debug.Log("Is still in progress");
        }

        int scrapState = PlayerPrefs.GetInt("Hat_Shop_ItemScrap",0);
        if (scrapState >= 5)
            convoInit = 1;
    }

    public void EnableShop()
    {
        if (PlayerPrefs.GetInt("NPC_Hat", 0) == 2)
            return;
        int totalScrap = PlayerPrefs.GetInt("Hat_Shop_ItemScrap", 0);
        int newScrap = totalScrap - 5;
        PlayerPrefs.SetInt("Hat_Shop_ItemScrap", newScrap);
        PlayerPrefs.SetInt("NPC_Hat", 2);
        GameManager._.goldManager.OnGoldChangeEvent();
    }


}
