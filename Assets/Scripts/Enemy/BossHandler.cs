using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{
    public EnemyComponentMaster master;
    public float[] healthBreakdown => new float[] { master.enemyHealth.health, master.card.health };


    public delegate void UpdateBossHealth(float[] healthBreakdown, string name);
    public static event UpdateBossHealth OnUpdateBossHealth;
    public void OnBossHealthChangeEvent()
    {

        if (OnUpdateBossHealth != null)
            OnUpdateBossHealth(healthBreakdown, master.card.enemyName);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        OnBossHealthChangeEvent();
    }


}
