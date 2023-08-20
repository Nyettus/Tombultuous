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
    private int EmergencyStop = 20;
    public GameObject[] PrefabToSpawn;


    // Start is called before the first frame update
    void Awake()
    {
        Startup(this);
        RG = GetComponent<RoomGridder>();
    }

    private void Start()
    {
        EmergencyStop = RoomCount * 2;
        RG.InitialiseGrid();
        //RG.debugger();
        Generation();
    }



    // Update is called once per frame
    void FixedUpdate()
    {


        //if (Input.GetKeyDown("p"))
        //{
        //    Instantiate(PrefabToSpawn, new Vector3(50, 0, 0), Quaternion.identity);
        //    //SceneManager.LoadScene(2, LoadSceneMode.Additive);

        //}
    }

    private void Generation()
    {

        while (RoomCount > 0 && EmergencyStop > 0)
        {
            RoomCount += RG.SetNextRoom();
            EmergencyStop -= 1;
        }
        if (EmergencyStop == 0)
        {
            Debug.Log("Emergency stop hit");
        }
        SpawnTestRooms();


    }

    public void SpawnTestRooms()
    {
        foreach (RoomGrid room in RG.activeGrid)
        {
            if (room.state == RoomGrid.State.occupied)
            {
                if (room.shape == RoomGrid.Shape._1x1)
                    SpawnAndRotate(PrefabToSpawn[0], room);
                else if (room.shape == RoomGrid.Shape._2x2)
                    SpawnAndRotate(PrefabToSpawn[1], room);
            }
        }
    }

    public void SpawnAndRotate(GameObject room, RoomGrid position)
    {
        GameObject ParentRoom = Instantiate(room, position.worldPos, Quaternion.identity);
        ParentRoom.transform.Rotate(Vector3.up, position.cartesianPlane * -90f);
    }

}
