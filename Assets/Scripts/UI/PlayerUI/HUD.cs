using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UsefulBox;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{

    public TextMeshProUGUI health, ammo, special, dash, reload, bossHealthName, goldCounter;
    public GameObject bossHealthHolder;

    public Image reloadBar, specialBar, dashBar, bossHealthBar;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerHealth.OnUpdateHealth += UpdateHealth;
        WeaponController.OnUpdateAmmo += UpdateAmmo;
        WeaponController.OnUpdateSpecial += UpdateSpecial;
        PlayerController.OnUpdateDash += UpdateDash;
        WeaponController.OnUpdateReload += UpdateReload;
        BossHandler.OnUpdateBossHealth += UpdateBossHealth;
        GoldManager.OnUpdateGold += UpdateGoldCounter;
        dashBar.enabled = false;
        reloadBar.enabled = false;
        specialBar.enabled = false;
        bossHealthHolder.SetActive(false);
    }



    private void OnDestroy()
    {
        PlayerHealth.OnUpdateHealth -= UpdateHealth;
        WeaponController.OnUpdateAmmo -= UpdateAmmo;
        WeaponController.OnUpdateSpecial -= UpdateSpecial;
        PlayerController.OnUpdateDash -= UpdateDash;
        WeaponController.OnUpdateReload -= UpdateReload;
        BossHandler.OnUpdateBossHealth -= UpdateBossHealth;
        GoldManager.OnUpdateGold -= UpdateGoldCounter;
    }



    private void UpdateHealth()
    {
        var healthBreakdown = UIManager._.healthBreakdown;
        health.text = "" + healthBreakdown[0] + " / " + healthBreakdown[1] + " | " + healthBreakdown[2] + " " + healthBreakdown[3];
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

    private void UpdateBossHealth(float[] healthAmount, string name)
    {
        if (!bossHealthHolder.activeInHierarchy)
        {
            bossHealthHolder.SetActive(true);
        }
        var healthBreakdown = healthAmount[0] / healthAmount[1];
        ConvertToBar(healthBreakdown, bossHealthBar, false);
        bossHealthName.text = name;
        if (healthBreakdown <= 0) bossHealthHolder.SetActive(false);


    }

    private void UpdateGoldCounter()
    {
        if (GameManager._.goldManager == null) return;
        if (SceneManager.GetActiveScene().buildIndex == (int)Scenes.Hub)
            goldCounter.text = "" + PlayerPrefs.GetInt("PermGold", 0)+"g";
        else
            goldCounter.text = "" + GameManager._.goldManager.gold + "g";
    }


    private void StandardPercentage(float input, TextMeshProUGUI label)
    {
        var breakdown = input;
        if (breakdown == 1 || breakdown == 0)
        {
            label.text = "";
            return;
        }
        string asPercent = PsychoticBox.ConvertToPercent(input);
        label.text = asPercent;
    }


    private void ConvertToBar(float input, Image bar, bool hideWhenMax = true)
    {
        if ((input == 1 && hideWhenMax) || (input == 0 && hideWhenMax))
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
