using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct DoorPairings
{
    public GameObject sealedWall;
    public GameObject doorWall;
    public Transform detector;
    public DoorPairings(GameObject sealedWall, GameObject doorWall, Transform detector)
    {
        this.sealedWall = sealedWall;
        this.doorWall = doorWall;
        this.detector = detector;
    }
}
public class SetDoors : MonoBehaviour
{
    public List<DoorPairings> doors = new List<DoorPairings>();

    public void OpenDoors()
    {
        RaycastHit hit;
        foreach(DoorPairings door in doors)
        {
            if(Physics.Raycast(door.detector.position,door.detector.forward,out hit, 1f))
            {
                door.sealedWall.SetActive(false);
                door.doorWall.SetActive(true);
            }
            else
            {
                door.sealedWall.SetActive(true);
                door.doorWall.SetActive(false);
            }
        }
    }
}
