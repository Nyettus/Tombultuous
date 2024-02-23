using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatHubTrigger : DialogueTrigger
{
    public override void StartConvo()
    {
        base.StartConvo();
        Debug.Log(Mathf.Clamp(timesSpoken + 1, 0, 1));

        if (timesSpoken != 3)
        {
            timesSpoken++;
            timesSpoken = Mathf.Clamp(timesSpoken, 0, convo.Length - 2);
        }

    }

    public void Activate3rdDialogue()
    {
        timesSpoken = 3;
    }
}
