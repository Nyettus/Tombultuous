using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{

    public PlayableCharacter card => GameManager._.playerCard;
    [Header("Lore")]
    public string characterName;
    public string description;

    [Header("Stat Additions")]
    public ItemMaster itemMaster;

    [Header("Combat Variables (Base)")]
    public WeaponController weaponMaster;
    public PlayerHealth healthMaster;
    public int health;
    public float damage;
    public float haste;
    public int pockets;

    [Header("Movement Variables (Base)")]
    public PlayerController movementMaster;
    public float moveSpeed;
    public float airAccel;
    public float jumpPower;
    public int jumpCount;
    public float dashCooldown;
    public float dashSpeed;

    [Header("Passthrough Logic (Base)")]
    public bool grounded;
    public bool dashing;


    [Header("Effects")]
    public CameraEffects cameraEffects;



    // Start is called before the first frame update
    void Awake()
    {
        Establish();
        //ReturnPlayer();
    }
    private void Start()
    {
        GameManager._.lockMouse();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Get the variables out of the scriptable objects
    public void Establish()
    {
        movementMaster = GetComponent<PlayerController>();
        weaponMaster = GetComponentInChildren<WeaponController>();
        itemMaster = GetComponent<ItemMaster>();
        EstablishCard();

    }

    public void EstablishCard()
    {

        //Name text
        characterName = card.characterName;
        description = card.description;

        //Combat var
        damage = card.damage;
        health = card.health;
        haste = card.haste;
        pockets = card.pockets;

        //Movement var
        moveSpeed = card.moveSpeed;
        airAccel = card.airAccel;
        jumpPower = card.jumpPower;
        jumpCount = card.jumpCount;
        dashCooldown = card.dashCooldown;
        dashSpeed = card.dashSpeed;
    }



}
