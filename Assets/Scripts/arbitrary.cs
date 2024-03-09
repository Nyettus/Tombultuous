using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitrary : SingletonPersist<Arbitrary>
{
    private void Awake()
    {
        Startup(this);
    }

    private void Update()
    {
# if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveManager._.DeleteSaveData();
        }
# endif 
    }
}
