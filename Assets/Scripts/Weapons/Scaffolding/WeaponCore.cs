using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCore : MonoBehaviour
{
    public bool shooting;
    protected ObjectPooler ObjectPool;
    private Transform modelHolder;

    [Header("Lore Traits")]
    public string weaponName;
    public string atkDesc;
    public string spkDesc;
    public string description;

    [Header("Special Traits")]
    protected float specialCooldown;
    protected float specialTime;
    protected bool specialUsed=false;

    [Header("Inventory Traits")]
    public bool equipped = false;

    protected virtual void Start()
    {
        ObjectPool = ObjectPooler.Instance;
        modelHolder = transform.GetChild(1);
    }

    public virtual void Shoot()
    {

    }
    public virtual void Special()
    {
        specialUsed = true;
        specialTime = Time.time + specialCooldown;
    }

    public virtual void Update()
    {
        if (specialTime < Time.time)
            specialUsed = false;
    }


    public void pickUpWeapon()
    {
        bool ticked = false;
        WeaponController master = GameManager.instance.Master.weaponMaster;
        for(int i = 0; i < master.equippedGuns.Length; i++)
        {
            if (master.equippedGuns[i] == null)
            {
                equipped = true;
                transform.SetParent(master.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                modelHolder.localRotation = Quaternion.identity;
                GetComponent<Collider>().enabled = false;
                master.updateEquipped();
                master.SelectWeapon();
                ticked = true;
                Debug.Log("Normal Pickup");
                break;
            }
        }
        if (!ticked)
        {
            int index = master.selectedWeapon;
            GameObject replaced = master.equippedGuns[index].gameObject;

 
            replaced.transform.SetParent(null);
            replaced.transform.position = transform.position;
            replaced.transform.rotation = transform.rotation;
            modelHolder.localRotation = Quaternion.identity;
            replaced.GetComponent<Collider>().enabled = true;
            replaced.GetComponent<WeaponCore>().equipped = false;


            equipped = true;
            transform.SetParent(master.transform);
            transform.SetSiblingIndex(index);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            GetComponent<Collider>().enabled = false;

            master.updateEquipped();
            master.SelectWeapon();

        }

    }


}
