using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialEquip : MonoBehaviour
{
    public ChangePlayer vessel;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._.CheckMasterError()) return;
        else if (once)
        {
            vessel.EquipNewCharacter(true);
            once = false;
            Destroy(this,10);
        }

        
    }
}
