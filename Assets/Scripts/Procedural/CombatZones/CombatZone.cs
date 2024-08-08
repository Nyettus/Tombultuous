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

    public GameObject lightHost;
    [SerializeField] private Light[] RTLights;

    private BossRoom bossRoom;

    [SerializeField]
    private GameObject UICanvas;
    [SerializeField]
    private List<GameObject> adjDoors;

    // Start is called before the first frame update
    void Start()
    {
        if (navmeshLinkHost != null) navmeshLinkHost.SetActive(false);
        if (TryGetComponent<BossRoom>(out BossRoom BR))
        {
            bossRoom = BR;
        }
    }

    public delegate void RoomEntered(RoomGrid thisGrid);
    public static event RoomEntered OnRoomEnter;
    public void OnRoomEnterEvent()
    {
        if (OnRoomEnter != null)
            OnRoomEnter(thisRoom);
    }

    public delegate void CombatStarted(RoomGrid thisGrid);
    public static event CombatStarted OnCombatStarted;
    public void OnCombatStartedEvent()
    {
        if (OnCombatStarted != null)
            OnCombatStarted(thisRoom);
    }

    public delegate void CombatEnded(RoomGrid thisGrid);
    public static event CombatEnded OnCombatEnded;
    public void OnCombatEndedEvent()
    {
        if (OnCombatEnded != null)
            OnCombatEnded(thisRoom);
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
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SetLights(false);
        }
    }

    private void ActivateCombatZone()
    {
        SetLights(true);

        if (activated || enemies.Count == 0) return;
        Debug.Log("Room activated");
        OnCombatStartedEvent();
        SetDoors(true);
        SetEnemies(true);

        if (navmeshLinkHost != null) navmeshLinkHost.SetActive(true);

        activated = true;
    }

    public void DisableCombatZones()
    {
        //Code to activate room clear
        SetDoors(false);
        OnCombatEndedEvent();
        if (navmeshLinkHost != null) navmeshLinkHost.SetActive(false);
        Debug.Log("Combat Zone Disabled");

        GameManager._.Master.itemMaster.onRoomClearHandler.OnRoomClear();
        if (bossRoom != null) bossRoom.OnBossKill();
    }
    public void DisableCombatZoneAsInvoke()
    {
        Invoke("DisableCombatZones", 0.1f);
    }

    private void SetDoors(bool state)
    {
        foreach (BoxCollider doors in sealedDoors)
        {
            doors.enabled = state;
        }
    }

    private void SetLights(bool state)
    {
        foreach (Light lights in RTLights)
        {
            lights.enabled = state;
        }
    }


    private void SetEnemies(bool state)
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyCountHandler>().master = this;
            enemy.SetActive(state);
        }
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


    #region Editor Methods
    public void AssignEnemies()
    {
        UIExclusive();
        LightExclusive();
        EnemyExclusive();
    }



    private void EnemyExclusive()
    {
        if (enemy == null)
        {
            var cachedTransform = transform.parent.Find("--- Enemies ---");
            if (cachedTransform != null)
                enemy = cachedTransform.gameObject;
            else
                enemy = null;
        }
        if (enemy == null)
        {

            Debug.LogError("Couldn't find enemy folder");
            return;
        }

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

    private void LightExclusive()
    {
        if (lightHost == null) lightHost = transform.parent.parent.Find("---RTLight---").gameObject;
        int allLayer = ~0;
        int exludeLayer = ~(1 << 3 | 1 << 15);
        LayerMask finalMask = allLayer & exludeLayer;
        RTLights = lightHost.GetComponentsInChildren<Light>();
        foreach (Light light in RTLights)
        {
            light.cullingMask = finalMask;
        }
        SetLights(false);
    }


    #endregion
}
