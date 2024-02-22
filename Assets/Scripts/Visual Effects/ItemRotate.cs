using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    public WeaponCore master;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.TryGetComponent<WeaponCore>(out WeaponCore temp))
        {
            master = temp;
        }
        if(TryGetComponent<Animator>(out Animator animator))
        {
            anim = animator;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (master == null || !master.equipped)
        {
            transform.Rotate(new Vector3(0, 100*Time.deltaTime, 0));
        }
        if(anim!= null && master!=null)
        {
            if (anim.enabled != master.equipped)
                anim.enabled = master.equipped;
        }

    }
}
