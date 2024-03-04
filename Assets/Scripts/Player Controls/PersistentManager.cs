using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public int healingCharges = 0;
    public bool canRecycle;

    // Start is called before the first frame update
    void Start()
    {
        GetPersVars();
    }


    private void GetPersVars()
    {
        healingCharges = PlayerPrefs.GetInt("VK_Shop_HealingCharge",0);

        int recycleState = PlayerPrefs.GetInt("VK_Shop_Recycle", 0);
        canRecycle = recycleState != 0;
    }

}
