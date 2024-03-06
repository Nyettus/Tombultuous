using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private void Start()
    {
        GameManager._.paused = false;
        Time.timeScale = 1;
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void GoToHub()
    {
        GameManager._.TransitionScene((int)Scenes.Hub);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("PermGold", 0);
        PlayerPrefs.SetInt("LossCount", 0);
        PlayerPrefs.SetInt("WinCount", 0);
        PlayerPrefs.SetInt("NPC_Hat", 0);
        PlayerPrefs.SetInt("VK_Shop_Recycle", 0);
        PlayerPrefs.SetInt("VK_Shop_HealingCharge", 0);
    }

}
