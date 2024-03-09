using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HatShopEntry : MonoBehaviour
{
    [SerializeField] private ItemShopEntry itemEntry;
    [SerializeField] private TextMeshProUGUI itemName, goldCost, scrapCost, buyButtonText;
    [SerializeField] private Button buyButton;

    private void Start()
    {
        if (itemEntry == null) Debug.LogError("Item Entry is null");
        itemName.text = itemEntry.itemToUnlock.itemName;
        goldCost.text = ""+itemEntry.goldCost+"g";
        scrapCost.text = "" + itemEntry.scrapCost + "g";
        RefreshButtonState();
    }

    public void PurchaseButton()
    {
        if (CanAfford(itemEntry.goldCost, itemEntry.scrapCost))
        {
            SaveManager._.BuyItem(itemEntry.itemToUnlock);
            RefreshButtonState();
            SaveManager._.SaveUserDataToFile();
        }
    }

    #region Skeleton Purchasing
    private bool CanAfford(int goldPrice, int scrapPrice)
    {
        int curGold = PlayerPrefs.GetInt("PermGold", 0);
        int curScrap = PlayerPrefs.GetInt("Hat_Shop_ItemScrap", 0);
        if(curGold<goldPrice || curScrap < scrapPrice)
        {
            Debug.LogWarning("Cannot afford item");
            return false;
        }
        else
        {
            int newTotalGold = curGold - goldPrice;
            PlayerPrefs.SetInt("PermGold", newTotalGold);

            int newTotalScrap = curScrap - scrapPrice;
            PlayerPrefs.SetInt("Hat_Shop_ItemScrap", newTotalScrap);

            
            
            return true;
        }
    }


    #endregion


    private void RefreshButtonState()
    {
        if (itemEntry.itemToUnlock.unlocked)
        {
            buyButton.interactable = false;
            buyButtonText.text = "BOUGHT";
        }
        else
        {
            int curGold = PlayerPrefs.GetInt("PermGold", 0);
            int curScrap = PlayerPrefs.GetInt("Hat_Shop_ItemScrap", 0);
            if (curGold < itemEntry.goldCost || curScrap < itemEntry.scrapCost)
            {
                buyButton.interactable = false;
                buyButtonText.text = "<s> Purchase </s>";
            }
            else
            {
                buyButton.interactable = true;
                buyButtonText.text = " Purchase ";
            }


        }
    }
}
