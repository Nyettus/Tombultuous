using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UsefulBox;

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
    public WeaponUI weaponUI;

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

    public delegate void UpdateReload();
    public static event UpdateReload OnUpdateReload;
    public void OnReloadChangeEvent()
    {
        if (OnUpdateReload != null)
            OnUpdateReload();
    }

    // Start is called before the first frame update
    void Start()
    {
        Establish();
        UpdateEquipped();


    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (equippedGuns[selectedWeapon] != null && !GameManager._.ToggleInputs())
        {
            equippedGuns[selectedWeapon].shooting = context.performed;

        }

    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (context.performed && equippedGuns[selectedWeapon] != null && !GameManager._.ToggleInputs())
        {
            equippedGuns[selectedWeapon].Special();

        }

    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager._.ToggleInputs())
        {
            int val = (int)context.ReadValue<float>() - 1;
            weaponUI.UpdateVisiblity();
            if (equippedGuns.Length > val && equippedGuns[val] != null && val != selectedWeapon)
            {
                previousWeapon = selectedWeapon;
                selectedWeapon = val;
                SelectWeapon();

            }
        }
    }

    public void OnScrollSelect(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager._.ToggleInputs())
        {
            Vector2 val = context.ReadValue<Vector2>();
            int totalWeapons = 0;
            foreach(WeaponCore weapon in equippedGuns)
            {
                if (weapon != null) totalWeapons++;
            }
            int change = 0;
            
            if (val.y < 0)
            {
                change = 1;
            }
            else
            {
                change = -1;
            }
            int toSwitch = PsychoticBox.WrapIndex(selectedWeapon + change,totalWeapons);
            if (equippedGuns[toSwitch] != null && selectedWeapon != toSwitch)
            {
                previousWeapon = selectedWeapon;
                selectedWeapon = toSwitch;
                SelectWeapon();
            }

        }

    }

    public void OnQuickSwitch(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager._.ToggleInputs())
        {
            weaponUI.UpdateVisiblity();
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
        if (context.performed && equippedGuns[selectedWeapon] != null && equippedGuns[selectedWeapon].TryGetComponent(out RangedWeaponBase yep) && !GameManager._.ToggleInputs())
        {
            if (yep.requireReload && (yep.curMag != yep.magSize))
            {
                yep.reloading = true;
                yep.Invoke("Reload", yep.reloadTime * GameManager._.Master.weaponMaster.hasteMult);
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
                
                weaponUI.UpdateHolding(equippedGuns[i].weaponName);
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
            weaponUI.UpdatePockets();
        }
    }




}
