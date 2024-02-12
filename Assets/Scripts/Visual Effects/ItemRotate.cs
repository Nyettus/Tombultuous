using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    public WeaponCore master;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.TryGetComponent<WeaponCore>(out WeaponCore temp))
        {
            master = temp;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (master == null || !master.equipped)
            transform.Rotate(new Vector3(0, 100*Time.deltaTime, 0));
    }
}
