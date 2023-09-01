using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayableCharacter : ScriptableObject
{
    //Name and desc
    [Header("Name")]
    public string characterName;
    public string description;

    //Starting equipment
    [Header("Starting Gear")]
    public ItemBase[] startingItems;
    public WeaponBase[] startingWeapons;


    //Combat Variables
    [Header("Combat")]
    public int health;
    public float damage;
    public float haste;
    public int pockets;

    //Neutral Movement Variables
    [Header("Neutral Movement")]
    public float airAccel;
    public float moveSpeed;

    //Jump Variables
    [Header("Jump")]
    public float jumpPower;
    public int jumpCount;

    //Dash Variables
    [Header("Dash")]
    public float dashCooldown;
    public float dashSpeed;
}
