using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomManager : Singleton<RoomManager>
{
    public float roomSize = 50;
    [SerializeField]
    private RoomGridder RG;
    [SerializeField]
    private int RoomCount = 10;
    private int EmergencyStop = 100;
    private bool once = false;


    // Start is called before the first frame update
    void Awake()
    {
        Startup(this);
        RG = GetComponent<RoomGridder>();
    }

    private void Start()
    {
        //EmergencyStop = RoomCount * 2;
        RG.InitialiseGrid();
        Generation();
    }


    public GameObject PrefabToSpawn;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (RoomCount > 0 && EmergencyStop > 0)
        {
            RoomCount += RG.SetNextRoom();
            EmergencyStop -= 1;
        }
        else if (EmergencyStop == 0 || RoomCount ==0)
        {
            once = true;
            EmergencyStop = -1;
            RoomCount = -1;
        }

        if (once)
        {

            RG.SpawnTestRooms();
            once = false;
        }
   

        if (Input.GetKeyDown("p"))
        {
            Instantiate(PrefabToSpawn, new Vector3(50,0,0), Quaternion.identity);
            //SceneManager.LoadScene(2, LoadSceneMode.Additive);
            
        }
    }

    private void Generation()
    {

    }

    public void SpawnTestRooms(RoomGrid position)
    {
        if(position.state==RoomGrid.State.occupied)
        Instantiate(PrefabToSpawn, position.worldPos, Quaternion.identity);
    }

}
