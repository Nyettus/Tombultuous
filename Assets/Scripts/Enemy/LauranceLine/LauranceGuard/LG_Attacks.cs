using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LG_Attacks : BaseEnemyAttacks
{
    EnemyComponentMaster CM;
    private void Start()
    {
        CM = GetComponent<EnemyComponentMaster>();
    }
    public void LG_Charge(int state)
    {
        bool asBool = state != 0;
        CM.SetAnimBool("DecisionBool", asBool);
    }



}
