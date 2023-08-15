using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogistics : MonoBehaviour
{
    public static RoomLogistics instance;
    public int roomCount = 50;

    public List<Vector3> roomPositions = new List<Vector3>();
    public List<Transform> availablePositions = new List<Transform>();
    public List<Vector3> bannedLocations = new List<Vector3>();

    public RoomVarients[] room;
    public float[] rates;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }

    public bool once = true;
    private void Update()
    {
        if(roomCount > 0)
        {
            int random = Random.Range(0,availablePositions.Count);
            if (availablePositions.Count>0)
            {
                RoomVarients temp = room[Chances()];
                int r = Random.Range(0, temp.rooms.Length);
                Instantiate(room[Chances()].rooms[r], availablePositions[random].position, availablePositions[random].rotation);
                availablePositions.RemoveAt(random);
            }
            else
            {
                Debug.Log("its the end of days");
                
            }


            roomCount--;
        }
        else if(once == true)
        {
            ProceduralEvents.instance.SpawnRooms();
            once = false;
        }


    }


    private int Chances()
    {
        float chance = Random.value;
        if (chance >= 0 && chance <= rates[0])
            return 0;
        else if (chance > rates[0])
            return 1;
        else
            return 0;
    }


}
