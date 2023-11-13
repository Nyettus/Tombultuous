using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAesthetic : MonoBehaviour
{
    
    private WeaponCore master;
    private Canvas canvas;
    private CanvasGroup opacity;
    public TextMeshProUGUI weaponName, atkDesc, spkDesc,lore;

    private Transform loreHolderBase,loreHolder;
    private float UIdistance;

    private void Start()
    {
        master = GetComponent<WeaponCore>();
        canvas = GetComponentInChildren<Canvas>();
        loreHolderBase = transform.GetChild(0);
        loreHolder = transform.GetChild(0).GetChild(0);
        opacity = GetComponentInChildren<CanvasGroup>();
        opacity.alpha = 0;

        Invoke("Establish", 0.1f);


    }

    // Update is called once per frame
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
        if (master.equipped)
            canvas.enabled = false;
        else
            canvas.enabled = true;
    }

    private void Establish()
    {
        weaponName.text = master.weaponName;
        atkDesc.text = master.atkDesc;
        spkDesc.text = master.spkDesc;
        lore.text = master.description;
    }



}
