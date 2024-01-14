using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public PlayerMaster master;
    public BoxCollider meleeHitbox;
    public float damageMult => master.damage + master.itemMaster.M_DamageMult;
    public float hasteMult => master.itemMaster.M_Haste;
    public int pockets => Mathf.Clamp(master.pockets + master.itemMaster.M_Pockets, master.itemMaster.MIN_Pockets, master.itemMaster.MAX_Pockets);

    public int selectedWeapon = 0;
    private int previousWeapon = -1;
    [SerializeField]
    public WeaponCore[] equippedGuns;

    public delegate void UpdateAmmo();
    public static event UpdateAmmo OnUpdateAmmo;
    public void OnAmmoChangeEvent()
    {

        if (OnUpdateAmmo != null)
            OnUpdateAmmo();
    }

    public delegate void UpdateSpecial();
    public static event UpdateSpecial OnUpdateSpecial;
    public void OnSpecialChangeEvent()
    {

        if (OnUpdateSpecial != null)
            OnUpdateSpecial();
    }

    // Start is called before the first frame update
    void Start()
    {
        Establish();
        UpdateEquipped();


    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (equippedGuns[selectedWeapon] != null)
        {
            equippedGuns[selectedWeapon].shooting = context.performed;

        }

    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (context.performed && equippedGuns[selectedWeapon] != null)
        {
            equippedGuns[selectedWeapon].Special();

        }

    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int val = (int)context.ReadValue<float>() - 1;
            if (equippedGuns.Length > val && equippedGuns[val] != null && val != selectedWeapon)
            {
                previousWeapon = selectedWeapon;
                selectedWeapon = val;
                SelectWeapon();

            }
        }
    }

    public void OnQuickSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (previousWeapon != -1)
            {
                int temp = selectedWeapon;
                selectedWeapon = previousWeapon;
                SelectWeapon();
                previousWeapon = temp;
            }


        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed && equippedGuns[selectedWeapon].TryGetComponent(out RangedWeaponBase yep))
        {
            if (yep.requireReload && (yep.curMag != yep.magSize))
            {
                yep.reloading = true;
                yep.Invoke("Reload", yep.reloadTime);
            }

        }
    }

    public void CleanseWeapons()
    {
        selectedWeapon = 0;
        foreach (WeaponCore weapon in equippedGuns)
        {
            if (weapon != null) Destroy(weapon.gameObject);
        }
        for (int i = 0; i < equippedGuns.Length; i++)
        {
            equippedGuns[i] = null;
        }
    }

    private void Establish()
    {
        master = GetComponentInParent<PlayerMaster>();
        equippedGuns = new WeaponCore[pockets];

    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                equippedGuns[i].OnSwitch();

            }

            else
                weapon.gameObject.SetActive(false);


            i++;
        }

    }

    public void UpdateEquipped()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            equippedGuns[i] = weapon.GetComponent<WeaponCore>();
            i++;
        }

    }

    public void RefreshPockets()
    {
        if (pockets != equippedGuns.Length)
        {
            Debug.Log("Attempted to refresh pockets");
            WeaponCore[] newArray = new WeaponCore[pockets];
            equippedGuns.CopyTo(newArray, 0);
            equippedGuns = newArray;
        }
    }




}
