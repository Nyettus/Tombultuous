using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : SingletonPersist<UIManager>
{
    [SerializeField]
    public InputActionAsset InputMap;
    private InputActionMap UIMap => InputMap.FindActionMap("UIMap");
    private GameManager gm => GameManager._;

    public int[] healthBreakdown = new int[] { 1, 1, 1, 1 };
    public int[] AmmoBreakdown = new int[] { -1, -1 };
    public float specialBreakdown = 1;



    private void Awake()
    {
        Startup(this);
    }

    void Start()
    {
        UIMap.Enable();
        UIMap.FindAction("Pause").performed += gm.Pause;
        PlayerHealth.OnUpdateHealth += ChangeHealthValue;
        WeaponController.OnUpdateAmmo += ChangeAmmoValues;
        WeaponController.OnUpdateSpecial += ChangeSpecialValue;
    }



    #region HealthUpdate




    public void ChangeHealthValue()
    {
        healthBreakdown = new int[]{gm.Master.healthMaster.flesh,
        gm.Master.healthMaster.fleshHealthMax,
        gm.Master.itemMaster.M_OverHealth,
        gm.Master.itemMaster.M_DecayHealth };
    }

    #endregion

    #region AmmoUpdate
    public void ChangeAmmoValues()
    {
        var currentGun = gm.Master.weaponMaster.equippedGuns[gm.Master.weaponMaster.selectedWeapon];
        if(currentGun.TryGetComponent<RangedWeaponBase>(out RangedWeaponBase compono)&& compono.requireReload)
        {
            AmmoBreakdown[0] = compono.curMag;
            AmmoBreakdown[1] = compono.magSize;
        }
        else
        {
            AmmoBreakdown = new int[] { -1, -1 };
        }
    }
    #endregion

    #region
    public void ChangeSpecialValue()
    {
        var currentGun = gm.Master.weaponMaster.equippedGuns[gm.Master.weaponMaster.selectedWeapon];
        specialBreakdown = currentGun.specialPercentage;
    }


    #endregion



    void OnDisable()
    {
        UIMap.Disable();
        UIMap.FindAction("Pause").performed -= gm.Pause;
        PlayerHealth.OnUpdateHealth -= ChangeHealthValue;
        WeaponController.OnUpdateAmmo -= ChangeAmmoValues;
        WeaponController.OnUpdateSpecial -= ChangeSpecialValue;
    }





}
