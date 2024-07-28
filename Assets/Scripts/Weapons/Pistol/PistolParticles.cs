using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolParticles : MonoBehaviour
{
    //Order the same way the barrels fire
    [SerializeField] private ParticleSystem[] fireParticles;

    public void PP_Fire(int input)
    {
        fireParticles[input].Play();
    }

    public void PP_Special()
    {
        foreach(ParticleSystem par in fireParticles)
        {
            par.Play();
        }
    }
}
