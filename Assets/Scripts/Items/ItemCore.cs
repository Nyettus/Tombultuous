using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCore : MonoBehaviour
{
    private CanvasGroup opacity;
    public TextMeshProUGUI itemName, itemDesc;
    private Transform loreHolderBase,loreHolder;
    private float UIdistance;

    public virtual void PickupItem()
    {
        
    }

    protected virtual void Start()
    {
        loreHolderBase = transform.GetChild(0);
        loreHolder = transform.GetChild(0).GetChild(0);
        opacity = GetComponentInChildren<CanvasGroup>();
        opacity.alpha = 0;
    }

    protected virtual void Update()
    {
        UIdistance = Mathf.Clamp(-0.2f * Vector3.Distance(this.transform.position, GameManager.instance.Master.transform.position) + 1.6f, 0, 1);
        if (UIdistance != 0)
        {
            loreHolderBase.LookAt(GameManager.instance.Master.transform);
            loreHolderBase.eulerAngles = new Vector3(0, loreHolderBase.eulerAngles.y+180f, loreHolderBase.eulerAngles.z);
            loreHolder.LookAt(GameManager.instance.Master.transform);
            loreHolder.eulerAngles = new Vector3(0, loreHolder.eulerAngles.y + 180f, loreHolder.eulerAngles.z);
            opacity.alpha = UIdistance;
        }
        transform.Rotate(new Vector3(0, 1, 0));
    }

}
