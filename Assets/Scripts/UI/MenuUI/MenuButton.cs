using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void EndGame()
    {
        Application.Quit();
    }

    public void GoToHub()
    {
        GameManager._.TransitionScene(0);
    }

}
