using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UsefulBox;
using TMPro;

public class VaultShop : MonoBehaviour
{

    public VaultShopPrices card;
    private void Start()
    {
        SetupOptions();
    }

    private void SetupOptions()
    {
        RecycleSetup();
        TestIncrementSetup();
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


    [SerializeField] private TextMeshProUGUI testIncrementTest;
    [SerializeField] private Button testIncrementButton;
    private void TestIncrementSetup()
    {
        int state = PlayerPrefs.GetInt("VK_Shop_IncTest", 0);
        if (state < card.layerPrice.Length)
        {
            testIncrementButton.interactable = true;
            testIncrementTest.text = "" + card.layerPrice[state] + "g";
        }
        else
        {
            testIncrementButton.interactable = false;
            testIncrementTest.text = "Bought";
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

    public void PurchaseTestIncremental()
    {
        string testTag = "VK_Shop_IncTest";
        int value = PlayerPrefs.GetInt(testTag, 0);
        if (value > card.layerPrice.Length) return;
        PurchaseIncremental(testTag, card.layerPrice);
    }







    #region Skeleton Purchasing
    private void RawPurchase(string tag,int price, int value)
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
                RawPurchase(tag, price[currentLevel], currentLevel+1);
            }
        }
    }
    #endregion


}
