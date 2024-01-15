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
    public float dashBreakdown = 1;
    public float reloadBreakdown = 1;



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
        PlayerController.OnUpdateDash += ChangeDashValue;
        WeaponController.OnUpdateReload += ChangeReloadValue;
    }



    #region HealthUpdate




    private  void ChangeHealthValue()
    {
        healthBreakdown = new int[]{gm.Master.healthMaster.flesh,
        gm.Master.healthMaster.fleshHealthMax,
        gm.Master.itemMaster.M_OverHealth,
        gm.Master.itemMaster.M_DecayHealth };
    }

    #endregion

    #region AmmoUpdate
    private void ChangeAmmoValues()
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

    #region Special Update
    private void ChangeSpecialValue()
    {
        var currentGun = gm.Master.weaponMaster.equippedGuns[gm.Master.weaponMaster.selectedWeapon];
        specialBreakdown = currentGun.specialPercentage;
    }


    #endregion

    #region Dash update
    private void ChangeDashValue()
    {
        dashBreakdown = gm.Master.movementMaster.dashPercentage;
    }

    #endregion

    #region Reload update
    private void ChangeReloadValue()
    {
        var currentGun = gm.Master.weaponMaster.equippedGuns[gm.Master.weaponMaster.selectedWeapon];
        if (currentGun.TryGetComponent<RangedWeaponBase>(out RangedWeaponBase compono) && compono.requireReload)
        {
            reloadBreakdown = compono.reloadPercentage;
        }
    }


    #endregion





    void OnDisable()
    {
        UIMap.Disable();
        UIMap.FindAction("Pause").performed -= gm.Pause;
        PlayerHealth.OnUpdateHealth -= ChangeHealthValue;
        WeaponController.OnUpdateAmmo -= ChangeAmmoValues;
        WeaponController.OnUpdateSpecial -= ChangeSpecialValue;
        PlayerController.OnUpdateDash -= ChangeDashValue;
        WeaponController.OnUpdateReload -= ChangeReloadValue;
    }





}
