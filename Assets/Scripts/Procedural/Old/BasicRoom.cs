using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRoom : MonoBehaviour
{
    public Transform[] doorwayLocations;
    private Transform[] roundedDoorwayLocations;

    public Transform[] nextRoomLocations;
    private Vector3[] roundedNextRoomLocations;

    public Transform[] roomPos;
    public Vector3[] roundedRoomPos;

    public Transform roomModelPos;
    public Vector3 roundedModelPos;

    public GameObject roomModel;

    private int roomSize;

    // Start is called before the first frame update
    void Start()
    {
        RoundVectors();
        RejectItself();
        PositionsInList();

        ProceduralEvents.instance.onSpawnRooms += SpawnRoom;
    }

   
    public void PositionsInList()
    {
        //set its positions as in logistics
        for(int i = 0; i < roomPos.Length; i++)
        {
            RoomLogistics.instance.roomPositions.Add(roundedRoomPos[i]);
            RoomLogistics.instance.bannedLocations.Add(roundedRoomPos[i]);
        }


        //set its guaranteed spawn location
        int guaranteed = Random.Range(1, roundedDoorwayLocations.Length);
        if (!RoomLogistics.instance.bannedLocations.Contains(roundedNextRoomLocations[guaranteed])&&
            !RoomLogistics.instance.roomPositions.Contains(roundedNextRoomLocations[guaranteed]))
        {
            RoomLogistics.instance.availablePositions.Add(roundedDoorwayLocations[guaranteed]);
        }
        else
        {
            for(int i = 0; i< roundedDoorwayLocations.Length; i++)
            {
                if (!RoomLogistics.instance.bannedLocations.Contains(roundedNextRoomLocations[i])&&
                    !RoomLogistics.instance.roomPositions.Contains(roundedNextRoomLocations[i]))
                {
                    guaranteed = i;
                    RoomLogistics.instance.availablePositions.Add(roundedDoorwayLocations[guaranteed]);
                    break;
                }
            }
        }

        //set remainding as possible
        for(int i = 0; i < roundedDoorwayLocations.Length; i++)
        {
            if (!RoomLogistics.instance.bannedLocations.Contains(roundedNextRoomLocations[i])&&
                i!=guaranteed&&
                !RoomLogistics.instance.roomPositions.Contains(roundedNextRoomLocations[i]))
            {
                RoomLogistics.instance.availablePositions.Add(roundedDoorwayLocations[i]);
            }
        }


    }


    public void RejectItself()
    {
        for(int i = 1; i < roundedNextRoomLocations.Length; i++)
        {
            if (RoomLogistics.instance.bannedLocations.Contains(roundedNextRoomLocations[i]))
            {
                Destroy(gameObject);
            }
        }
        for(int i = 1; i <roomPos.Length; i++)
        {
            if (RoomLogistics.instance.bannedLocations.Contains(roomPos[i].position))
            {
                Destroy(gameObject);
            }
        }
        
    }



    public void RoundVectors()
    {
        roundedRoomPos = new Vector3[roomPos.Length];
        for(int i = 0; i < roundedRoomPos.Length; i++)
        {
            roundedRoomPos[i] = new Vector3(Mathf.Round(roomPos[i].position.x * 2) / 2,
                Mathf.Round(roomPos[i].position.y * 2) / 2,
                Mathf.Round(roomPos[i].position.z * 2) / 2);
        }
        

        roundedModelPos = new Vector3(Mathf.Round(roomModelPos.position.x * 2) / 2,
            Mathf.Round(roomModelPos.position.y * 2) / 2,
            Mathf.Round(roomModelPos.position.z * 2 )/ 2);

        roundedDoorwayLocations = new Transform[doorwayLocations.Length];
        for(int i = 0; i < doorwayLocations.Length; i++)
        {
            roundedDoorwayLocations[i] = doorwayLocations[i];
            roundedDoorwayLocations[i].position = new Vector3(
                Mathf.Round(doorwayLocations[i].position.x * 2) / 2,
                Mathf.Round(doorwayLocations[i].position.y * 2) / 2,
                Mathf.Round(doorwayLocations[i].position.z * 2) / 2);
        }

        roundedNextRoomLocations = new Vector3[nextRoomLocations.Length];
        for(int i = 0; i < nextRoomLocations.Length; i++)
        {

            roundedNextRoomLocations[i] = new Vector3(
                Mathf.Round(nextRoomLocations[i].position.x * 2) / 2,
                Mathf.Round(nextRoomLocations[i].position.y * 2) / 2,
                Mathf.Round(nextRoomLocations[i].position.z * 2) / 2);
        }

        roomSize = roomPos.Length;

    }
   

    public void SpawnRoom()
    {
        Instantiate(roomModel, roundedModelPos, roomModelPos.rotation);
        if (EndRoomCheck()&&roomSize ==1)
        {
            Debug.Log("IMMA END ROOM");
        }
    }

    public bool EndRoomCheck()
    {
        bool stay = true;
        for(int i = 1; i < roundedNextRoomLocations.Length; i++)
        {
            if (RoomLogistics.instance.roomPositions.Contains(roundedNextRoomLocations[i]))
            {
                stay = false;
                return false;
            }
        }
        return stay;
    }

    private void OnDestroy()
    {
        ProceduralEvents.instance.onSpawnRooms -= SpawnRoom;

        RoomLogistics.instance.roomCount++;
        for(int i = 0; i < roundedDoorwayLocations.Length; i++)
        {
            if (RoomLogistics.instance.availablePositions.Contains(roundedDoorwayLocations[i]))
            {
                RoomLogistics.instance.availablePositions.Remove(roundedDoorwayLocations[i]);
            }
        }
        for(int i = 0; i < roundedRoomPos.Length; i++)
        {
            if (RoomLogistics.instance.roomPositions.Contains(roundedRoomPos[i]))
            {
                RoomLogistics.instance.roomPositions.Remove(roundedRoomPos[i]);
            }
        }

    }
}
