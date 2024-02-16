using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : SingletonPersist<UIManager>
{
    [SerializeField]
    public InputActionAsset InputMap;

    public UIpopUp popUp;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
        if (gm.CheckMasterError()) return;
        healthBreakdown = new int[]{gm.Master.healthMaster.flesh,
        gm.Master.healthMaster.fleshHealthMax,
        gm.Master.itemMaster.M_OverHealth,
        gm.Master.itemMaster.M_DecayHealth };
    }

    #endregion

    #region AmmoUpdate
    private void ChangeAmmoValues()
    {
        if (gm.CheckMasterError()) return;
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
        if (gm.CheckMasterError()) return;
        var currentGun = gm.Master.weaponMaster.equippedGuns[gm.Master.weaponMaster.selectedWeapon];
        specialBreakdown = currentGun.specialPercentage;
    }


    #endregion

    #region Dash update
    private void ChangeDashValue()
    {
        if (gm.CheckMasterError()) return;
        dashBreakdown = gm.Master.movementMaster.dashPercentage;
    }

    #endregion




    #region Reload update
    private void ChangeReloadValue()
    {
        if (gm.CheckMasterError()) return;
        var currentGun = gm.Master.weaponMaster.equippedGuns[gm.Master.weaponMaster.selectedWeapon];
        if (currentGun.TryGetComponent<RangedWeaponBase>(out RangedWeaponBase compono) && compono.requireReload)
        {
            reloadBreakdown = compono.reloadPercentage;
        }
    }


    #endregion

    #region Notification
    public void WriteToNotification(string input,float speed = 1)
    {
        popUp.AddNotification(input, speed);
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
