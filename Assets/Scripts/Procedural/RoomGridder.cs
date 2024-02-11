using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UsefulBox;


[System.Serializable]
public class RoomGrid
{
    public float roomSize;
    public Vector2Int position;
    public Vector3 worldPos;
    public State state;
    public Type type;
    public Shape shape;
    //This refers to index of V2I[] cartesian
    public int cartesianPlane = 0;
    public int roomID = -1;
    public bool deadEnd = false;

    public enum Type { Standard, Boss, Treasure };
    public enum State { Available, Forbidden, Occupied, Unlikely, Empty, MultiGrid };
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
    [HideInInspector]
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
        RoomGrid spawnRoom = new RoomGrid(start, RoomGrid.State.MultiGrid);
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
                activeGrid.Add(new RoomGrid(spawnPosition, RoomGrid.State.Available));
            }
            else if (existingRoom.state == RoomGrid.State.Available)
            {
                existingRoom.state = RoomGrid.State.Unlikely;
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
            tested = CreateAtPosition(point, RoomGrid.State.Empty);
            if (!(tested.state == RoomGrid.State.Available || tested.state == RoomGrid.State.Unlikely || tested.state == RoomGrid.State.Empty))
            {
                return false;
            }

        }
        return true;
    }

    private void ReservePoints(Vector2Int start, Vector2Int furthest, RoomGrid.Shape shape, RoomGrid.State multiState, RoomGrid.State startState = RoomGrid.State.Occupied)
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
            return SetRectangular(position, new Vector2Int(2, 2), RoomGrid.Shape._2x2, RoomGrid.State.MultiGrid);
        else
            return SetRectangular(position, new Vector2Int(1, 2), RoomGrid.Shape._1x2, RoomGrid.State.MultiGrid);
    }


    public int SetNextRoom()
    {
        List<RoomGrid> availableRooms = activeGrid.Where(room => room.state == RoomGrid.State.Available || room.state == RoomGrid.State.Unlikely).ToList();

        int RandomPos = Random.Range(0, availableRooms.Count - 1);
        RoomGrid randomRoom = availableRooms[RandomPos];
        RoomGrid roomToUpdate = activeGrid.Find(room => room.position == randomRoom.position);
        if (roomToUpdate != null)
        {
            if (randomRoom.state == RoomGrid.State.Available)
            {

                return SelectRoom(roomToUpdate);
            }
            else if (randomRoom.state == RoomGrid.State.Unlikely && Random.value <= 0.1f)
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
        room.state = RoomGrid.State.Occupied;

        room.shape = RoomGrid.Shape._1x1;
        //Room rotation (on a cartesian)
        room.cartesianPlane = Random.Range(0, 3);
        CreateAdjacent(room.position);
        return -1;
    }

    private int SetRectangular(RoomGrid checkRoom, Vector2Int dimentions, RoomGrid.Shape shape, RoomGrid.State multiGridState, RoomGrid.State startState = RoomGrid.State.Occupied, RoomGrid.Type type = RoomGrid.Type.Standard)
    {
        Vector2Int additiveDimention = dimentions - new Vector2Int(1, 1);

        //Quadrants like in math, Z up X right
        bool[] quadrants = { true, true, true, true };
        var furthestPoints = CartesianBounds(checkRoom.position, additiveDimention);
        List<int> trueIndecies = new List<int>();
        for (int i = 0; i < quadrants.Length; i++)
        {
            //Create offset variant
            quadrants[i] = CheckGrid(checkRoom.position, furthestPoints[i]);
            if (quadrants[i]) trueIndecies.Add(i);
        }

        if (trueIndecies.Count == 0)
        {
            Debug.Log("" + dimentions + " couldn't place at" + checkRoom.position);
            return 0;
        }
        int randomIndex = trueIndecies[Random.Range(0, trueIndecies.Count)];
        checkRoom.cartesianPlane = randomIndex;
        checkRoom.roomID = multiGridID;
        ReservePoints(checkRoom.position, furthestPoints[randomIndex], shape, multiGridState, startState);
        RoomGrid finalLocation = activeGrid.Find(location => location.position == checkRoom.position);
        finalLocation.state = startState;
        finalLocation.type = type;
        multiGridID++;

        return -1;

    }

    private int SetCenteredSquare(RoomGrid checkRoom, int side, bool randomDir, RoomGrid.Shape shape, RoomGrid.State multiGridState, RoomGrid.State startState = RoomGrid.State.Occupied, RoomGrid.Type type = RoomGrid.Type.Standard)
    {
        if (side % 2 == 0)
        {
            Debug.LogError("Centered square cannot have even sides");
            return 0;
        }
        //Not quadrants but cartesian directions
        bool[] facings = { false, false, false, false }; // Order of v2i cartesian 
        List<int> trueIndecies = new List<int>();
        for (int i = 0; i < cartesian.Length; i++)
        {

            if (CheckGrid(checkRoom.position + cartesian[PsychoticBox.WrapIndex(i + 1, cartesian.Length)], checkRoom.position + cartesian[PsychoticBox.WrapIndex(i - 1, cartesian.Length)] + cartesian[i] * (side - 1)))
            {
                facings[i] = true;
                if (facings[i]) trueIndecies.Add(i);
            }
        }

        if (trueIndecies.Count == 0)
        {
            Debug.Log("Square of side " + side + " couldn't place at" + checkRoom.position);
            return 0;
        }
        int randomIndex = trueIndecies[Random.Range(0, trueIndecies.Count)];
        RoomGrid newCenter = activeGrid.Find(newRoom => newRoom.position == checkRoom.position + cartesian[randomIndex] * ((side - 1) / 2));
        ReservePoints(newCenter.position, checkRoom.position + cartesian[PsychoticBox.WrapIndex(randomIndex - 1, cartesian.Length)] + cartesian[randomIndex] * (side - 1), shape, multiGridState, startState);
        newCenter.shape = shape;
        newCenter.state = startState;
        newCenter.type = type;
        if (randomDir)
        {
            newCenter.cartesianPlane = Random.Range(0, 3);
        }
        else
        {
            var direction = System.Array.IndexOf(cartesian, (newCenter.position - checkRoom.position));
            newCenter.cartesianPlane = direction;
        }
        multiGridID++;
        return -1;

    }

    #endregion

    #region Post Creation Logic
    public List<RoomGrid> DetectEndRooms()
    {
        RoomGrid existingRoom = null;
        List<RoomGrid> returnList = new List<RoomGrid>();
        List<RoomGrid> existantRooms = activeGrid.Where(room => (room.state == RoomGrid.State.Occupied || room.state == RoomGrid.State.MultiGrid)).ToList();
        foreach (RoomGrid position in existantRooms)
        {
            bool elligible = true;

            int neighbours = 0;
            foreach (Vector2Int direction in cartesian)
            {
                existingRoom = activeGrid.Find(room => room.position == position.position + direction);
                //TODO distinction between 1x1 and multigrid
                if (position.shape == RoomGrid.Shape._1x1)
                {
                    if (existingRoom.state == RoomGrid.State.Occupied || existingRoom.state == RoomGrid.State.MultiGrid)
                    {
                        neighbours++;
                        if (neighbours > 1)
                        {
                            elligible = false;
                            break;
                        }
                    }
                }
                else
                {
                    if ((position.roomID != existingRoom.roomID) && (existingRoom.state == RoomGrid.State.Occupied || existingRoom.state == RoomGrid.State.MultiGrid))
                    {
                        neighbours++;
                        if (neighbours > 1)
                        {
                            elligible = false;
                            break;
                        }
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
        endRooms.RemoveAll(room => room.shape != RoomGrid.Shape._1x1);
        List<RoomGrid> shuffledEndRooms = endRooms.OrderBy(x => Random.value).ToList();
        //List<RoomGrid> shuffledEndRooms = endRooms.OrderBy(x => x.worldPos.magnitude).ToList();
        //shuffledEndRooms.Reverse();

        foreach (RoomGrid room in shuffledEndRooms)
        {
            if (amount > 0)
            {
                RoomGrid roomToChange = activeGrid.Find(check => check == room);
                roomToChange.type = RoomGrid.Type.Treasure;
                foreach (Vector2Int direction in cartesian)
                {
                    var neighbour = activeGrid.Find(room => room.position == roomToChange.position + direction);
                    if (neighbour.state == RoomGrid.State.Occupied || neighbour.state == RoomGrid.State.MultiGrid)
                    {
                        var newDir = System.Array.IndexOf(cartesian, direction * -1);
                        roomToChange.cartesianPlane = newDir;
                        break;

                    }
                }
                amount--;
            }
        }

        //if (shuffledEndRooms.Count < amount)
        //{
        //    Debug.LogError("Not enough slots for treasure rooms");
        //    RoomManager._.ReloadScene();
        //}
        //for(int i = amount; i>0; i--)
        //{
        //    float multiplier = (float)i / (float)amount;
        //    int index = (int)Mathf.Clamp((multiplier * shuffledEndRooms.Count),0,amount-1);
        //    Debug.Log("" + shuffledEndRooms.Count + " : " + index + " : " + multiplier);
        //    RoomGrid roomToChange = activeGrid.Find(check => check == shuffledEndRooms[index]);
        //    roomToChange.type = RoomGrid.Type.Treasure;
        //    foreach (Vector2Int direction in cartesian)
        //    {
        //        var neighbour = activeGrid.Find(room => room.position == roomToChange.position + direction);
        //        if (neighbour.state == RoomGrid.State.Occupied || neighbour.state == RoomGrid.State.MultiGrid)
        //        {
        //            var newDir = System.Array.IndexOf(cartesian, direction * -1);
        //            roomToChange.cartesianPlane = newDir;
        //            break;

        //        }
        //    }
        //}



        if (amount > 0)
        {
            Debug.LogError("Couldnt create all treasure rooms");
            RoomManager._.ReloadScene();
        }

    }

    public void CreateBossRoom(TileSet tileset)
    {
        int index = Random.Range(0, tileset._BossRooms.Length);

        RoomGrid furthest = FurthestRoom(DetectEndRooms());
        CreateAdjacent(furthest.position);
        RoomGrid furthestAdjacents = null;
        float distance = 0;
        foreach (Vector2Int direction in cartesian)
        {
            RoomGrid existingRoom = CreateAtPosition(furthest.position + direction, RoomGrid.State.Empty);
            float current = Vector3.Distance(new Vector3(0, 0, 0), existingRoom.worldPos);
            if (current > distance)
            {
                furthestAdjacents = existingRoom;
                distance = current;
            }
        }
        int test = SetCenteredSquare(furthestAdjacents, 3, false, RoomGrid.Shape._3x3, RoomGrid.State.Forbidden, RoomGrid.State.Forbidden, RoomGrid.Type.Boss);
        furthestAdjacents.state = RoomGrid.State.MultiGrid;
        if(test == 0)
        {
            Debug.LogError("Boss Room Didn't spawn");
            RoomManager._.ReloadScene();
        }
    }

    public List<RoomGrid[]> AestheticDoorsLocation()
    {

        List<RoomGrid[]> returnList = new List<RoomGrid[]>();
        List<RoomGrid> onlyRoomedGrids = activeGrid.Where(room => room.state == RoomGrid.State.Occupied || room.state == RoomGrid.State.MultiGrid).ToList();
        foreach (RoomGrid knownRoom in onlyRoomedGrids)
        {


            foreach (Vector2Int direction in cartesian)
            {
                bool requireDoor = false;
                RoomGrid roomToCheck = activeGrid.Find(room => room.position == knownRoom.position + direction);
                if (roomToCheck != null && (roomToCheck.state == RoomGrid.State.Occupied || roomToCheck.state == RoomGrid.State.MultiGrid))
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
