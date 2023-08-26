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
    public TileSet TileSets;
    public GameObject aestheticDoor;
    private List<GameObject> allRooms = new List<GameObject>();


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
        Generation();
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
            ReloadScene();

        }
        RG.DetectEndRooms();
        //SpawnTestRooms();
        SpawnRooms();
        Invoke("OpenDoors", 0.1f);

    }

    private void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

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
                else if (room.shape == RoomGrid.Shape._1x2)
                    SpawnAndRotate(PrefabToSpawn[2], room);
            }
        }
    }

    public void SpawnRooms()
    {
        GameObject roomToSpawn = null;
        int roomIndex;
        foreach (RoomGrid room in RG.activeGrid)
        {
            if (room.state == RoomGrid.State.occupied)
            {
                if (room.shape == RoomGrid.Shape._1x1)
                {
                    roomIndex = Random.Range(0, TileSets._1x1.Length);
                    roomToSpawn = TileSets._1x1[roomIndex];
                    SpawnAndRotate(roomToSpawn, room);
                }

                else if (room.shape == RoomGrid.Shape._2x2)
                {
                    roomIndex = Random.Range(0, TileSets._2x2.Length);
                    roomToSpawn = TileSets._2x2[roomIndex];
                    SpawnAndRotate(roomToSpawn, room);
                }

                else if (room.shape == RoomGrid.Shape._1x2)
                {
                    roomIndex = Random.Range(0, TileSets._1x2.Length);
                    roomToSpawn = TileSets._1x2[roomIndex];
                    SpawnAndRotate(roomToSpawn, room);
                }

            }
        }
    }
    private void OpenDoors()
    {

        foreach (GameObject room in allRooms)
        {
            if (room.TryGetComponent<SetDoors>(out SetDoors script))
            {
                script.OpenDoors();
            }
        }
        List<RoomGrid[]> doorLocal = RG.AestheticDoorsLocation();
        List<Vector3> alreadyPositioned = new List<Vector3>();
        foreach (RoomGrid[] location in doorLocal)
        {
            Vector3 midpoint = (location[0].worldPos + location[1].worldPos) / 2;
            if (alreadyPositioned.Find(v3 => v3 == midpoint) == Vector3.zero)
            {
                alreadyPositioned.Add(midpoint);
                Vector3 direction = (location[1].worldPos - location[0].worldPos).normalized;
                GameObject holding = Instantiate(aestheticDoor, midpoint, Quaternion.identity);
                holding.transform.rotation = Quaternion.LookRotation(direction);
            }
            else
                Debug.Log("successful Skip");

        }


    }
    public void SpawnAndRotate(GameObject room, RoomGrid position)
    {
        GameObject ParentRoom = Instantiate(room, position.worldPos, Quaternion.identity);
        ParentRoom.transform.Rotate(Vector3.up, position.cartesianPlane * -90f);
        allRooms.Add(ParentRoom);
    }

}
