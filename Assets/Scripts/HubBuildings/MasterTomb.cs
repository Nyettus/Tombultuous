using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MasterTomb : MonoBehaviour
{
    public VaultKeeperTrigger VaultkeeperDialogue;
    public void DestroySelf()
    {
        VaultkeeperDialogue.convoInit = VaultkeeperDialogue.ReturnConvoInt(2);
        Destroy(gameObject);
    }
}
