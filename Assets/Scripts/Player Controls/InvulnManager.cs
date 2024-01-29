using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulnManager : MonoBehaviour
{
    public bool dashInvuln => GameManager._.Master.dashing;


    public bool isInvuln()
    {
        return (dashInvuln | shovelInvuln);
    }


    public void Update()
    {
        TimeOutShovel();
    }



    #region shovel
    public bool shovelInvuln;
    public float shovelEndTime = 0;
    public int shovelAcculm;
    public void SetShovel(bool state, float duration)
    {
        shovelInvuln = state;
        if (state)
        {
            shovelEndTime = Time.time + duration;

        }
        else
        {
            shovelEndTime = Time.time - 1;
        }

    }
    private void TimeOutShovel()
    {

        if (Time.time > shovelEndTime && shovelInvuln)
        {
            shovelInvuln = false;
            UIManager._.WriteToNotification("Shovel Accumulated <b>" + shovelAcculm + "</b> damage");
        }

        if (shovelInvuln)
        {
            shovelAcculm += GameManager._.Master.healthMaster.lastDamageInstance;
            GameManager._.Master.healthMaster.lastDamageInstance = 0;
        }
    }



    #endregion

}
