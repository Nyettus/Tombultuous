using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MasterTomb : MonoBehaviour
{
    public DialogueTrigger VaultkeeperDialogue;
    public void DestroySelf()
    {
        VaultkeeperDialogue.convoInit = 2;
        Destroy(gameObject);
    }
}
