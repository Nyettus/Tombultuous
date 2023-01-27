using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    public WeaponCore master;
    // Start is called before the first frame update
    void Start()
    {
        master = GetComponentInParent<WeaponCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!master.equipped)
            transform.Rotate(new Vector3(0, 1, 0));
    }
}
