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
        master.enemies.Remove(this.gameObject);
        if (master.enemies.Count <= 0)
        {
            Invoke("DisableCombat", 0.1f);
        }
    }

    private void DisableCombat()
    {
        master.DisableCombatZones();
    }
}
