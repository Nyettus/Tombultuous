using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountHandler : MonoBehaviour
{
    public CombatZone master;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RemoveFromMaster()
    {
        if (master == null)
        {
            Debug.LogError("Enemy has no host");
            return;
        }
        master.enemyCount--;
        if (master.enemyCount <= 0)
        {
            master.DisableCombatZoneAsInvoke();
        }
    }


}
