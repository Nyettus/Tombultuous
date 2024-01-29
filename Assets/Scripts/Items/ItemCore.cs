using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCore : MonoBehaviour
{
    public ItemBase[] baseItems;

    private string itemName => baseItems[0].itemName;
    private string itemDesc => baseItems[0].itemDesc;
    private Canvas canvas;
    private CanvasGroup opacity;
    public TextMeshProUGUI UIName, UIDesc;

    private Transform loreHolderBase, loreHolder;
    private float UIdistance;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        loreHolderBase = transform.GetChild(0);
        loreHolder = transform.GetChild(0).GetChild(0);
        opacity = GetComponentInChildren<CanvasGroup>();
        opacity.alpha = 0;

        Invoke("Establish", 0.1f);
    }

    void FixedUpdate()
    {
        if (GameManager._.CheckMasterError()) return;
        UIdistance = Mathf.Clamp(-0.2f * Vector3.Distance(this.transform.position, GameManager._.Master.transform.position) + 1.6f, 0, 1);
        if (UIdistance != 0)
        {
            loreHolderBase.LookAt(GameManager._.Master.transform);
            loreHolderBase.eulerAngles = new Vector3(0, loreHolderBase.eulerAngles.y + 180f, loreHolderBase.eulerAngles.z);
            loreHolder.LookAt(GameManager._.Master.transform);
            loreHolder.eulerAngles = new Vector3(0, loreHolder.eulerAngles.y + 180f, loreHolder.eulerAngles.z);
            opacity.alpha = UIdistance;
        }
    }

    private void Establish()
    {
        UIName.text = itemName;
        UIDesc.text = itemDesc;
    }

    public void onPickup()
    {
        Debug.Log("Item picked up");
        foreach(ItemBase item in baseItems)
        {
            if(item is PermanentBuffItem)
            {
                var holding = item as PermanentBuffItem;
                foreach(StatBuff buff in holding.buffs)
                {
                    if(buff.type == StatType.Health)
                    {
                        GameManager._.Master.healthMaster.HealFlesh((int)buff.change);
                    }
                }
            }
        }
        Destroy(this.gameObject);
    }

}
