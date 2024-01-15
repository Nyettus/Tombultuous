using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UsefulBox;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI health,ammo,special,dash,reload;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnUpdateHealth += UpdateHealth;
        WeaponController.OnUpdateAmmo += UpdateAmmo;
        WeaponController.OnUpdateSpecial += UpdateSpecial;
        PlayerController.OnUpdateDash += UpdateDash;
        WeaponController.OnUpdateReload += UpdateReload;

    }



    private void OnDestroy()
    {
        PlayerHealth.OnUpdateHealth -= UpdateHealth;
        WeaponController.OnUpdateAmmo -= UpdateAmmo;
        WeaponController.OnUpdateSpecial -= UpdateSpecial;
        PlayerController.OnUpdateDash -= UpdateDash;
        WeaponController.OnUpdateReload -= UpdateReload;
    }



    private void UpdateHealth()
    {
        var healthBreakdown = UIManager._.healthBreakdown;
        health.text = ""+healthBreakdown[0]+" / "+healthBreakdown[1]+" | "+healthBreakdown[2]+" "+healthBreakdown[3];
    }
    private void UpdateAmmo()
    {
        var ammoBreakdown = UIManager._.AmmoBreakdown;
        if (ammoBreakdown[0] != -1) ammo.text = "" + ammoBreakdown[0] + " / " + ammoBreakdown[1];
        else
            ammo.text = "Inf";
    }

    private void UpdateSpecial()
    {
        StandardPercentage(UIManager._.specialBreakdown, special);
    }

    private void UpdateDash()
    {
        StandardPercentage(UIManager._.dashBreakdown, dash);
    }

    private void UpdateReload()
    {
        StandardPercentage(UIManager._.reloadBreakdown, reload);
    }


    private void StandardPercentage(float input, TextMeshProUGUI label)
    {
        var breakdown = input;
        if (breakdown == 1)
        {
            label.text = "";
            return;
        }
        string asPercent = PsychoticBox.ConvertToPercent(input);
        label.text = asPercent;
    }
}
