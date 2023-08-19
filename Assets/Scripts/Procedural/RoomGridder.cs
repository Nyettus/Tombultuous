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
    public enum State { available, forbidden, occupied, unlikely };

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
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0,1),    //North
            new Vector2Int(0,-1),    //South
            new Vector2Int(1,0),    //East
            new Vector2Int(-1,0)     //West
        };
        foreach (Vector2Int direction in directions)
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
        
        int RandomPos = Random.Range(0, availableRooms.Count-1);
        RoomGrid randomRoom = availableRooms[RandomPos];
        RoomGrid roomToUpdate = activeGrid.Find(room => room.position == randomRoom.position);
        if (roomToUpdate != null)
        {
            roomToUpdate.state = RoomGrid.State.occupied;
            CreateAdjacent(roomToUpdate.position);
            return -1;
        }
        else
        {
            Debug.LogError("No Rooms to add");
            return 0;
        }
    }

    public void debugger()
    {
        List<RoomGrid> availableRooms = activeGrid.Where(room => room.state == RoomGrid.State.available).ToList();
        Debug.Log(availableRooms.Count);
        foreach (RoomGrid room in availableRooms)
        {

            Debug.Log(""+room.state+" | "+room.position);
        }
    }

}
