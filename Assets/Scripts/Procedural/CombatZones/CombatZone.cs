using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CombatZone : MonoBehaviour
{
    public GameObject sealedHost;
    public List<BoxCollider> sealedDoors = new List<BoxCollider>();
    public GameObject enemy;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject navmeshLinkHost;
    private bool activated = false;
    public RoomGrid thisRoom;

    [SerializeField]
    private GameObject UICanvas;
    [SerializeField]
    private List<GameObject> adjDoors;

    // Start is called before the first frame update
    void Start()
    {
        if (navmeshLinkHost != null) navmeshLinkHost.SetActive(false);
    }

    public delegate void RoomEntered(RoomGrid thisGrid);
    public static event RoomEntered OnRoomEnter;
    public void OnRoomEnterEvent()
    {
        if (OnRoomEnter != null)
            OnRoomEnter(thisRoom);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActivateCombatZone();
            RevealMap();
            OnRoomEnterEvent();
        }

    }

    private void ActivateCombatZone()
    {


        if (activated || enemies.Count == 0) return;
        Debug.Log("Room activated");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyCountHandler>().master = this;
            enemy.SetActive(true);
        }
        SetDoors(true);
        if (navmeshLinkHost != null) navmeshLinkHost.SetActive(true);

        activated = true;
    }

    private void SetDoors(bool state)
    {
        foreach (BoxCollider doors in sealedDoors)
        {
            doors.enabled = state;
        }
    }

    public void DisableCombatZones()
    {
        //Code to activate room clear
        SetDoors(false);
        if (navmeshLinkHost != null) navmeshLinkHost.SetActive(false);
        Debug.Log("Combat Zone Disabled");
        GameManager._.Master.itemMaster.onRoomClearHandler.OnRoomClear();
    }

    private void RevealMap()
    {
        if (UICanvas == null) return;
        UICanvas.SetActive(true);
        if (adjDoors.Count == 0) return;
        foreach (GameObject door in adjDoors)
        {
            door.SetActive(true);
        }
    }

    public void AssignEnemies()
    {
        EnemyExclusive();
        UIExclusive();

    }

    private void EnemyExclusive()
    {
        if (enemy == null) enemy = transform.parent.Find("--- Enemies ---").gameObject;
        if (enemy == null)
        {

            Debug.LogError("Couldn't find enemy folder");
            return;
        }
        if (enemies.Count == enemy.transform.childCount) return;

        enemies.Clear();
        foreach (Transform transform in enemy.transform)
        {
            enemies.Add(transform.gameObject);
            transform.gameObject.SetActive(false);
        }



    }
    private void UIExclusive()
    {
        UICanvas = transform.parent.parent.Find("---UI---").gameObject;
        if (UICanvas == null)
        {
            Debug.LogError("No UI canvas found");
            return;

        }
        UICanvas.SetActive(false);
    }



}
