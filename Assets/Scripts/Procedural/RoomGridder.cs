using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class RoomGrid
{
    public float roomSize;
    public Vector2Int position;
    public Vector3 worldPos;
    public State state;
    public Shape shape;
    public int cartesianPlane = 0;

    public enum State { available, forbidden, occupied, unlikely };
    public enum Shape { _1x1, _2x2, _L, _3x3 };

    public RoomGrid(Vector2Int gridPosition, State state)
    {
        position = gridPosition;
        this.state = state;
        roomSize = RoomManager._.roomSize;
        worldPos = new Vector3(gridPosition.x * roomSize, 0, gridPosition.y * roomSize);
    }
}
public class RoomGridder : MonoBehaviour
{
    [SerializeField]
    public List<RoomGrid> activeGrid = new List<RoomGrid>();

    Vector2Int[] cartesian = new Vector2Int[]
{
            new Vector2Int(0,1),    //North
            new Vector2Int(0,-1),    //South
            new Vector2Int(1,0),    //East
            new Vector2Int(-1,0)     //West
};

    private void Start()
    {
        InitialiseGrid();

    }

    public void InitialiseGrid()
    {
        Vector2Int start = new Vector2Int(0, 0);

        activeGrid.Add(new RoomGrid(start, RoomGrid.State.forbidden));
        CreateAdjacent(start);
        SpawnTestRooms();
    }


    public void CreateAdjacent(Vector2Int start)
    {

        foreach (Vector2Int direction in cartesian)
        {
            Vector2Int spawnPosition = start + direction;
            RoomGrid existingRoom = activeGrid.Find(room => room.position == spawnPosition);

            if (existingRoom == null)
            {
                activeGrid.Add(new RoomGrid(spawnPosition, RoomGrid.State.available));
            }
            else if (existingRoom.state == RoomGrid.State.available)
            {
                existingRoom.state = RoomGrid.State.unlikely;
            }


        }
    }

    private RoomGrid CreateAtPosition(Vector2Int position)
    {
        RoomGrid existingRoom = activeGrid.Find(room => room.position == position);
        if (existingRoom == null)
        {
            RoomGrid returnRoom = new RoomGrid(position, RoomGrid.State.available);
            activeGrid.Add(returnRoom);
            return returnRoom;
        }
        else
            return existingRoom;
    }
    private List<Vector2Int> GridPoints(Vector2Int start, Vector2Int furthest)
    {
        List<Vector2Int> AllPoints = new List<Vector2Int>();
        int minX = Mathf.Min(start.x, furthest.x);
        int maxX = Mathf.Max(start.x, furthest.x);
        int minZ = Mathf.Min(start.y, furthest.y);
        int maxZ = Mathf.Max(start.y, furthest.y);
        for (int x = minX; x <= maxX; x++)
        {
            for (int z = minZ; x <= maxZ; z++)
            {
                AllPoints.Add(new Vector2Int(x, z));
            }
        }
        return AllPoints;
    }
    private bool CheckGrid(Vector2Int start, Vector2Int furthest)
    {
        List<Vector2Int> AllPoints = GridPoints(start, furthest);
        RoomGrid tested;
        foreach (Vector2Int point in AllPoints)
        {
            tested = CreateAtPosition(point);
            if (!(tested.state == RoomGrid.State.available || tested.state == RoomGrid.State.unlikely))
                return false;
        }
        return true;
    }
    private void ReservePoints(Vector2Int start, Vector2Int furthest,RoomGrid.Shape shape)
    {
        RoomGrid changedOne;
        List<Vector2Int> AllPoints = GridPoints(start, furthest);
        foreach (Vector2Int point in AllPoints)
        {
            changedOne = activeGrid.Find(room => room.position == point);
            changedOne.state = RoomGrid.State.forbidden;
        }
        changedOne = activeGrid.Find(room => room.position == start);
        changedOne.state = RoomGrid.State.occupied;
        changedOne.shape = shape;
    }


    public void SpawnTestRooms()
    {
        foreach (RoomGrid room in activeGrid)
        {
            RoomManager._.SpawnTestRooms(room);
        }
    }


    public int SetNextRoom()
    {
        List<RoomGrid> availableRooms = activeGrid.Where(room => room.state == RoomGrid.State.available || room.state == RoomGrid.State.unlikely).ToList();

        int RandomPos = Random.Range(0, availableRooms.Count - 1);
        RoomGrid randomRoom = availableRooms[RandomPos];
        RoomGrid roomToUpdate = activeGrid.Find(room => room.position == randomRoom.position);
        if (roomToUpdate != null)
        {
            if (randomRoom.state == RoomGrid.State.available)
            {

                return SelectRoom(roomToUpdate);
            }
            else if (randomRoom.state == RoomGrid.State.unlikely && Random.value <= 1f)
            {
                Debug.Log("Unlikely Room hit");
                return SelectRoom(roomToUpdate);
            }
            else
            {
                Debug.Log("Unlikely Room missed");
                return 0;
            }
        }
        else
        {
            Debug.LogError("No Rooms to add");
            return 0;
        }
    }

    #region Room Shape Checks and sets
    private int SelectRoom(RoomGrid position)
    {
        //Simple for debugging
        float random = Random.value;
        if (random >= 1)
            return Set_1x1(position);
        else
            return Set_2x2(position);
    }



    private int Set_1x1(RoomGrid room)
    {
        Debug.Log("Tried 1x1");
        room.state = RoomGrid.State.occupied;

        room.shape = RoomGrid.Shape._1x1;
        //Room rotation (on a cartesian)
        room.cartesianPlane = Random.Range(0, 3);
        CreateAdjacent(room.position);
        return -1;
    }

    private int Set_2x2(RoomGrid checkRoom)
    {
        Debug.Log("Tried 2x2");


        //Quadrants like in math, Z up X right
        bool[] quadrants = { true, true, true, true };
        Vector2Int[] furthestPoints =
        {
            checkRoom.position+cartesian[0]+cartesian[2],
            checkRoom.position+cartesian[0]+cartesian[3],
            checkRoom.position+cartesian[1]+cartesian[3],
            checkRoom.position+cartesian[1]+cartesian[2]
        };

        List<int> trueIndecies = new List<int>();
        for(int i=0; i < quadrants.Length; i++)
        {
            quadrants[i] = CheckGrid(checkRoom.position, furthestPoints[i]);
            if (quadrants[i]) trueIndecies.Add(i);
        }

        if (trueIndecies.Count == 0)
        {
            Debug.Log("2x2 cannot fit");
            return 0;
        }
        int randomIndex = trueIndecies[Random.Range(0, trueIndecies.Count)];
        checkRoom.cartesianPlane = randomIndex;
        ReservePoints(checkRoom.position, furthestPoints[randomIndex], RoomGrid.Shape._2x2);
        return -1;
        




    }

    #endregion

    public void debugger()
    {
        List<RoomGrid> availableRooms = activeGrid.Where(room => room.state == RoomGrid.State.available).ToList();
        Debug.Log(availableRooms.Count);
        foreach (RoomGrid room in availableRooms)
        {

            Debug.Log("" + room.state + " | " + room.position);
        }
    }

}
