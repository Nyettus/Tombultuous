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
        SpawnTestRooms();
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
    private void OpenDoors()
    {
        foreach (GameObject room in allRooms)
        {
            if (room.TryGetComponent<SetDoors>(out SetDoors script))
            {
                script.OpenDoors();
            }
        }
    }
    public void SpawnAndRotate(GameObject room, RoomGrid position)
    {
        GameObject ParentRoom = Instantiate(room, position.worldPos, Quaternion.identity);
        ParentRoom.transform.Rotate(Vector3.up, position.cartesianPlane * -90f);
        allRooms.Add(ParentRoom);
    }

}
