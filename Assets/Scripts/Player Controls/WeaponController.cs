using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public PlayerMaster master;



    public int selectedWeapon = 0;
    private int previousWeapon = -1;
    [SerializeField]
    public WeaponCore[] equippedGuns;
    // Start is called before the first frame update
    void Start()
    {
        Establish();
        updateEquipped();
        //SelectWeapon();

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(equippedGuns[selectedWeapon]!=null)
            equippedGuns[selectedWeapon].shooting = context.performed;
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (context.performed&&equippedGuns[selectedWeapon]!=null)
        {
            equippedGuns[selectedWeapon].Special();
        }

    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int val = (int)context.ReadValue<float>()-1;
            if (equippedGuns.Length>val&&equippedGuns[val] != null&&val!=selectedWeapon)
            {
                previousWeapon = selectedWeapon;
                Debug.Log(previousWeapon);
                selectedWeapon = val;
                SelectWeapon();
            }
        }
    }

    public void OnQuickSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(previousWeapon != -1)
            {
                int temp = selectedWeapon;
                selectedWeapon = previousWeapon;
                Debug.Log(selectedWeapon);
                SelectWeapon();
                previousWeapon = temp;
            }


        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if(context.performed&&equippedGuns[selectedWeapon].TryGetComponent(out RangedWeaponBase yep))
        {
            if (yep.requireReload&&(yep.curMag != yep.magSize))
            {
                yep.reloading = true;
                yep.Invoke("Reload", yep.reloadTime);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Establish()
    {
        master = GetComponentInParent<PlayerMaster>();
        equippedGuns = new WeaponCore[master.pockets];
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);


            i++;
        }

    }

    public void updateEquipped()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            equippedGuns[i] = weapon.GetComponent<WeaponCore>();
            i++;
        }
        
    }

}
