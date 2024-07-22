using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName, lore, desc;
    [SerializeField] private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        UIManager._.itemPopUp = this;
    }


    public void ShowItemNotif(ItemBase item)
    {
        itemName.text = item.itemName;
        lore.text = item.itemLore;
        desc.text = item.itemDesc;
        anim.Play("Animation");
    }
}
