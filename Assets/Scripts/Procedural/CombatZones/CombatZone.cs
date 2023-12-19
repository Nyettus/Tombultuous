using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    public GameObject sealedHost;
    public List<BoxCollider> sealedDoors = new List<BoxCollider>();
    public List<GameObject> enemies = new List<GameObject>();
    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ActivateCombatZone();
        }
    }

    private void ActivateCombatZone()
    {
        if (activated) return;
        Debug.Log("Room activated");
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyCountHandler>().master = this;
            enemy.SetActive(true);
        }
        SetDoors(true);


        activated = true;
    }

    private void SetDoors(bool state)
    {
        foreach(BoxCollider doors in sealedDoors)
        {
            doors.enabled = state;
        }
    }

    public void DisableCombatZones()
    {
        //Code to activate room clear
        SetDoors(false);
        Debug.Log("Combat Zone Disabled");
    }

    private List<BoxCollider> GetChildren()
    {
        var holding = new List<BoxCollider>();
        foreach (Transform child in transform)
        {
            holding.Add(child.gameObject.GetComponent<BoxCollider>());
        }
        return holding;

    }

}
