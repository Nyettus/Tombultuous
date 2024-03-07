using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UsefulBox;
using TMPro;
using DialogueEditor;

public class VaultShop : HubMenuBase
{

    public VaultShopPrices card;
    protected override void Start()
    {

        SetupOptions();
        base.Start();
    }
    private void SetupOptions()
    {
        RecycleSetup();
        HealingChargeSetup();
    }



    #region Split Setup
    [SerializeField] private TextMeshProUGUI recyclePriceText;
    [SerializeField] private Button recycleButton;
    private void RecycleSetup()
    {
        int state = PlayerPrefs.GetInt("VK_Shop_Recycle", 0);
        if (state == 0)
        {
            recycleButton.interactable = true;
            recyclePriceText.text = "" + card.recyclePrice + "g";
        }
        else
        {
            recycleButton.interactable = false;
            recyclePriceText.text = "Bought";
        }

    }


    [SerializeField] private TextMeshProUGUI healingChargeText;
    [SerializeField] private Button healingChargeButton;
    [SerializeField] private Image healingChargeUpgradeBar;
    private void HealingChargeSetup()
    {
        int state = PlayerPrefs.GetInt("VK_Shop_HealingCharge", 0);
        if (state < card.healingPrice.Length)
        {
            healingChargeButton.interactable = true;
            healingChargeText.text = "" + card.healingPrice[state] + "g";
        }
        else
        {
            healingChargeButton.interactable = false;
            healingChargeText.text = "Bought";
        }
        healingChargeUpgradeBar.fillAmount = (float)state / card.healingPrice.Length;
    }

    #endregion

    public void PurchaseRecycle()
    {
        string recycleTag = "VK_Shop_Recycle";
        int value = PlayerPrefs.GetInt(recycleTag, 0);
        if (value > 0) return;
        PurchaseSingular(recycleTag, card.recyclePrice);

    }

    public void PurchaseHealingCharges()
    {
        string healTag = "VK_Shop_HealingCharge";
        int state = PlayerPrefs.GetInt(healTag, 0);
        if (state > card.healingPrice.Length) return;
        PurchaseIncremental(healTag, card.healingPrice);
        state = PlayerPrefs.GetInt(healTag, 0);
        healingChargeUpgradeBar.fillAmount = (float)state / card.healingPrice.Length;

    }


    public ItemBase itemToBuy;
    public void BuyItem()
    {
        itemToBuy.unlocked = true;
        SaveManager._.unlockedItems.Add(itemToBuy.ID, true);
        //the save goes onto the confirm purchase button
    }




    #region Skeleton Purchasing
    private void RawPurchase(string tag, int price, int value)
    {
        int money = PlayerPrefs.GetInt("PermGold", 0);
        int remainingMoney = money - price;
        PlayerPrefs.SetInt("PermGold", remainingMoney);
        PlayerPrefs.SetInt(tag, value);
        Debug.Log("Successfully purchased: " + tag);
        SetupOptions();
        GameManager._.goldManager.OnGoldChangeEvent();
        return;
    }

    private void PurchaseSingular(string tag, int price)
    {
        if (PlayerPrefs.GetInt(tag, 0) > 0)
        {
            Debug.LogWarning("Tried to buy already owned upgrade");
            return;
        }
        else
        {
            int money = PlayerPrefs.GetInt("PermGold", 0);
            if (money < price)
            {
                Debug.LogWarning("Cannot afford item");
                return;
            }
            else
            {
                RawPurchase(tag, price, 1);
            }
        }

    }
    private void PurchaseIncremental(string tag, int[] price)
    {
        int currentLevel = PlayerPrefs.GetInt(tag, 0);
        if (currentLevel > price.Length)
        {
            Debug.LogWarning("Tried to buy already maxxed upgrade");
            return;
        }
        else
        {
            int money = PlayerPrefs.GetInt("PermGold", 0);
            if (money < price[currentLevel])
            {
                Debug.LogWarning("Cannot afford item");
                return;
            }
            else
            {
                RawPurchase(tag, price[currentLevel], currentLevel + 1);
            }
        }
    }
    #endregion


}
