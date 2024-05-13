using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCore : MonoBehaviour
{
    public bool shooting;
    protected ObjectPooler ObjectPool => ObjectPooler._;
    private Transform modelHolder;
    protected LayerMask layerMask = ~(1 << 6 | 1 << 2);

    [Header("Lore Traits")]
    public string weaponName;
    public string atkDesc;
    public string spkDesc;
    public string description;

    [Header("Special Traits")]
    protected float specialCooldown;
    public float specialTime;
    protected bool specialUsed=false;
    private float specialStartTime;
    [HideInInspector]
    public float specialPercentage;

    [Header("Inventory Traits")]
    public bool equipped = false;
    public GameObject prefab;
    

    protected virtual void Start()
    {
        modelHolder = transform.GetChild(1);
    }


    public virtual void Shoot()
    {
        GameManager._.Master.weaponMaster.OnAmmoChangeEvent();
    }

    public virtual void OnSwitch()
    {
        SpecialPercentage();
        GameManager._.Master.weaponMaster.OnAmmoChangeEvent();
        GameManager._.Master.weaponMaster.OnSpecialChangeEvent();
        GameManager._.Master.weaponMaster.OnReloadChangeEvent();
    }
    public virtual void OnMeleeHit(IEnemyDamageable damageScript,float additive = 0)
    {

    }


    public virtual void Special()
    {
        if (GameManager._.paused) return;
        specialUsed = true;
        specialTime = Time.time + specialCooldown;
        specialStartTime = Time.time;
        GameManager._.Master.weaponMaster.OnAmmoChangeEvent();
    }

    public virtual void Update()
    {

        if (specialUsed)
        {
            SpecialPercentage();
            GameManager._.Master.weaponMaster.OnSpecialChangeEvent();
        }

        if (specialTime < Time.time)
            specialUsed = false;

    }


    public void pickUpWeapon()
    {
        bool ticked = false;
        WeaponController master = GameManager._.Master.weaponMaster;
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
                LayerChange(gameObject, "Viewmodel");
                master.UpdateEquipped();
                master.selectedWeapon = i;
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
            LayerChange(replaced.gameObject, "Player");


            equipped = true;
            transform.SetParent(master.transform);
            transform.SetSiblingIndex(index);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            GetComponent<Collider>().enabled = false;
            LayerChange(gameObject, "Viewmodel");

            master.UpdateEquipped();
            master.SelectWeapon();

        }
        GameManager._.Master.weaponMaster.OnAmmoChangeEvent();

    }

    private void OnDisable()
    {
        shooting = false;
    }
    private void LayerChange(GameObject go, string layerName)
    {
        go.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in go.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(layerName);
            Transform hasChildren = child.GetComponentInChildren<Transform>();
            if (hasChildren != null)
                LayerChange(child.gameObject, layerName);
        }
    }

    private void SpecialPercentage()
    {
        var denominator = (specialTime - specialStartTime);
        var neumerator = (Time.time - specialStartTime);
        specialPercentage = Mathf.Clamp(neumerator / denominator, 0, 1);
    }

}
