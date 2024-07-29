using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public WeaponController master;
    [SerializeField] private CanvasGroup group;
    private int selectedWeapon => master.selectedWeapon;
    private int pockets => master.pockets;
    private float rate = 10f;
    [SerializeField] private float alpha;

    [SerializeField] private WeaponSlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        Initialise();

    }

    // Update is called once per frame
    void Update()
    {
        SetVisibiltiy();
    }

    public void Initialise()
    {
        UpdatePockets();
        master.weaponUI = this;
    }

    public void UpdatePockets()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            bool state = pockets > i;
            slots[i].WholeEnable(state);
            if (i < master.equippedGuns.Length)
            {
                slots[i].slotNo.text = "" + (i + 1);
            }
            else
            {
                slots[i].slotNo.text = "";
            }

        }
        UpdateVisiblity();
    }

    public void UpdateHolding(string name)
    {
        foreach (WeaponSlot slot in slots)
        {
            slot.SetName(false);
        }

        slots[selectedWeapon].SetName(true, name);
        UpdateVisiblity();
    }
    public void UpdateVisiblity()
    {
        alpha = 5;
        for (int i = 0; i < slots.Length; i++)
        {
            if (i >= master.equippedGuns.Length) return;
            slots[i].slotNo.text = "" + (i + 1);

        }

    }
    private void SetVisibiltiy()
    {
        if (alpha > 0.01f)
        {
            alpha -= rate * Time.deltaTime;
            group.alpha = alpha;
        }
        else
        {
            group.alpha = 0;
            alpha = 0;
        }

    }

}
