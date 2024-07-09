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
        MarrowExtractorSetup();
        ScrapRetSetup();
    }



    #region Split Setup
    [Header("Recycle")]
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

    [Header("Scrap Retention")]
    [SerializeField] private TextMeshProUGUI scrapRetChargeText;
    [SerializeField] private Button scrapRetChargeButton;
    [SerializeField] private Image scrapRetChargeUpgradeBar;
    [SerializeField] private Canvas scrapRetBlocker;
    private void ScrapRetSetup()
    {
        int state = PlayerPrefs.GetInt("VK_Shop_ScrapRetention", 0);
        int recycleState = PlayerPrefs.GetInt("VK_Shop_Recycle", 0);
        if (state < card.scrapRetentionPrice.Length)
        {
            scrapRetChargeButton.interactable = true;
            scrapRetChargeText.text = "" + card.scrapRetentionPrice[state] + "g";
        }
        else
        {
            scrapRetChargeButton.interactable = false;
            scrapRetChargeText.text = "Bought";
        }
        scrapRetChargeUpgradeBar.fillAmount = (float)state / card.scrapRetentionPrice.Length;
        if(recycleState == 0)
        {
            scrapRetBlocker.enabled = true;
            scrapRetChargeButton.interactable = false;
        }
        else
        {
            scrapRetBlocker.enabled = false;
        }

    }

    [Header("Healing pots")]
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

    [Header("Marrow Extractor")]
    [SerializeField] private TextMeshProUGUI marrowExtractorText;
    [SerializeField] private Button marrowExtractorButton;
    [SerializeField] private Canvas marrowExtratorLocked;
    private void MarrowExtractorSetup()
    {
        int state = PlayerPrefs.GetInt("VK_Shop_MarrowExtractor");
        int healPotState = PlayerPrefs.GetInt("VK_Shop_HealingCharge");

        if (state == 0)
        {
            marrowExtractorButton.interactable = true;
            marrowExtractorText.text = "" + card.boneMarrowPrice + "g";
        }
        else
        {
            marrowExtractorButton.interactable = false;
            marrowExtractorText.text = "Bought";
        }
        if (healPotState == 0)
        {
            marrowExtratorLocked.enabled = true;
            marrowExtractorButton.interactable = false;
            return;
        }
        else
        {
            marrowExtratorLocked.enabled = false;
        }


    }

    #endregion

    public void PurchaseRecycle()
    {
        string recycleTag = "VK_Shop_Recycle";
        int value = PlayerPrefs.GetInt(recycleTag, 0);
        if (value > 0) return;
        PurchaseSingular(recycleTag, card.recyclePrice);

    }

    public void PurchaseScrapRetention()
    {
        string scrapRetTag = "VK_Shop_ScrapRetention";
        int state = PlayerPrefs.GetInt(scrapRetTag, 0);
        if (state > card.scrapRetentionPrice.Length) return;
        PurchaseIncremental(scrapRetTag, card.scrapRetentionPrice);
        state = PlayerPrefs.GetInt(scrapRetTag, 0);
        scrapRetChargeUpgradeBar.fillAmount = (float)state / card.scrapRetentionPrice.Length;
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

    public void PurchaseMarrowExtractor()
    {
        string marrowTag = "VK_Shop_MarrowExtractor";
        int value = PlayerPrefs.GetInt(marrowTag, 0);
        if (value > 0) return;
        PurchaseSingular(marrowTag, card.boneMarrowPrice);
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
