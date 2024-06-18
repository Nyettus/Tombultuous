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




}
