using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummonPlayer : MonoBehaviour
{
    public Transform spawnPosition;
    public SetDoors spawnRoomDoors;

    public PlayerMaster holding;
    // Start is called before the first frame update
    void Start()
    {
        Summon();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        GameManager._.Master = holding;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager._.CheckMasterError())
        {
            Debug.Log("Attempted to set master again");
            GameManager._.Master = holding;
        }
        else if(!once)
        {
            spawnRoomDoors.OpenDoors();
            StartCoroutine("GiveWeapon");
        }

        Debug.Log("Im alive");
    }


    public void Summon()
    {
        var temp = Instantiate(GameManager._.playerPrefab);
        holding = temp.transform.GetChild(0).GetComponent<PlayerMaster>();
        holding.movementMaster.rb.Move(spawnPosition.position, Quaternion.identity);
    }


    private bool once = false;
    private IEnumerator GiveWeapon()
    {
        if (once) yield break;
        once = true;
        GameManager._.Master.itemMaster.RefreshEffects();
        foreach (WeaponStorage weapon in GameManager._.weaponStorage)
        {

            var currentWep = Instantiate(weapon.weaponPrefab);
            Debug.Log("I spawned 1 weapon");
            var currentCore = currentWep.GetComponent<WeaponCore>();
            yield return new WaitForSeconds(0.1f);
            currentCore.pickUpWeapon();
            currentCore.specialTime = weapon.specialRemaining + Time.time;
        }
        Destroy(this);
        
    }

}
