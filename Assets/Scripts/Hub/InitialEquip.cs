using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialEquip : MonoBehaviour
{
    public ChangePlayer vessel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._.CheckMasterError()) return;
        vessel.EquipNewCharacter();
        Destroy(this);
        
    }
}
