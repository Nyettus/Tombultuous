using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultKeeperTrigger : DialogueTrigger
{
    private bool hatArrived;
    private void Start()
    {
        PrefsCheck();
    }

    public override void StartConvo()
    {
        base.StartConvo();
        convoInit = ReturnConvoInt(1);
        if (hatArrived) timesSpoken = 2;
    }
    public override int ReturnConvoInt(int input)
    {

        if (convoInit == 2) return 2;
        else return input;
    }


    private void PrefsCheck()
    {
        hatArrived = PlayerPrefs.GetInt("NPC_Hat", 0) == 1;
        if (hatArrived)
        {
            timesSpoken = 1;
        }
        int money = PlayerPrefs.GetInt("PermGold", 0);
        if (money > 0 && !hatArrived)
        {
            timesSpoken = 2;
        }

    }
}
