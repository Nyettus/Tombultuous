using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class RoomGrid
{
    public float roomSize;
    public Vector2Int position;
    public Vector3 worldPos;
    public State state;
    public Type type;
    public Shape shape;
    public int cartesianPlane = 0;
    public int roomID = -1;
    public bool deadEnd = false;

    public enum Type { Standard, Boss, Treasure };
    public enum State { available, forbidden, occupied, unlikely, empty, multiGrid };
    public enum Shape { _1x1, _2x2, _1x2, _3x3 };

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
    private int multiGridID = 0;
    public List<RoomGrid> activeGrid = new List<RoomGrid>();
    private Vector2Int bossSpawnPosition;
    private Vector2Int[] cartesian = new Vector2Int[]
    {
            new Vector2Int(-1,0),   //West
            new Vector2Int(0,-1),    //South
            new Vector2Int(1,0),    //East
            new Vector2Int(0,1),    //North

    };

    private void Start()
    {
        InitialiseGrid();

    }

    #region Neutral Room Creators
    public void InitialiseGrid()
    {
        Vector2Int start = new Vector2Int(0, 0);
        RoomGrid spawnRoom = new RoomGrid(start, RoomGrid.State.multiGrid);
        activeGrid.Add(spawnRoom);

        CreateAdjacent(start);
        //SpawnTestRooms();
    }


    private void CreateAdjacent(Vector2Int start)
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


    private RoomGrid CreateAtPosition(Vector2Int position, RoomGrid.State state)
    {
        RoomGrid existingRoom = activeGrid.Find(room => room.position == position);
        if (existingRoom == null)
        {
            RoomGrid returnRoom = new RoomGrid(position, state);
            activeGrid.Add(returnRoom);
            return returnRoom;
        }
        else
        {
            return existingRoom;
        }

    }
    #endregion

    #region Multi Grid Room Logic
    private Vector2Int[] CartesianBounds(Vector2Int position, Vector2Int size)
    {
        Vector2Int[] furthestPoints =
        {
            position+cartesian[3]*size.y+cartesian[2]*size.x,
            position+cartesian[3]*size.x+cartesian[0]*size.y,
            position+cartesian[1]*size.y+cartesian[0]*size.x,
            position+cartesian[1]*size.x+cartesian[2]*size.y
        };
        return furthestPoints;
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
            for (int z = minZ; z <= maxZ; z++)
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
            tested = CreateAtPosition(point, RoomGrid.State.empty);
            if (!(tested.state == RoomGrid.State.available || tested.state == RoomGrid.State.unlikely || tested.state == RoomGrid.State.empty))
            {
                return false;
            }

        }
        return true;
    }

    private void ReservePoints(Vector2Int start, Vector2Int furthest, RoomGrid.Shape shape, RoomGrid.State multiState, RoomGrid.State startState = RoomGrid.State.occupied)
    {
        RoomGrid changedOne;
        List<Vector2Int> AllPoints = GridPoints(start, furthest);
        foreach (Vector2Int point in AllPoints)
        {
            changedOne = activeGrid.Find(room => room.position == point);
            CreateAdjacent(changedOne.position);
            changedOne.shape = shape;
            changedOne.state = multiState;
            changedOne.roomID = multiGridID;

        }
        changedOne = activeGrid.Find(room => room.position == start);
        changedOne.state = startState;
        changedOne.shape = shape;
    }

    #endregion

    #region Room Shape Checks and sets

    private int SelectRoom(RoomGrid position)
    {
        //Simple for debugging
        float random = Random.value;
        if (random <= 0.75)
            return Set_1x1(position);
        else if (random <= 0.875)
            return SetRectangular(position, new Vector2Int(2, 2), RoomGrid.Shape._2x2, RoomGrid.State.multiGrid);
        else
            return SetRectangular(position, new Vector2Int(1, 2), RoomGrid.Shape._1x2, RoomGrid.State.multiGrid);
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
            else if (randomRoom.state == RoomGrid.State.unlikely && Random.value <= 0.1f)
            {
                Debug.Log("Unlikely Room hit at: " + randomRoom.position);
                return SelectRoom(roomToUpdate);
            }
            else
            {
                return 0;
            }
        }
        else
        {
            Debug.LogError("No Rooms to add");
            return 0;
        }
    }


    private int Set_1x1(RoomGrid room)
    {
        room.state = RoomGrid.State.occupied;

        room.shape = RoomGrid.Shape._1x1;
        //Room rotation (on a cartesian)
        room.cartesianPlane = Random.Range(0, 3);
        CreateAdjacent(room.position);
        return -1;
    }

    private int SetRectangular(RoomGrid checkRoom, Vector2Int dimentions, RoomGrid.Shape shape, RoomGrid.State multiGridState,RoomGrid.State startState = RoomGrid.State.occupied,RoomGrid.Type type = RoomGrid.Type.Standard , int offset = 0)
    {
        Vector2Int additiveDimention = dimentions - new Vector2Int(1, 1);

        //Quadrants like in math, Z up X right
        bool[] quadrants = { true, true, true, true };
        var furthestPoints = CartesianBounds(checkRoom.position, additiveDimention);
        Vector2Int[] newStart = new Vector2Int[4];
        List<int> trueIndecies = new List<int>();
        for (int i = 0; i < quadrants.Length; i++)
        {
            //Create offset variant
            newStart[i] = checkRoom.position + (new Vector2Int(cartesian[i].y, cartesian[i].x)*-offset);
            quadrants[i] = CheckGrid(newStart[i], furthestPoints[i]);
            if (quadrants[i]) trueIndecies.Add(i);
        }

        if (trueIndecies.Count == 0)
        {
            Debug.Log("" + dimentions + " couldn't place at" + newStart[0]);
            return 0;
        }
        int randomIndex = trueIndecies[Random.Range(0, trueIndecies.Count)];
        checkRoom.cartesianPlane = randomIndex;
        checkRoom.roomID = multiGridID;
        ReservePoints(newStart[randomIndex], furthestPoints[randomIndex], shape, multiGridState,startState);
        RoomGrid finalLocation = activeGrid.Find(location => location.position == newStart[randomIndex]);
        finalLocation.state = startState;
        finalLocation.type = type;
        multiGridID++;
        if(offset != 0)
        {
            Debug.Log("Boss room with offset spawned at: " + newStart[randomIndex]);
        }
        return -1;

    }

    #endregion


    #region Post Creation Logic
    public List<RoomGrid> DetectEndRooms()
    {
        RoomGrid existingRoom = null;
        List<RoomGrid> returnList = new List<RoomGrid>();
        List<RoomGrid> existantRooms = activeGrid.Where(room => room.state == RoomGrid.State.occupied || room.state == RoomGrid.State.multiGrid).ToList();
        foreach (RoomGrid position in existantRooms)
        {
            bool elligible = true;

            int neighbours = 0;
            foreach (Vector2Int direction in cartesian)
            {
                existingRoom = activeGrid.Find(room => room.position == position.position + direction);
                if (existingRoom.state == RoomGrid.State.occupied || existingRoom.state == RoomGrid.State.multiGrid)
                {
                    neighbours++;
                    if (neighbours > 1)
                    {
                        elligible = false;
                        break;
                    }

                }

            }
            if (elligible && existingRoom != null)
            {
                position.deadEnd = true;
                returnList.Add(position);
            }
        }
        return returnList;
    }

    public RoomGrid FurthestRoom(List<RoomGrid> endRooms)
    {
        RoomGrid furthest = null;
        float distance = 0;
        foreach (RoomGrid room in endRooms)
        {
            float current = Vector3.Distance(new Vector3(0, 0, 0), room.worldPos);
            if (current > distance)
            {
                distance = current;
                furthest = room;
            }

        }
        if (furthest != null)
            return furthest;
        else
        {
            Debug.LogError("No Furthest Room");
            return null;
        }

    }
    public void SetTreasureRooms()
    {
        int amount = 3;
        List<RoomGrid> endRooms = DetectEndRooms();
        //remove furthest
        endRooms.Remove(FurthestRoom(endRooms));
        foreach(RoomGrid room in endRooms)
        {
            if(amount>0 && room.shape == RoomGrid.Shape._1x1)
            {
                RoomGrid roomToChange = activeGrid.Find(check => check == room);
                roomToChange.type = RoomGrid.Type.Treasure;
                amount--;
            }
            else if (amount > 0)
            {
                Debug.LogError("No applicible End Rooms");
            }
        }
    }

    public void CreateBossRoom(TileSet tileset)
    {
        int index = Random.Range(0, tileset._BossRooms.Length);

        RoomGrid furthest = FurthestRoom(DetectEndRooms());
        Debug.Log("Furthest room at " + furthest.position + "  " + furthest.state);
        CreateAdjacent(furthest.position);
        RoomGrid furthestAdjacents = null;
        float distance = 0;
        foreach(Vector2Int direction in cartesian)
        {
            RoomGrid existingRoom = CreateAtPosition(furthest.position + direction, RoomGrid.State.empty);
            float current = Vector3.Distance(new Vector3(0, 0, 0), existingRoom.worldPos);
            if (current > distance)
            {
                furthestAdjacents = existingRoom;
                distance = current;
            }
        }
        SetRectangular(furthestAdjacents, new Vector2Int(3, 3), RoomGrid.Shape._3x3, RoomGrid.State.forbidden,RoomGrid.State.forbidden,RoomGrid.Type.Boss,1);
        furthestAdjacents.state = RoomGrid.State.multiGrid;
        Debug.Log("Boss room created at " + furthestAdjacents.position);
    }

    public List<RoomGrid[]> AestheticDoorsLocation()
    {

        List<RoomGrid[]> returnList = new List<RoomGrid[]>();
        List<RoomGrid> onlyRoomedGrids = activeGrid.Where(room => room.state == RoomGrid.State.occupied || room.state == RoomGrid.State.multiGrid).ToList();
        foreach (RoomGrid knownRoom in onlyRoomedGrids)
        {


            foreach (Vector2Int direction in cartesian)
            {
                bool requireDoor = false;
                RoomGrid roomToCheck = activeGrid.Find(room => room.position == knownRoom.position + direction);
                if (roomToCheck != null && (roomToCheck.state == RoomGrid.State.occupied || roomToCheck.state == RoomGrid.State.multiGrid))
                {
                    if (knownRoom.roomID >= 0 && roomToCheck.roomID != knownRoom.roomID)
                    {
                        requireDoor = true;
                    }
                    else if (knownRoom.roomID < 0)
                    {
                        requireDoor = true;
                    }
                }
                if (requireDoor)
                {
                    returnList.Add(new RoomGrid[2] { knownRoom, roomToCheck });
                }

            }
        }

        return returnList;
    }



    #endregion

    public void debugger()
    {
        var output = GridPoints(new Vector2Int(0, 0), new Vector2Int(1, 1));
        Debug.Log(output);
    }

}
