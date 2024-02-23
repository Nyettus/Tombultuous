using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultKeeperTrigger : DialogueTrigger
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("NPC_Hat", 0) == 1)
        {
            timesSpoken = 1;
        }
    }

    public override void StartConvo()
    {
        base.StartConvo();
        convoInit = ReturnConvoInt(1);
    }
    public override int ReturnConvoInt(int input)
    {

        if (convoInit == 2) return 2;
        else return input;
    }
}
