using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UsefulBox;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI runGold,remainingGold,finalGold,retention;
    private Canvas thisCanvas;
    private void Start()
    {
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.enabled = false;
    }


    public void Initialise()
    {
        GameManager._.isDead = true;
        thisCanvas.enabled = true;

        GoldManager quickRef = GameManager._.goldManager;
        int totalGold = quickRef.gold;
        int remainGold = quickRef.FinalGold(false,false);
        float retain = quickRef.goldRetention;

        runGold.text = "<s>" + totalGold+"g</s>";
        remainingGold.text = "" + remainGold+"g";
        retention.text = "" + PsychoticBox.ConvertToPercent( retain,"0");
        GameManager._.ShowMouse(true);

    }

    public void EndGameButton()
    {

        GameManager._.isDead = false;
        GameManager._.EndGame(false);
    }
}
