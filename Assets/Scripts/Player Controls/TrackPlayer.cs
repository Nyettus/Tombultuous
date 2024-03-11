using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TrackPlayer : MonoBehaviour
{
    public Vector3 offset;
    public Canvas interactCanvas;
    public CanvasGroup interactGroup;

    private int canvasState = 0;
    void Update()
    {
        if (GameManager._.CheckMasterError()) return;
        transform.position = GameManager._.Master.transform.position + offset;
        transform.rotation = Camera.main.transform.rotation;
        ChaseOpacity();
    }


    private void ChaseOpacity()
    {
        if (ConversationManager.Instance.IsConversationActive || GameManager._.inMenu)
        {
            interactCanvas.enabled = false;
            return;
        }

        if (canvasState == 1)
        {
            interactCanvas.enabled = true;
        }

        if (Mathf.Abs(interactGroup.alpha-canvasState)>0.01f)
        {
            interactGroup.alpha = Mathf.Lerp(interactGroup.alpha, canvasState, 10 * Time.deltaTime);
        }
        else if (canvasState == 0)
        {
            interactCanvas.enabled = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ShowUI")
        {
            canvasState = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ShowUI")
        {
            canvasState = 0;
        }
    }

}
