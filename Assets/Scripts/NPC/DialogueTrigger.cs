using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueTrigger : MonoBehaviour
{
    public NPCConversation[] convo;
    public int convoInit = 0;
    [SerializeField] protected int timesSpoken = 0;
    [SerializeField] protected bool first = true;
    public virtual void StartConvo()
    {
        int whichBranch = Mathf.Clamp(timesSpoken, 0, convo.Length-1);
        ConversationManager.Instance.StartConversation(convo[whichBranch]);
        ConversationManager.Instance.SetInt("Initial", convoInit);
        
    }

    public virtual int ReturnConvoInt(int input)
    {
        return input;
    }



}
