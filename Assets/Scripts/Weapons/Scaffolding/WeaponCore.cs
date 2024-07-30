using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCore : MonoBehaviour
{
    [Header("Card")]
    public WeaponBase card;

    public bool shooting;
    protected ObjectPooler ObjectPool => ObjectPooler._;
    private Transform modelHolder;
    protected LayerMask layerMask = ~(1 << 6 | 1 << 2);
    [SerializeField] protected Animator anim;

    [Header("Lore Traits")]
    public string weaponName;
    public string atkDesc;
    public string spkDesc;
    public string description;

    [Header("Special Traits")]
    protected float specialCooldown;
    public float specialTime;
    protected bool specialUsed = false;
    private float specialStartTime;
    [HideInInspector]
    public float specialPercentage;

    [Header("Inventory Traits")]
    public bool equipped = false;
    public GameObject prefab;
    public Sprite smallThumbnail;
    public Sprite largeThumbnail;


    protected virtual void Start()
    {
        modelHolder = transform.GetChild(1);
        Establish();
    }

    protected virtual void Establish()
    {
        prefab = card.prefab;
        weaponName = card.weaponName;
        atkDesc = card.normalDescription;
        spkDesc = card.specialDescription;
        description = card.lore;
        smallThumbnail = card.smallThumbnail;
        largeThumbnail = card.largeThumbnail;
        specialCooldown = card.cooldown;
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
    protected virtual void OnDisable()
    {
        shooting = false;
        if (anim != null)
        {
            anim.StopPlayback();
        }

    }
    public virtual void OnMeleeHit(IEnemyDamageable damageScript, float additive = 0)
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
        for (int i = 0; i < master.equippedGuns.Length; i++)
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

    public void OnRecycle()
    {
        int scrapAmount = Random.Range(1, 2);
        int goldAmount = Random.Range(50, 100);
        var holding = GameManager._.goldManager;
        holding.GetScrap(scrapAmount);
        holding.GetGold(goldAmount);
        Destroy(this.gameObject);
    }


    protected void SetAnimTrigger(string input,bool state = true)
    {
        if (anim == null) return;
        if (state)
            anim.SetTrigger(input);
        else
            anim.ResetTrigger(input);
    }

    protected void SetAnimInt(string input, int value)
    {
        if (anim == null) return;
        anim.SetInteger(input, value);
    }
    protected void SetAnimBool(string input, bool state)
    {
        if (anim == null) return;
        anim.SetBool(input, state);
    }

}
