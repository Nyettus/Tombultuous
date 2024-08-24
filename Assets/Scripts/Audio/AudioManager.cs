using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UsefulBox;


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

    

    public void UpdatePauseParams(bool state)
    {
        int asInt = state ? 1 : 0;
        RuntimeManager.StudioSystem.setParameterByName("PauseBool", asInt);
    }

}
