using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEvents : MonoBehaviour
{



    public static ProceduralEvents instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    public event Action onSpawnRooms;
    public void SpawnRooms()
    {
        if (onSpawnRooms != null)
        {
            onSpawnRooms();
        }
    }

    public event Action onStepRoom;
    public void StepRooms()
    {
        if (onStepRoom != null)
        {
            onStepRoom();
        }
    }
}
