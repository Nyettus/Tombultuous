using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UsefulBox;

public class HUD : MonoBehaviour
{
    
    public TextMeshProUGUI health,ammo,special,dash,reload;

    public Image reloadBar, specialBar, dashBar;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnUpdateHealth += UpdateHealth;
        WeaponController.OnUpdateAmmo += UpdateAmmo;
        WeaponController.OnUpdateSpecial += UpdateSpecial;
        PlayerController.OnUpdateDash += UpdateDash;
        WeaponController.OnUpdateReload += UpdateReload;
        dashBar.enabled = false;
        reloadBar.enabled = false;
        specialBar.enabled = false;
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
        ConvertToBar(UIManager._.specialBreakdown, specialBar);
    }

    private void UpdateDash()
    {
        StandardPercentage(UIManager._.dashBreakdown, dash);
        ConvertToBar(UIManager._.dashBreakdown, dashBar);
    }

    private void UpdateReload()
    {
        StandardPercentage(UIManager._.reloadBreakdown, reload);
        ConvertToBar(UIManager._.reloadBreakdown, reloadBar);
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


    private void ConvertToBar(float input, Image bar)
    {
        if(input == 1)
        {
            bar.enabled = false;
            return;
        }
        else
        {
            bar.enabled = true;
            bar.fillAmount = input;
        }
    }
}
