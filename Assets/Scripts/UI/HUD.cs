using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI health,ammo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnUpdateHealth += UpdateHealth;
        WeaponController.OnUpdateAmmo += UpdateAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
