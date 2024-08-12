using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : SingletonPersist<AudioManager>
{
    

    private void Awake()
    {
        Startup(this);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
