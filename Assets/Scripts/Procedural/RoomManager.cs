using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UsefulBox;
using System.Threading.Tasks;
using System.Threading;


public class RoomManager : Singleton<RoomManager>
{
    public float roomSize = 50;
    [SerializeField]
    public RoomGridder RG;
    [SerializeField]
    private int RoomCount = 10;
    private int EmergencyStop;
    public GameObject[] PrefabToSpawn;
    public TileSet TileSets;
    public GameObject aestheticDoor;
    private List<GameObject> allRooms = new List<GameObject>();

    public Vector3 worldMidpoint;
    private CancellationTokenSource cancel;


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





    private async void Generation()
    {
        cancel = new CancellationTokenSource();
        await SetRooms(cancel.Token);
        RG.SetTreasureRooms();
        RG.CreateBossRoom(TileSets);
        //SpawnTestRooms();
        await SpawnRooms(cancel.Token);
        OpenDoors();
        worldMidpoint = BigMapMidPoint();

    }

    private async Task SetRooms(CancellationToken cancel)
    {
        while (RoomCount > 0 && EmergencyStop > 0 && !cancel.IsCancellationRequested)
        {
            RoomCount += RG.SetNextRoom();
            EmergencyStop -= 1;
            await Task.Yield();
        }
        if (EmergencyStop == 0)
        {
            Debug.Log("Emergency stop hit");
            ReloadScene();
            
        }
    }

    public void ReloadScene()
    {
        cancel?.Cancel();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

    }

    public void SpawnTestRooms()
    {
        foreach (RoomGrid room in RG.activeGrid)
        {
            if (room.state == RoomGrid.State.Occupied && room.type == RoomGrid.Type.Standard)
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

    public async Task SpawnRooms(CancellationToken cancel)
    {
        GameObject roomToSpawn = null;
        int roomIndex;
        foreach (RoomGrid room in RG.activeGrid)
        {
            if (cancel.IsCancellationRequested) return;
            if (room.state == RoomGrid.State.Occupied && room.type == RoomGrid.Type.Standard)
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
            else if (room.type == RoomGrid.Type.Treasure && room.shape == RoomGrid.Shape._1x1)
            {
                roomIndex = Random.Range(0, TileSets._TreasureRooms.Length);
                roomToSpawn = TileSets._TreasureRooms[roomIndex];
                SpawnAndRotate(roomToSpawn, room);
            }
            else if (room.type == RoomGrid.Type.Boss)
            {
                roomIndex = Random.Range(0, TileSets._BossRooms.Length);
                roomToSpawn = TileSets._BossRooms[roomIndex];
                SpawnAndRotate(roomToSpawn, room);
            }
            await Task.Yield();
        }
    }




    private void OpenDoors()
    {
        SummonPlayer playerSummon = GetComponent<SummonPlayer>();
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

        }
        playerSummon.OpenDoors();


    }
    public void SpawnAndRotate(GameObject room, RoomGrid position)
    {
        GameObject ParentRoom = Instantiate(room, position.worldPos, Quaternion.identity);
        ParentRoom.transform.Rotate(Vector3.up, position.cartesianPlane * -90f);
        var holding = ParentRoom.transform.GetChild(0).Find("CombatZone").GetComponent<CombatZone>();
        holding.thisRoom = position;
        allRooms.Add(ParentRoom);
    }

    public Vector3 BigMapMidPoint()
    {
        var FilteredGrid = RG.activeGrid.Where(room => room.state == RoomGrid.State.Occupied || room.state == RoomGrid.State.MultiGrid || room.state == RoomGrid.State.Forbidden);

        int maxX = FilteredGrid.Max(v => v.position.x);
        int minX = FilteredGrid.Min(v => v.position.x);

        int maxY = FilteredGrid.Max(v => v.position.y);
        int minY = FilteredGrid.Min(v => v.position.y);


        Vector2Int midPoint = new Vector2Int((maxX + minX) / 2, (maxY + minY) / 2);
        Vector3 convertToWorld = PsychoticBox.ConvertGridToWorldPos(midPoint, yOffset: 150);

        return convertToWorld;

    }


}
