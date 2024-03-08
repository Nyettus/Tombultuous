using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HatShop : HubMenuBase
{
    [SerializeField] private Canvas displayCanvas;
    [SerializeField] private TextMeshProUGUI displayItemName,displayItemLore,displayItemDesc;
    protected override void Start()
    {
        base.Start();
        HideDisplay();
    }

    public void DisplayItem(ItemBase item)
    {
        if (item == null)
        {
            HideDisplay();
            return;
        }
        displayCanvas.enabled = true;
        displayItemName.text = item.itemName;
        displayItemLore.text = item.itemLore;
        displayItemDesc.text = item.itemDesc;

    }


    public void HideDisplay()
    {
        displayCanvas.enabled = false;
    }


}
