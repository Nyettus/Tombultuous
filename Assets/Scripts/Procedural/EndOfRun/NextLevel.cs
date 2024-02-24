using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private int nextIDs = (int)Scenes.Hub;
    public void GotoNextLevel()
    {
        if(nextIDs == 0)
        {
            GameManager._.EndGame(true);
        }
        else
        {
            GameManager._.TransitionScene(nextIDs);
        }
    }
}
