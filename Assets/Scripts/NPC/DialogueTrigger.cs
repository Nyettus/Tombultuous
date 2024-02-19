using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueTrigger : MonoBehaviour
{
    public NPCConversation convo;
    public int convoInit = 0;
    private bool first = true;
    public void StartConvo()
    {

        ConversationManager.Instance.StartConversation(convo);
        ConversationManager.Instance.SetInt("Initial", convoInit);
        Debug.Log(ConversationManager.Instance.GetInt("Initial"));
        if (first && convoInit != 2)
        {
            Debug.Log("set int");
            first = false;
            convoInit = 1;
        }
    }

}
